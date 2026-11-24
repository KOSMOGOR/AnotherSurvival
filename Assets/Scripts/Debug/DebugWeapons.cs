using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DebugWeapons : MonoBehaviour
{
    public List<WeaponInfo> weapons;
    public List<Item> items;

    [Inject] readonly PlayerFacade _playerFacade;

    void Start()
    {
        weapons = Resources.LoadAll<WeaponInfo>("Weapons").ToList();
        items = Resources.LoadAll<Item>("Items").ToList();
    }

    void Update()
    {
        foreach (PlayerWeapons.WeaponRuntimeInfo wri in _playerFacade.PlayerWeapons.WeaponRuntimeInfos)
        {
            if (wri.weaponInstance is not TargetInAngleWeapon weapon) continue;
            foreach (IEnemy enemy in weapon.Targets)
            {
                if (enemy is not MonoBehaviour monoEnemy || monoEnemy != null) Debug.DrawRay(_playerFacade.Position, enemy.Position - _playerFacade.Position);
            }
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.Width(100f));
        {
            foreach (WeaponInfo weaponInfo in weapons)
                if (GUILayout.Button(weaponInfo.weaponConfig.Id)) _playerFacade.AddOrUpgradeWeapon(weaponInfo);
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical(GUILayout.Width(100f));
        {
            foreach (Item item in items)
                if (GUILayout.Button(item.name)) _playerFacade.AddOrUpgradeItem(item);
        }
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }
}
