using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public int[] colorList = new int[3];
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    
}
