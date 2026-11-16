using Godot;

/// <summary>
/// 暂停菜单
/// </summary>
public partial class PauseMenu : CanvasLayer
{
	/// <summary>
	/// 当可见度发生变化
	/// </summary>
	private void OnVisibleChange()
	{
        GetTree().Paused = Visible;
	}
}