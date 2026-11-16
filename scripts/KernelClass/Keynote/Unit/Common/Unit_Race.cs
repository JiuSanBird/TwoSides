using System.Collections.Generic;

/// <summary>
/// Unit_种族
/// </summary>
public partial class Unit
{
    /// <summary>
    /// 种族, 其中第一个种族必定为主种族
    /// </summary>
    private List<Race> Race { get; set; } = [null];

    /// <summary>
    /// 加入种族
    /// </summary>
    public void AddRace(Race race, bool isMainRace)
    {
        if(isMainRace)
        {
            if (Race[0] != null) RemoveRace(Race[0], true);
            Race[0] = race;
        }
        else
        {
            Race.Add(race);
        }

        race.Skill.ForEach(AddSkill);
        race.Entry.ForEach(AddEntry);
    }

    /// <summary>
    /// 移除种族
    /// </summary>
    public void RemoveRace(Race race, bool isMainRace)
    {
        if (isMainRace)
        {
            Race[0] = null;
        }
        else
        {
            Race.Remove(race);
        }

        race.Skill.ForEach(RemoveSkill);
        race.Entry.ForEach(RemoveEntry);
    }

    /// <summary>
    /// 是否为宠物
    /// </summary>
    public bool IsPet()
    {
        foreach(Race race in Race)
        {
            if(race != null && race.Cfg.IsPet)
            {
                return true;
            }
        }
        return false;
    }
}