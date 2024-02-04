using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Game object")]
    [SerializeField] private GameObject bulletHole;

    [Header("Layers")]
    [SerializeField] private LayerMask hittableLayers;

    [Header("Transform")]
    [SerializeField] private Transform weaponNozzle;    
    private Transform playerCameraTransform;

    [Header("Positions")]
    [SerializeField] private Vector3 initialPosition;

    [Header("Shoot parameters")]
    [SerializeField] private float fireDistance = 200;
    [SerializeField] private float fireRate = 0.6f;    // intervalo entre cada disparo
    [SerializeField] private float recoilForce = 4f;
    [SerializeField] private int maxNumAmmo = 8;
    [SerializeField] public int currentNumAmmo { get; private set; }
    [SerializeField] private float lastTimeShoot = Mathf.NegativeInfinity;

    [Header("Sound and visual effects")]
    [SerializeField] private GameObject flashEffect;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        playerCameraTransform = GameObject.FindGameObjectWithTag("PlayerCamera").transform;

        currentNumAmmo = 8;
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

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 5f);
    }

    private void HandleShoot()
    {
        Flash();
        Recoil();
        BulletHole();

        currentNumAmmo--;
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
        RaycastHit hit;

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, fireDistance, hittableLayers))
        {
            GameObject bulletHoleClone = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
            Destroy(bulletHoleClone, 5f);
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

        return shoot;
    }
}
