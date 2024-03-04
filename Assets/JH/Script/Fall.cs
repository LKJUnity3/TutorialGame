using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(GameObject.FindWithTag("Way"))
        {
            GameManager.instance.GameOver();
        }
    }
}
