@tool
extends EditorPlugin

## C# Auto Build
##
## 切回编辑器窗口时，如果发现 .cs 比编译产物（程序集 dll）新，就在后台跑一次
## `dotnet build`。编译完 dll 一变新，Godot 编辑器自己的 HotReloadAssemblyWatcher
## 就会在 0.5 秒内重载程序集，Inspector 随之刷新 —— 于是不用再手动按 Ctrl+B。
##
## ---------------------------------------------------------------------------
## v2 修的问题
## ---------------------------------------------------------------------------
## 旧版用 OS.create_process，那是"发射后不管"的：拿不到 stdout，也不知道退出码。
## 于是编译一旦失败（比如某个 .cs 有语法错误），插件只会干等 60 秒，
## 然后吐一句"迟迟没有产出新的程序集，编译可能失败了" —— 具体哪一行、什么错，
## 完全看不到。结果就是：插件看起来像彻底坏了，实际上它每次都在正常干活，
## 只是每一次都被同一个编译错误挡住了。
##
## 现在改成：在线程里跑 OS.execute（它是阻塞的，但不在主线程上，编辑器不卡），
## 拿到退出码和完整输出。失败时把错误原样打进 Output 面板。
##
## 另外加了一个手动触发：「项目 → 工具 → 立即编译 C#」。
## 不再完全依赖窗口焦点通知 —— 万一通知没送到，你还有手动的路。
##
## 装：把 addons/csharp_auto_build 丢进项目，项目设置 → 插件 → 勾选「C# Auto Build」
## 卸：取消勾选，或者直接删掉这个文件夹

const BUILD_CONFIG := "Debug"        # 编辑器加载的是 Debug 程序集
const WATCHDOG_MSEC := 180000        # 3 分钟还没结束就提醒一下（不杀线程）
const LOG_FILE := "res://.godot/auto_build.log"

var _building := false
var _thread: Thread = null
var _csproj := ""
var _started_at := 0


func _enter_tree() -> void:
	# 注意这里是 GDScript，不是 C#：方法是 snake_case，直接传方法引用即可，
	# 没有 Callable.From 这种东西（那是 C# 侧的写法）。
	add_tool_menu_item("立即编译 C#", _on_manual_build)
	set_process(true)


func _exit_tree() -> void:
	remove_tool_menu_item("立即编译 C#")
	# 插件被关掉时线程可能还在跑，必须等它收尾，否则会崩
	if _thread != null:
		if _thread.is_alive():
			_thread.wait_to_finish()
		_thread = null
	_building = false


func _notification(what: int) -> void:
	if what != NOTIFICATION_WM_WINDOW_FOCUS_IN or _building:
		return
	if _is_playing():
		return                         # 游戏跑着的时候 dll 被占用，编译会失败
	if not _is_stale():
		return                         # 没有改动就别白编
	_start_build()


func _on_manual_build() -> void:
	if _building:
		print("[C# Auto Build] 已经在编译了，等这次结束。")
		return
	_start_build()


# ------------------------------------------------------------------ 编译

func _start_build() -> void:
	_csproj = _find_csproj()
	if _csproj.is_empty():
		push_error("[C# Auto Build] 项目根目录下找不到 .csproj。")
		return

	_building = true
	_started_at = Time.get_ticks_msec()
	print("[C# Auto Build] 开始编译 %s（%s）…" % [_csproj.get_file(), BUILD_CONFIG])

	_thread = Thread.new()
	_thread.start(_build_thread)


## 这段跑在子线程里。
## OS.execute 会阻塞调用线程直到子进程结束，所以绝对不能放在主线程，
## 否则整个编辑器会卡住几秒钟。
func _build_thread() -> Dictionary:
	var log_abs := ProjectSettings.globalize_path(LOG_FILE)
	if FileAccess.file_exists(LOG_FILE):
		DirAccess.remove_absolute(log_abs)          # 清掉上一次的残留

	# 让 MSBuild 自己把日志按 UTF-8 写进文件。
	var flp := '-flp:LogFile="%s";Encoding=UTF-8;Verbosity=minimal' % log_abs

	var output := []
	var code := OS.execute(
		"dotnet",
		["build", _csproj, "-c", BUILD_CONFIG, "-nologo", "-v:minimal", "-fl", flp],
		output,
		true)                          # read_stderr = true，编译错误走的是 stderr

	# 注意：这里**故意不用** output 里的内容。
	# Windows 上 Godot 会把子进程吐出的 UTF-8 字节按系统 ANSI 代码页解码，
	# 结果是 "正在确定要还原的项目…" 变成 "姝ｅ湪纭畾瑕佽繕鍘熺殑椤圭洰鈥?"。
	# 报错里的文件名和行号还在（纯 ASCII），但错误描述全糊了。
	# 所以主用日志文件：FileAccess 读取时是按 UTF-8 解码的，中文正常。
	var fallback := ""
	for chunk in output:
		fallback += str(chunk)

	var text := ""
	if FileAccess.file_exists(LOG_FILE):
		var f := FileAccess.open(LOG_FILE, FileAccess.READ)
		if f != null:
			text = f.get_as_text()
			f.close()

	return {"code": code, "out": text, "fallback": fallback}


func _process(_delta: float) -> void:
	if _thread == null:
		return

	if _thread.is_alive():
		if _building and Time.get_ticks_msec() - _started_at > WATCHDOG_MSEC:
			push_warning("[C# Auto Build] 编译已经跑了 3 分钟还没结束，可能卡住了。"
				+ " 建议手动跑一次 dotnet build 看看。")
			_started_at = Time.get_ticks_msec()   # 避免每帧都警告
		return

	var result: Dictionary = _thread.wait_to_finish()
	_thread = null
	_building = false
	_report(result)


## 把结果打进 Output 面板。成功就一行，失败把完整错误原样倒出来。
func _report(result: Dictionary) -> void:
	var code := int(result["code"])
	if code == 0:
		print("[C# Auto Build] ✓ 编译成功，程序集已更新，Inspector 马上刷新。")
		return

	push_error("[C# Auto Build] ✗ 编译失败（退出码 %d）。下面只列错误行，完整日志见 %s" % [code, LOG_FILE])

	var text := str(result["out"]).strip_edges()
	if text.is_empty():
		# 日志文件没生成（比如 dotnet 根本没启动），退回到原始输出并说明可能乱码
		text = str(result["fallback"]).strip_edges()
		if text.is_empty():
			print("    (没捕获到任何输出 —— 手动在项目目录跑一次 dotnet build 看看。)")
			return
		print("    (日志文件没生成，下面是原始输出，中文可能乱码)")

	# 只把真正带 "error" 的行揪出来，不然刷你一屏
	var shown := 0
	for line in text.split("\n"):
		var trimmed := line.strip_edges()
		if trimmed == "":
			continue
		if trimmed.to_lower().contains("error") or trimmed.contains("错误"):
			print("    " + trimmed)
			shown += 1
	if shown == 0:
		print("    (日志里没有找到 error 行，看完整日志：" + LOG_FILE + ")")


# ------------------------------------------------------------------ 判断

func _is_stale() -> bool:
	if _find_csproj().is_empty():
		return false                   # 不是 C# 项目，什么都不做
	var dll := _find_assembly()
	if dll.is_empty():
		return true                    # 还没编译过
	return _newest_script_time("res://") > FileAccess.get_modified_time(dll)


func _is_playing() -> bool:
	return EditorInterface.has_method("is_playing_scene") and EditorInterface.is_playing_scene()


## res:// 下最新的 .cs 的时间戳（跳过 .godot 之类的点开头目录）
func _newest_script_time(dir_path: String) -> int:
	var newest := 0
	var dir := DirAccess.open(dir_path)
	if dir == null:
		return 0
	dir.list_dir_begin()
	var entry := dir.get_next()
	while entry != "":
		var path := dir_path.path_join(entry)
		if dir.current_is_dir():
			if not entry.begins_with("."):
				newest = maxi(newest, _newest_script_time(path))
		elif entry.ends_with(".cs"):
			newest = maxi(newest, FileAccess.get_modified_time(path))
		entry = dir.get_next()
	dir.list_dir_end()
	return newest


# ------------------------------------------------------------------ 路径

## 项目根目录下的 .csproj（绝对路径）。Godot 默认在项目根建一个。
func _find_csproj() -> String:
	var dir := DirAccess.open("res://")
	if dir == null:
		return ""
	dir.list_dir_begin()
	var entry := dir.get_next()
	while entry != "":
		if not dir.current_is_dir() and entry.ends_with(".csproj"):
			dir.list_dir_end()
			return ProjectSettings.globalize_path("res://" + entry)
		entry = dir.get_next()
	dir.list_dir_end()
	return ""


## 程序集名：优先用项目设置，取不到就用 .csproj 的文件名
func _assembly_name() -> String:
	var setting := str(ProjectSettings.get_setting("dotnet/project/assembly_name", ""))
	if not setting.is_empty():
		return setting
	var csproj := _find_csproj()
	return "" if csproj.is_empty() else csproj.get_file().get_basename()


func _find_assembly() -> String:
	var assembly_name := _assembly_name()
	if assembly_name.is_empty():
		return ""
	var dll := "res://.godot/mono/temp/bin/%s/%s.dll" % [BUILD_CONFIG, assembly_name]
	return dll if FileAccess.file_exists(dll) else ""
