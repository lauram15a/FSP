using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private LayerMask hittableLayers;

    private Transform playerCameraTransform;
    
    private float fireDistance;             
    

    // Start is called before the first frame update
    void Start()
    {
        playerCameraTransform = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
        fireDistance = 200;
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, fireDistance, hittableLayers))
            {
                GameObject bulletHoleClone = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHoleClone, 5f);
            }
        }
    }
}
