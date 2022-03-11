using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    //MirrorのColliderにつけて、IsChargingを切り替える
    public static bool IsCharging = false;

    [SerializeField] GameObject Area;
    private int MaxChargeTime = 2;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (MaxChargeTime > 0)
            {
                IsCharging = true;
                MaxChargeTime--;
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            IsCharging = false;
            if (MaxChargeTime <= 0)
            {
                Area.SetActive(false);
            }
        }
    }
}
