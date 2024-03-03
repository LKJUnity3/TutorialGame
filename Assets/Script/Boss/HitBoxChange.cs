using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class HitBoxChange : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints = new Transform[3];
    [SerializeField]
    private GameObject CubePrefabs;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnCube(i);
        }
    }

    private void SpawnCube(int index)
    {
        GameObject Clone = Instantiate(CubePrefabs, spawnPoints[index]);
        Renderer renderer = Clone.GetComponent<Renderer>();
        Clone.GetComponent<HitCount>().state = index;
        renderer.material.color = ChangeColor(index); 
    }

    private Color ChangeColor(int index)
    {
        return index switch
        {
            0 => Color.red,
            1 => Color.green,
            2 => Color.blue,
            _ => Color.white,
        };
    }
}
