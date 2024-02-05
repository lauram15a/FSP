using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("Weapons list")]
    [SerializeField] List<WeaponController> weaponsList = new List<WeaponController>();    

    [Header("Positions")]
    [SerializeField] private Transform weaponParentSocket;          // donde vamos a colocar nuestras armas
    [SerializeField] private Transform defaultWeaponPosition;       
    [SerializeField] private Transform aimingPosition;              // posición cuando estemos apuntando con nuestras armas

    [Header("Weapons array")]
    [SerializeField] private int activeWeaponIndex;                
    [SerializeField] private WeaponController[] weaponSlots = new WeaponController[5];   
    [SerializeField] private WeaponController currentWeapon;

    #region Start() and Update

    // Start is called before the first frame update
    void Start()
    {
        activeWeaponIndex = -1; // el player empieza sin armas

        foreach (WeaponController weapon in weaponsList)
        {
            AddWeapon(weapon);
        }

        currentWeapon = weaponSlots[0];
    }

    // Update is called once per frame
    void Update()
    {
        ChooseWeapon();
    }

    #endregion

    private void ChooseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponsList.Count > 1)
            {
                SwitchWeapon(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weaponsList.Count > 2)
            {
                SwitchWeapon(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (weaponsList.Count > 3)
            {
                SwitchWeapon(3);
            }
        }
    }

    private void AddWeapon(WeaponController weaponPrefab)
    {
        weaponParentSocket.position = defaultWeaponPosition.position;

        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null) 
            {
                WeaponController weaponClone = Instantiate(weaponPrefab, weaponParentSocket);

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

            EventManager.current.NewWeaponEvent.Invoke();

            if (currentWeapon is FireWeaponController)
            {
                FireWeaponController currentFireWeapon = (FireWeaponController)currentWeapon;
                EventManager.current.UpdateBulletsEvent.Invoke(currentFireWeapon.GetCurrentNumAmmo(), currentFireWeapon.GetMaxNumAmmo(), currentFireWeapon.GetTotalNumAmmo());
            }
        }
    }
}
