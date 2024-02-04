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

    private void OnEnable()
    {
        EventManager.current.UpdateBulletsEvent.AddListener(UpdateInfoAmmo);
    }

    private void OnDisable()
    {
        EventManager.current.UpdateBulletsEvent.RemoveListener(UpdateInfoAmmo);
    }

    private void UpdateInfoAmmo(int newCurrentNumAmmo, int newMaxNumAmmo, int newTotalNumAmmo)
    {
        UpdateTextsAmmo(newCurrentNumAmmo, newMaxNumAmmo, newTotalNumAmmo);
        UpdateColorAmmo(newCurrentNumAmmo, newTotalNumAmmo);
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
            textCurrentNumAmmo.color = Color.white;
        }

        if (newTotalNumAmmo <= 0)
        {
            textTotalNumAmmo.color = new Color(1, 0, 0);
        }
        else
        {
            textTotalNumAmmo.color = Color.white;
        }
    }

}
