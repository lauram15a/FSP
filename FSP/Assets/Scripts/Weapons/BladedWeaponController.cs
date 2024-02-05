using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class BladedWeaponController : WeaponController
{
    [Header("Game objects")]
    [SerializeField] private GameObject stabbingHole;

    [Header("Stabbing parameters")]
    [SerializeField] private float stabbingDistance = 0;

    #region Awake(), Start() and Update
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    private void StabbingHole()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(playerCameraTransform.position, playerCameraTransform.forward, stabbingDistance, hittableLayers);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != owner)
            {
                GameObject bulletHoleClone = Instantiate(stabbingHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(bulletHoleClone, 5f);
            }
        }
    }
}
