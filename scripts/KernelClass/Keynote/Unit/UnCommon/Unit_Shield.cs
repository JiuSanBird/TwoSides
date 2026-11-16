using System;
using System.Collections.Generic;

/// <summary>
/// Unit_护盾相关
/// </summary>
public partial class Unit
{
    public List<Shield> Shield { get; set; } = new();
    /// <summary>
    /// 获得护盾
    /// 用以加强护盾
    /// </summary>
    public event Action<Shield> GainShield;

    public bool AddShield(Shield item)
    {
        GainShield.Invoke(item);
        Shield.Add(item);
        return true;
    }

    public bool RemoveShield(Shield item)
    {
        Shield.Remove(item);
        return true;
    }
}

/// <summary>
/// 护盾
/// </summary>
public class Shield
{
    public int Value { get; set; }
    /// <summary>持续回合数</summary>
    public int Bout { get; set; }
    /// <summary>护盾被击碎时调用</summary>
    public Action Shredding { get; set; }
}