using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject weaponInfoPrefab;

    private void Start()
    {
        EventManager.current.NewGunEvent.AddListener(ShowWeaponInfo);
    }

    public void ShowWeaponInfo()
    {
        Instantiate(weaponInfoPrefab, transform);
    }
}
