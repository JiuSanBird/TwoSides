using Godot;

public partial class StartButton : ClickEvent
{
    protected override void OnClick()
    {
        GetTree().ChangeSceneToFile("res://scenes/Main/Map.tscn");
    }
}
