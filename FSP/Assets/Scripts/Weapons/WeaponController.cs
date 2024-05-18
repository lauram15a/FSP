using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    [Header("Game objects")]
    protected GameObject owner;

    [Header("Layers")]
    [SerializeField] protected LayerMask hittableLayers;

    [Header("Transforms")]
    [SerializeField] protected Transform weaponNozzle;
    protected Transform playerCameraTransform;

    [Header("Positions")]
    [SerializeField] protected Vector3 initialPosition;


    #region Awake(), Start() and Update
    protected void Awake()
    {
        initialPosition = transform.localPosition;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        playerCameraTransform = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 5f);
    }

    #endregion

    #region Getters and setters

    public void SetOwner(GameObject new_owner)
    {
        owner = new_owner;
    }

    #endregion
}
