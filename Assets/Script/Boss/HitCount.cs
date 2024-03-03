using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitCount : MonoBehaviour
{
    public int state;
    private ThrowSword weapon;
    private void OnTriggerEnter(Collider other)
    {
        weapon = other.GetComponent<ThrowSword>();
        if (weapon != null)
        {
            BossManager.instance.CheckColorCount(state);
        }
    }

    
}
