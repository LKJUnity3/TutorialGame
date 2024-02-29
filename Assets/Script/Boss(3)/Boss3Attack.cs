using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Attack : MonoBehaviour
{
    public int Repet = 9;
    public Transform target;
    [SerializeField] AttackSO data;

    private Animator animator;
    SpawnDotori spawnDotori;

    private void Start()
    {
        data.targetTransform = target;
        animator = GetComponentInChildren<Animator>();
        spawnDotori = GetComponent<SpawnDotori>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("@Jump");
            if(!spawnDotori.IsNullReturn())
            {
                spawnDotori.DotoriSpawn();
            }
            else
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {

        float rand = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand);
        int i = 0;
        while(i < Repet)
        {
            ProjectileManager.instance.AttackProjectiles2(data, gameObject);
            i++;
        }
    }


}
