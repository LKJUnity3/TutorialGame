using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnDotori : MonoBehaviour
{
    [SerializeField] GameObject dotoriPrefab;
    [SerializeField]
    private Transform Player;
    Animator animator;
    private GameObject dotori;
    public AudioClip Bomb;
    private AudioSource audioSource;

    private void Start()
    {
        Player = GameManager.instance.Player.transform;
        audioSource = GetComponent<AudioSource>();
        //DotoriSpawn();
    }
    public void DotoriSpawn()
    {
        GameObject Clone = Instantiate(dotoriPrefab);
        Clone.transform.position = transform.position;
        Clone.GetComponent<DotoriMove>().Setup(Player,this);
        Clone.GetComponentInChildren<Renderer>().material.color = Color.black;
        animator = Clone.GetComponentInChildren<Animator>();
        dotori = Clone;
        audioSource.PlayOneShot(Bomb);

    }
    public void DestroyDotori(GameObject gameObject)
    {
        Destroy(gameObject,0.5f);
        animator.SetBool("isBomb", true);
    }
    public bool IsNullReturn()
    {
        return dotori == null;
    }
}
