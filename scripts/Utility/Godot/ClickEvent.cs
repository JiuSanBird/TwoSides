using Godot;

/// <summary>
/// 点击事件基类
/// </summary>
public abstract partial class ClickEvent : Button
{
    public override void _Ready() => Pressed += OnClick;
    protected abstract void OnClick();
}