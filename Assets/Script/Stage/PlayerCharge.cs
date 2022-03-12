using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    //Mirror��Collider�ɂ��āAIsCharging��؂�ւ���
    public static bool IsCharging = false;

    //MirrorManager�Ŏg�p
    public bool FinishArea = false;
    public bool ChargeTime = false;

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
