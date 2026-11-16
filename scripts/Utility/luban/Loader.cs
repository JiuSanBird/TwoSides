using Godot;
using System.Text.Json;

public partial class Loader : Node
{
    // 全局静态访问点
    public static Tables Tables { get; private set; }

    public override void _Ready()
    {
        // 假设JSON 配置文件放在 res://luban//json/ 目录下
        string configDir = "res://luban//json/";
        // 传入自定义加载函数
        Tables = new Tables(fileName => LoadJson(configDir + fileName + ".json"));
    }

    private static JsonElement LoadJson(string path)
    {
        // 1. 使用 Godot 的 FileAccess 读取文件
        using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.PrintErr($"无法加载配置文件: {path}");
            // 返回一个空的 JSON 数组，防止 Luban 构造报错
            return JsonDocument.Parse("[]").RootElement.Clone();
        }

        using JsonDocument doc = JsonDocument.Parse(file.GetAsText());
        return doc.RootElement.Clone();
    }
}
