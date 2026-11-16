using System.Collections.Generic;

/// <summary>
/// Unit_词条管理
/// </summary>
public partial class Unit
{
    public List<Entry> Entry { get; set; } = new();

    public void AddEntry(Entry item)
    {
        Entry.Add(item);
        item.Effectuate(this);
    }

    public void RemoveEntry(Entry item)
    {
        Entry.Remove(item);
        item.Expire();
    }
}
