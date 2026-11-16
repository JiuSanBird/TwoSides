using System.Collections.Generic;

/// <summary>
/// 获取描述
/// 就是需要IDescribe,IGetDescribe两个接口
/// </summary>
public interface IGetDescribe
{
    /// <summary>
    /// 获取描述
    /// => Interpreter.Process(this);
    /// </summary>
    public string GetDescribe();

    /// <summary>
    /// 获取表达式
    /// 仅供描述解释器调用
    /// </summary>
    public List<string> GetCalculated();
}