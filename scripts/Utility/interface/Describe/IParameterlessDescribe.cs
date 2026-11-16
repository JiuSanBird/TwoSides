/// <summary>
/// 无参描述
/// 使懒专用
/// </summary>
public interface IParameterlessDescribe
{
    /// <summary>
    /// 原生描述
    /// 这是未经处理的描述,内含"{参数}"等内容,仅供文本解释器调用
    /// </summary>
    public string PrimaryDescribe { get; }
}