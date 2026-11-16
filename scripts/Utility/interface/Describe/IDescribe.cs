using System.Collections.Generic;

/// <summary>
/// 带参数描述
/// </summary>
public interface IDescribe : IParameterlessDescribe
{
    /// <summary>
    /// 参数
    /// </summary>
    public List<int> Par { get; }
}