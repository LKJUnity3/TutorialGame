using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    
    [SerializeField]
    private GameObject Boss;
    public static BossManager instance;
    public GameObject colorSpherePrefab;
    public int[] colorList = new int[3];
    public int hitCount = 0;

    private List<GameObject> cube = new List<GameObject>();

    public Animator animator;

    AudioSource audioSource;
    public AudioClip audioBossDie1;

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
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckColorCount(int state)
    {
         if (colorList[hitCount] == state)
        {
            hitCount++;
            Debug.Log("True");
            if (hitCount == 3)
            {
                animator.SetTrigger("Die");
                audioSource.PlayOneShot(audioBossDie1);
                for (int i = 0; i < cube.Count; i++)
                {
                    GameManager.instance.DestroyCube(cube[i]);
                }
                cube.Clear();
            }
        }
        else
        {
            hitCount = 0;
            Debug.Log("False");
            SpawnDotoriPrefab();
        }

    }

    public void GetCube(GameObject game)
    {
        cube.Add(game);
    }


    private void SpawnDotoriPrefab()
    {
        GameObject Clone = Instantiate(colorSpherePrefab);
        Clone.transform.position = new Vector3(0, 8, 0);
    }
}
