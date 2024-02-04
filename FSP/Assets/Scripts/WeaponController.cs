using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private GameObject bulletHole;
    private GameObject owner;

    [Header("Layers")]
    [SerializeField] private LayerMask hittableLayers;

    [Header("Transforms")]
    [SerializeField] private Transform weaponNozzle;
    private Transform playerCameraTransform;

    [Header("Positions")]
    [SerializeField] private Vector3 initialPosition;

    [Header("Shoot parameters")]
    [SerializeField] private float fireDistance = 200;
    [SerializeField] private float fireRate = 0.6f;    // intervalo entre cada disparo
    [SerializeField] private float recoilForce = 4f;
    [SerializeField] private float lastTimeShoot = Mathf.NegativeInfinity;

    [Header("Ammo parameters")]
    [SerializeField] private int totalNumAmmo = 100;
    [SerializeField] private int maxNumAmmo = 10;
    [SerializeField] private int currentNumAmmo;

    [Header("Recharge parameters")]
    [SerializeField] private float reloadTime = 1.5f;

    [Header("Sound and visual effects")]
    [SerializeField] private GameObject flashEffect;


    private void Awake()
    {
        initialPosition = transform.localPosition;
        currentNumAmmo = maxNumAmmo;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCameraTransform = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (CanShoot())
            {
                HandleShoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CanReload())
            {
                StartCoroutine(Reload());
            }
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 5f);
    }

    private void HandleShoot()
    {
        Flash();
        Recoil();
        BulletHole();

        currentNumAmmo--;
        Debug.Log("currentNumAmmo: " + currentNumAmmo);
        lastTimeShoot = Time.time;
    }

    private void Flash()
    {
        GameObject flashClone = Instantiate(flashEffect, weaponNozzle.position, Quaternion.Euler(weaponNozzle.forward), transform);
        Destroy(flashClone, 1f);
    }

    private void Recoil()
    {
        transform.Rotate(-recoilForce, 0, 0);
        transform.position = transform.position - transform.forward * (recoilForce / 50f);
    }

    private void BulletHole()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(playerCameraTransform.position, playerCameraTransform.forward, fireDistance, hittableLayers);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != owner)
            {
                GameObject bulletHoleClone = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHoleClone, 5f);
            }
        }
    }

    private bool CanShoot()
    {
        bool shoot = false;

        if (currentNumAmmo > 0)
        {
            if ((lastTimeShoot + fireRate) < Time.time)
            {
                shoot = true;
            }
        }
        else
        {
            Debug.Log("Sin balas en la recámara.");
        }

        return shoot;
    }

    private bool CanReload()
    {
        bool reload = false;

        if (totalNumAmmo > 0)
        {
            reload = true;
        }
        else
        {
            Debug.Log("No te queda más munición");
        }

        return reload;
    }

    private void AmmoReaload()
    {
        if (totalNumAmmo >= maxNumAmmo)
        {
            totalNumAmmo = totalNumAmmo - (maxNumAmmo - currentNumAmmo);
            currentNumAmmo = maxNumAmmo;
        }
        else
        {
            currentNumAmmo = totalNumAmmo;
            totalNumAmmo = totalNumAmmo - currentNumAmmo;
        }
    }

    #region Corrutina

    IEnumerator Reload()
    {
        Debug.Log("Recargando...");
        yield return new WaitForSeconds(reloadTime);
        AmmoReaload();
        Debug.Log("Recargada!");
    }

    #endregion

    #region Getters and setters

    public void SetOwner(GameObject new_owner)
    {
        owner = new_owner;
    }

    #endregion
}
