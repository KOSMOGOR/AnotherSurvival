using UnityEngine;
using Zenject;

public class DebugWeapons : MonoBehaviour
{
    public WeaponInfo testWeaponInfo;

    [Inject] PlayerFacade _playerFacade;

    void OnGUI()
    {
        if (GUILayout.Button("Get Weapon"))
        {
            _playerFacade.AddOrUpgradeWeapon(testWeaponInfo);
        }
    }
}
