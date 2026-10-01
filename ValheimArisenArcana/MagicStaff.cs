using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using BepInEx;
using Jotunn.Entities;
using Jotunn;
using Jotunn.Managers;
namespace ValheimArisenArcana
{
   public class MagicStaff : MonoBehaviour
    {
            public const int MaxLevel = 4;
            public const string prefab = "StaffIceShards";

            public static readonly List<SpellData> spellList = new List<SpellData>
{
    new SpellData(
        "Ice Arrow",
        "staff_iceshard_projectile",
        new HitData(10),
        5f,
        10,
        SpellType.Ice
    ),

    new SpellData(
        "Lightning Bolt",
        "staff_lightning_projectile",
        new HitData(10),
        5f,
        10,
        SpellType.Lighting
    ),

    new SpellData(
        "FireBall",
        "staff_fireball_projectile",
        new HitData(10),
        5f,
        10,
        SpellType.Fire
    )
};

    private readonly Dictionary<SpellAction, Spells> spells = new Dictionary<SpellAction, Spells>();
    public void Initialize()
        {
            Debug.Log("Magic Staff initialized!");
            for (int i = 0; i < spellList.Count; i++)
            {
                initSpells(spellList[i],i);
            }
            
        }
    public int GetLevel(ItemDrop.ItemData item)
    {
        return Mathf.Clamp(item.m_quality, 1, MaxLevel);
    }

    public void BindSpell(SpellAction action, Spells spell)
    {
        spells[action] = spell;
    }

    public Spells GetSpell(SpellAction action)
    {
        spells.TryGetValue(action, out Spells spell);
        return spell;
    }
    public void initSpells(SpellData spellData, int idx)
        {
            GameObject prefab =  PrefabManager.Instance.GetPrefab(spellData.SpellPrefab);
            if (prefab == null)
            {
              return;
            }
            ;
            Spells newSpell = new Spells(spellData.SpellName,spellData.SpellName,spellData);
            SpellAction action = (SpellAction)idx;
            BindSpell(action,newSpell);
        }
    
    public void castSpell(SpellAction action, Player player)
        {
            Spells currSpell = GetSpell(action);
            // implement function to cast
                    GameObject prefab =
            ZNetScene.instance.GetPrefab(currSpell.SpellData.SpellPrefab);
            
            if(prefab == null)
            {
                return;
            }
            ;
            // Spawn slightly in front of and above the player
            Vector3 spawnPosition =
                player.transform.position
                + GameCamera.instance.transform.forward* 1.5f
                + Vector3.up * 1.2f;

            // Make the projectile face where the player is facing
            Quaternion rotation =
                Quaternion.LookRotation(GameCamera.instance.transform.forward);

            Vector3 velocity =
            GameCamera.instance.transform.forward * 30f;
            GameObject projectile = Instantiate(prefab,spawnPosition,rotation);
            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            if(projectileComponent != null)
            {
                projectileComponent.Setup(
                    player,
                    velocity,
                    -1f,
                    currSpell.SpellData.HitData,
                    null,
                    null
                );
            }


        }

    } 
}