/// <summary>
/// 有名氏
/// </summary>
public interface IHaveName
{
    /// <summary>
    /// 名称
    /// 改名是不可逆的
    /// </summary>
    public string Name { get; }
}