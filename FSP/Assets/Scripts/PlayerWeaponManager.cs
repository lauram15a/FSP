using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("Weapons list")]
    [SerializeField] List<WeaponController> startWeapons = new List<WeaponController>();

    [Header("Positions")]
    [SerializeField] private Transform weaponParentSocket;          // dónde vamos a colocar nuestras armas
    [SerializeField] private Transform defaultWeaponPosition;       // posición inicial por defecto de nuestras armas
    [SerializeField] private Transform aimingPosition;              // posición por defecto cuando estemos apuntando con nuestras armas

    [Header("Weapons array")]
    [SerializeField] private int activeWeaponIndex;                 // cuál es el arma activa del jugador
    [SerializeField] private WeaponController[] weaponSlots = new WeaponController[4];   // sabemos cuántas armas max va a tener el player. Va a ser fijo.
 

    // Start is called before the first frame update
    void Start()
    {
        activeWeaponIndex = -1; // el player empieza sin armas

        foreach (WeaponController startingWeapon in startWeapons)
        {
            AddWeapon(startingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SwitchWeapon(0);
        }
    }

    /// <summary>
    /// Add weapon in the weapons list.
    /// </summary>
    /// <param name="weapon"></param>
    private void AddWeapon(WeaponController p_weaponPrefab)
    {
        weaponParentSocket.position = defaultWeaponPosition.position;

        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null) 
            {
                WeaponController weaponClone = Instantiate(p_weaponPrefab, weaponParentSocket);
                weaponClone.SetOwner(gameObject);
                weaponClone.gameObject.SetActive(false);

                weaponSlots[i] = weaponClone;
                return;
            }
        }
    }

    /// <summary>
    /// Switch tha weapon that the player is using
    /// </summary>
    /// <param name="p_weaponIndex"></param>
    private void SwitchWeapon(int p_weaponIndex)
    {
        if ((p_weaponIndex != activeWeaponIndex) && (p_weaponIndex >= 0))
        {
            weaponSlots[p_weaponIndex].gameObject.SetActive(true);
            activeWeaponIndex = p_weaponIndex;
        }
    }
}
