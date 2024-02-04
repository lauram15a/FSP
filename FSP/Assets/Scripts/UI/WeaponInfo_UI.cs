using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class WeaponInfo_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text textCurrentNumAmmo;
    [SerializeField] private TMP_Text textMaxNumAmmo;
    [SerializeField] private TMP_Text textTotalNumAmmo;
    [SerializeField] private TMP_Text textRecharging;

    private void OnEnable()
    {
        EventManager.current.UpdateBulletsEvent.AddListener(UpdateInfoAmmo);
        EventManager.current.UpdateRechargingEvent.AddListener(UpdateInfoRecharging);
    }

    private void OnDisable()
    {
        EventManager.current.UpdateBulletsEvent.RemoveListener(UpdateInfoAmmo);
        EventManager.current.UpdateRechargingEvent.RemoveListener(UpdateInfoRecharging);
    }

    private void UpdateInfoAmmo(int newCurrentNumAmmo, int newMaxNumAmmo, int newTotalNumAmmo)
    {
        UpdateTextsAmmo(newCurrentNumAmmo, newMaxNumAmmo, newTotalNumAmmo);
        UpdateColorAmmo(newCurrentNumAmmo, newTotalNumAmmo);
    }

    private void UpdateInfoRecharging(string newTextRecharging)
    {
        UpdateTextRecharging(newTextRecharging);
        UpdateColorRecharging(newTextRecharging);
    }

    private void UpdateTextsAmmo(int newCurrentNumAmmo, int newMaxNumAmmo, int newTotalNumAmmo)
    {
        textCurrentNumAmmo.text = newCurrentNumAmmo.ToString();
        textMaxNumAmmo.text = newMaxNumAmmo.ToString();
        textTotalNumAmmo.text = newTotalNumAmmo.ToString();
    }

    private void UpdateColorAmmo(int newCurrentNumAmmo, int newTotalNumAmmo)
    {
        if (newCurrentNumAmmo <= 0)
        {
            textCurrentNumAmmo.color = new Color(1, 0, 0);
        }
        else
        {
            textCurrentNumAmmo.color = Color.black;
        }

        if (newTotalNumAmmo <= 0)
        {
            textTotalNumAmmo.color = new Color(1, 0, 0);
        }
        else
        {
            textTotalNumAmmo.color = Color.black;
        }
    }

    private void UpdateTextRecharging(string newTextRecharging)
    {
        textRecharging.text = newTextRecharging;
    }

    private void UpdateColorRecharging(string newTextRecharging)
    {
        if (newTextRecharging == "Recharging...")
        {
            textRecharging.color = Color.gray;
        }
        else if (newTextRecharging == "No more ammo")
        {
            textRecharging.color = Color.red;
        }
        else
        {
            textRecharging.color = Color.green;
        }
    }
}
