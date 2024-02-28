using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{   
    public GameObject startPoint;

    private void Start()
    {
        transform.position = startPoint.transform.position;
        Debug.Log("¿€µø");
    }
}
