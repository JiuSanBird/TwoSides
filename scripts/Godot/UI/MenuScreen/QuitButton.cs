public partial class QuitButton : ClickEvent
{
    protected override void OnClick()
    {
        GetTree().Quit();
    }
}