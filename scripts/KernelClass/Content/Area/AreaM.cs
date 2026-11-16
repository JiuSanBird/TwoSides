/// <summary>
/// 地区管理器
/// </summary>
public class AreaM : Singleton<AreaM>
{
    /// <summary>
    /// 当前所在地区
    /// </summary>
    public Area Curr { get; private set; }

    /// <summary>
    /// 切换地区
    /// </summary>
    /// <param name="area">目标地区</param>
    public void SwitchCurr(Area area = null)
    {
        // 如果没有指定, 就随机生成一个地区
        area ??= TbArea.GetOne(null, Curr);

        Curr?.Expire();
        Curr = area;
        Curr.Effectuate();
    }
}