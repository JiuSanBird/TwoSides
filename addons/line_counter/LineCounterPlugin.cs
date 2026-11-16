#if TOOLS
using Godot;
using System.IO;

[Tool]
public partial class LineCounterPlugin : EditorPlugin
{
    // 注意：这里必须用 new Callable(this, MethodName.xxx)，不能用 Callable.From(委托)。
    // Callable.From 包出来的托管委托会跟着 RemoveToolMenuItem 一起在编辑器关闭时被销毁，
    // 实测会让 Godot 在退出/禁用插件时直接崩掉（0xC0000005）。原生 Callable 只持有对象引用，没这个问题。
    public override void _EnterTree() => AddToolMenuItem("输出总代码行数", new Callable(this, MethodName.PrintTotalLine));
    public override void _ExitTree() => RemoveToolMenuItem("输出总代码行数");

    private void PrintTotalLine()
    {
        string rootPath = ProjectSettings.GlobalizePath("res://scripts");

        if (!Directory.Exists(rootPath))
        {
            GD.PrintErr($"目录不存在: {rootPath}");
            return;
        }

        //                                         搜索选项.所有目录
        string[] files = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories);

        int totalLine = 0;
        foreach (string file in files)
        {
            totalLine += CountCodeLines(file);
        }
        GD.Print($"总代码行数: {totalLine}");
    }

    /// <summary>
    /// 统计单个文件的代码行数
    /// </summary>
    private static int CountCodeLines(string filePath)
    {
        int count = 0;

        using var reader = new StreamReader(filePath);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            string t = line.Trim();

            // 跳过空行
            if (t.Length == 0) continue;

            // 跳过整行注释（// 和 /// 都算）
            if (t.StartsWith("//")) continue;

            ++count;
        }

        return count;
    }
}
#endif