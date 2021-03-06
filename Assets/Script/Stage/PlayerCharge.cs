using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    //MirrorのColliderにつけて、IsChargingを切り替える
    public static bool IsCharging = false;

    //MirrorManagerで使用
    public bool FinishArea = false;
    public bool ChargeTime = false;

    private void Start()
    {
        IsCharging = false;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //if (MaxChargeTime > 0)
            //{
                IsCharging = true;
                ChargeTime = true;
                //MaxChargeTime--;
            //}
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            IsCharging = false;
            ChargeTime = false;
            if (FinishArea ==true)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
