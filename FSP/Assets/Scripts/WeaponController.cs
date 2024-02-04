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
    [SerializeField] private Transform weaponMuzzle;    // --> weaponNozzle
    private Transform playerCameraTransform;

    [Header("Shoot parameters")]
    private float fireDistance;
    private float recoilForce;
    private Vector3 initialPosition;

    [Header("Sound and visual effects")]
    [SerializeField] private GameObject flashEffect;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        playerCameraTransform = GameObject.FindGameObjectWithTag("PlayerCamera").transform;

        fireDistance = 200;
        recoilForce = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 5f);
    }

    private void HandleShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Flash();
            Recoil();

            RaycastHit hit;

            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, fireDistance, hittableLayers))
            {
                GameObject bulletHoleClone = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHoleClone, 5f);
            }
        }
    }

    private void Flash()
    {
        GameObject flashClone = Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
        Destroy(flashClone, 1f);
    }

    private void Recoil()
    {
        transform.Rotate(-recoilForce, 0, 0);
        transform.position = transform.position - transform.forward * (recoilForce / 50f);
    }

    
}
