using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class HitBoxChange : MonoBehaviour
{
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
        GameObject Clone = Instantiate(CubePrefabs);
        Renderer renderer = Clone.GetComponent<Renderer>();
        switch (index)
        {
            case 0:
                Clone.transform.position = new Vector3 (4, 1, 0);
                renderer.material.color = Color.red;
                break;
            case 1:
                Clone.transform.position = new Vector3 (-4,1,0);
                renderer.material.color = Color.green;
                break;
            case 2:
                Clone.transform.position = new Vector3(0, 1, 4);
                renderer.material.color = Color.blue;
                break;
        }
    }
}
