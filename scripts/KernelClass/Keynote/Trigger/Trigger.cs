using System;

/// <summary>
/// 触发器基类(模板方法 + 登记式解绑)
///
/// 用法:子类 override ReplenishEffectuate,用 Bind 把「订阅」和「解绑」成对写进去。
/// 失效时 Expire 统一执行登记下来的解绑,子类不用自己记着 -=,也就不会漏。
/// </summary>
public abstract class Trigger
{
    /// <summary>
    /// 生效/获得
    /// 供派生类调用,派生类必须调用
    /// 外界调用Effectuate,子类继承ReplenishEffectuate
    /// 如果无参数,则传值null即可
    /// </summary>
    public virtual void Effectuate(Unit owner = null) => ReplenishEffectuate(owner);
    /// <summary>
    /// 生效补充 
    /// 事件绑定
    /// 供最派生类调用
    /// </summary>
    protected virtual void ReplenishEffectuate(Unit owner = null) { }

    /// <summary>
    /// 解除绑定
    /// 生效时把解绑内容准备好,就不用暂存这个事件了
    /// </summary>
    protected event Action Untie;
    /// <summary>
    /// 失效/失去
    /// </summary>
    public virtual void Expire()
    {
        Untie?.Invoke();
        Untie = null;
        ReplenishExpire();
    }
    /// <summary>
    /// 失效补充
    /// 事件解绑
    /// </summary>
    protected virtual void ReplenishExpire() { }

    /// <summary>
    /// 绑定与解绑
    /// 封装值特供
    /// 必须在失去时解绑,其他一律不行
    /// </summary>
    protected void BindAndUntie(WValue wValue, Action<WValue> execute)
    {
        wValue.DataCompute += execute;

        Untie += () => wValue.DataCompute -= execute;
    }
}
