using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public GameObject colorSpherePrefab;
    public int[] colorList = new int[3];
    public int hitCount = 0;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        Instantiate(colorSpherePrefab);
    }

    public void CheckColorCount(int state)
    {
        if (colorList[hitCount] == state)
        {
            hitCount++;
            Debug.Log("True");
        }
        else
        {
            hitCount = 0;
            Debug.Log("False");
            Instantiate(colorSpherePrefab);
        }
    }
    
}
