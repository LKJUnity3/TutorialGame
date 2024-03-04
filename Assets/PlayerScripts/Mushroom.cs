using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public ThrowSword sword;

    private void OnTriggerEnter(Collider other)
    {
        sword = other.GetComponent<ThrowSword>();
        if( sword != null)
        {
        Debug.Log("�����浹");
        Destroy(gameObject, 1f);
        }
    }
}
