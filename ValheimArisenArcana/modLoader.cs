using System;
using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
namespace ValheimArisenArcana
{
[BepInPlugin("com.dawud.ArisenMagicMod", "Valheim Arisen Magic Mod", "1.0.0")]
    public class modLoader : BaseUnityPlugin
    {
       
        private void Awake()
        {
            PrefabManager.OnVanillaPrefabsAvailable += AddMagicStaff;
        }
        private void Update(){}
        private void AddMagicStaff()
        {
 CustomItem customItem = new CustomItem(
                "ArisenMagicStaff",
                "StaffIceShards"
            );

            MagicStaff magicStaff =
                customItem.ItemPrefab.AddComponent<MagicStaff>();

            magicStaff.Initialize();

            ItemManager.Instance.AddItem(customItem);

            Logger.LogInfo("Arisen Magic Staff registered!");
            PrefabManager.OnVanillaPrefabsAvailable -= AddMagicStaff;
        }
    }
}