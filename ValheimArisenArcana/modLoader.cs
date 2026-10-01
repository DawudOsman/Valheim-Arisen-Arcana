using System;
using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
namespace ValheimArisenArcana
{
[BepInPlugin("com.dawud.ArisenMagicMod", "Valheim Arisen Magic Mod", "1.0.0")]
[BepInDependency(Jotunn.Main.ModGuid)]
    public class modLoader : BaseUnityPlugin
    {
       
        private void Awake()
        {
            PrefabManager.OnVanillaPrefabsAvailable += AddMagicStaff;
        }
        private void Update()
        {
            // temp implementationm of local Player
            Player localPlayer = Player.m_localPlayer;
            // check if user casting spell
            bool Blocking = ZInput.GetButton("Block");
            if (Blocking)
            {
                
                Logger.LogInfo($"Block Button held for {ZInput.GetButtonLastPressedTimer("Block")}");
                if(ZInput.GetButtonDown("Attack"))
                {
                    if (localPlayer != null)
                    {
                        ItemDrop.ItemData currWeapon = localPlayer.GetCurrentWeapon();
                        if (currWeapon.m_dropPrefab != null && currWeapon.m_dropPrefab.name == "ArisenMagicStaff")
                        {
                            MagicStaff magicStaff = currWeapon.m_dropPrefab.GetComponent<MagicStaff>();
                            magicStaff.castSpell(SpellAction.Spell1, localPlayer);
                        }
                        ;
                    }
                    ;
                }
                ;
            }
            ;
        }
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