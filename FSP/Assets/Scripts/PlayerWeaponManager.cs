using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("Weapons list")]
    [SerializeField] List<WeaponController> startWeapons = new List<WeaponController>();    // la lista de las armas que tiene disponibles. Ahora por ejemplo es len 2

    [Header("Positions")]
    [SerializeField] private Transform weaponParentSocket;          // dónde vamos a colocar nuestras armas
    [SerializeField] private Transform defaultWeaponPosition;       // posición inicial por defecto de nuestras armas
    [SerializeField] private Transform aimingPosition;              // posición por defecto cuando estemos apuntando con nuestras armas

    [Header("Weapons array")]
    [SerializeField] private int activeWeaponIndex;                 // cuál es el arma activa del jugador
    [SerializeField] private WeaponController[] weaponSlots = new WeaponController[4];   // sabemos cuántas armas max va a tener el player. Va a ser fijo.
    [SerializeField] private WeaponController currentWeapon;


    // Start is called before the first frame update
    void Start()
    {
        activeWeaponIndex = -1; // el player empieza sin armas

        foreach (WeaponController startingWeapon in startWeapons)
        {
            AddWeapon(startingWeapon);
        }

        currentWeapon = weaponSlots[0];
    }

    // Update is called once per frame
    void Update()
    {
        ChooseWeapon();
    }

    private void ChooseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (startWeapons.Count > 1)
            {
                SwitchWeapon(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (startWeapons.Count > 2)
            {
                SwitchWeapon(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (startWeapons.Count > 3)
            {
                SwitchWeapon(3);
            }
        }
    }

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

    private void SwitchWeapon(int p_weaponIndex)
    {
        if ((p_weaponIndex != activeWeaponIndex) && (p_weaponIndex >= 0))
        {
            currentWeapon.gameObject.SetActive(false);

            currentWeapon = weaponSlots[p_weaponIndex];

            currentWeapon.gameObject.SetActive(true);
            activeWeaponIndex = p_weaponIndex;

            EventManager.current.NewGunEvent.Invoke();
            EventManager.current.UpdateBulletsEvent.Invoke(currentWeapon.GetCurrentNumAmmo(), currentWeapon.GetMaxNumAmmo(), currentWeapon.GetTotalNumAmmo());
        }
    }
}
