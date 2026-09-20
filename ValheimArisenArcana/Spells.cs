using System;
using UnityEngine;
namespace ValheimArisenArcana
{
    public class Spells
    {
            public string Id { get; }
    public string Name { get; }
    public SpellData SpellData {get;}

    public Spells(
        string id,
        string name,
        SpellData spellData)
    {
        Id = id;
        Name = name;
        SpellData = spellData;
    }
    public void setHitData(){}
    }

 // spellAction
 public enum SpellAction
{
    Spell1,
    Spell2,
    Spell3,
    Spell4
}

public struct SpellData
{
    public string SpellName;
    public string SpellPrefab;
    public HitData HitData;
    public float EitrCost;
    public float Damage;
    public SpellType SpellType;

    public SpellData(
        string spellName,
        string spellPrefab,
        HitData hitData,
        float eitrCost,
        float damage,
        SpellType spellType)
    {
        SpellName = spellName;
        SpellPrefab = spellPrefab;
        HitData = hitData;
        EitrCost = eitrCost;
        Damage = damage;
        SpellType = spellType;
    }
    public void setHitData()
        {
            if (SpellType == SpellType.Fire)
            {
                HitData.m_damage.m_fire = Damage;
            }
            if (SpellType == SpellType.Lighting)
            {
                HitData.m_damage.m_lightning = Damage;

            }
            if(SpellType == SpellType.Ice)
            {
                HitData.m_damage.m_frost = Damage;
            }
        }
}
public enum SpellType
    {
        Fire,
        Lighting,
        Ice,
    }

}

