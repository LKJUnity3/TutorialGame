using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Boss;
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
        SpawnDotoriPrefab();
    }

    public void CheckColorCount(int state)
    {
         if (colorList[hitCount] == state)
        {
            hitCount++;
            Debug.Log("True");
            if (hitCount == 3)
            {
                Destroy(Boss);
            }
        }
        else
        {
            hitCount = 0;
            Debug.Log("False");
            SpawnDotoriPrefab();
        }

    }


    private void SpawnDotoriPrefab()
    {
        GameObject Clone = Instantiate(colorSpherePrefab);
        Clone.transform.position = new Vector3(0, 4, 0);
    }
}
