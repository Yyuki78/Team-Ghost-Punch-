using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    //Mirror‚ÌCollider‚É‚Â‚¯‚ÄAIsCharging‚ğØ‚è‘Ö‚¦‚é
    public static bool IsCharging = false;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            IsCharging = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            IsCharging = false;
        }
    }
}
