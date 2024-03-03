using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitCount : MonoBehaviour
{
    public int state;
    private Weapon weapon;
    private void OnTriggerEnter(Collider other)
    {
        weapon = other.GetComponent<Weapon>();
        if (weapon != null)
        {
            BossManager.instance.CheckColorCount(state);
        }
    }

    
}
