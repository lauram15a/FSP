using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladedWeaponController : WeaponController
{
    [Header("Game objects")]
    [SerializeField] private GameObject stabbingHole;

    [Header("Stabbing parameters")]
    [SerializeField] private float stabbingDistance = 2;

    #region Awake(), Start() and Update
    // Start is called before the first frame update
    void Start()
    {
        base.Start(); // Llama al Start() del padre
        EventManager.current.UpdateBulletsEvent.Invoke(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Attack();
    }

    #endregion

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1")) // Botón izquierdo del ratón por defecto
        {
            StabbingHole();
        }
    }

    private void StabbingHole()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(playerCameraTransform.position, playerCameraTransform.forward, stabbingDistance, hittableLayers);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != owner)
            {
                GameObject stabbingHoleClone = Instantiate(stabbingHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(stabbingHoleClone, 5f);
            }
        }
    }
}
