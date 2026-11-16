using System.Collections.Generic;

/// <summary>
/// Unit_技能管理
/// </summary>
public partial class Unit
{
    public List<Skill> Skill { get; set; } = new();

    public void AddSkill(Skill item)
    {
        Skill.Add(item);
    }

    public void RemoveSkill(Skill item)
    {
        Skill.Remove(item);
    }
}