using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�����浹");
        Destroy(gameObject, 1f);
    }
}
