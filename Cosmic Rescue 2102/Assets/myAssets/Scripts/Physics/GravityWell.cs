using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    [Header("Planet Gravity Settings")]
    [Space(10)]
    public float Gravity = 9.81f;
    [Space(10)]
    [Tooltip("Gravitational Influence Based on Distance")]
    public float GI_Dist = 750f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GravityReceiver>())
        {
            other.GetComponent<GravityReceiver>().Gravity = this.GetComponent<GravityWell>().Gravity;
            other.GetComponent<GravityReceiver>().Gravity_Well = this.GetComponent<GravityWell>();
        }

    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.GetComponent<GravityReceiver>())
        {
            other.GetComponent<GravityReceiver>().Gravity = 0f;
            other.GetComponent<GravityReceiver>().Gravity_Well = null;
        }
    }

    public float GetGravity()
    {
        return Gravity;
    }

    public float Get_GI_Dist()
    {
        return GI_Dist;
    }
}
