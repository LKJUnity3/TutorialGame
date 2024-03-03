using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnDotori : MonoBehaviour
{
    [SerializeField] GameObject dotoriPrefab;
    private bool isNull = false;
    private float time = 5f;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    Animator animator;

    private void Start()
    {
        //DotoriSpawn();
    }
    private void Update()
    {
        if (isNull)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                isNull = false;
                time = 5f;
                //DotoriSpawn();
            }
        }
    }

    public void DotoriSpawn()
    {
        GameObject Clone = Instantiate(dotoriPrefab);
        Clone.transform.position = transform.position;
        Clone.GetComponent<DotoriMove>().Setup(Player,this);
        Clone.GetComponent<Renderer>().material.color = Color.black;
        animator = Clone.GetComponent<Animator>();
    }

    public void DestroyDotori(GameObject gameObject)
    {
        Destroy(gameObject,0.5f);                                       
        isNull = true;
    }

    public bool IsNullReturn()
    {
        return isNull;
    }

}
