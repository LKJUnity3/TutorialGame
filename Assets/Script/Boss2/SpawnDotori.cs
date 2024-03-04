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
    Animator animator;

    GameObject _dotoryParent;

    private void Start()
    {
        _dotoryParent = GameManager.instance.DotorySpwan;
        Player = GameManager.instance.Player.transform;
        //DotoriSpawn();
    }
    private void Update()
    {
        if (_dotoryParent.transform.childCount == 0)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                isNull = false;
                time = 5f;
                //DotoriSpawn();
            }
            else
            {
                isNull = true;
            }
        }
    }

    public void DotoriSpawn()
    {
        GameObject Clone = Instantiate(dotoriPrefab);
        Clone.transform.position = transform.position;
        Clone.GetComponent<DotoriMove>().Setup(Player,this);
        Clone.GetComponentInChildren<Renderer>().material.color = Color.black;
        Clone.transform.parent = _dotoryParent.transform;
        animator = Clone.GetComponentInChildren<Animator>();
    }

    public void DestroyDotori(GameObject gameObject)
    {
        Destroy(gameObject,0.5f);
        animator.SetBool("isBomb", true);
        //isNull = true;
    }

    public bool IsNullReturn()
    {
        return isNull;
    }

}
