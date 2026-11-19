using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DebugWeapons : MonoBehaviour
{
    public List<WeaponInfo> weapons;

    [Inject] readonly PlayerFacade _playerFacade;

    void Start()
    {
        weapons = Resources.LoadAll<WeaponInfo>("Weapons").ToList();
    }

    void OnGUI()
    {
        foreach (WeaponInfo weaponInfo in weapons)
        {
            if (GUILayout.Button(weaponInfo.weaponConfig.Id)) _playerFacade.AddOrUpgradeWeapon(weaponInfo);
        }
    }
}
