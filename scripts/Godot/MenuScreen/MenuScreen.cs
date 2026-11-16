using Godot;

public partial class MenuScreen : Node2D
{
    public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("MenuScreen_退出游戏"))
        {
            GetTree().Quit();
            return;
        }
    }
}
