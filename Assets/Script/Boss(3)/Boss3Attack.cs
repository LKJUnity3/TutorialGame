using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Attack : MonoBehaviour
{
    public int Repet = 9;
    public Transform target;
    [SerializeField] AttackSO data;


    private void Start()
    {
        data.targetTransform = target;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ãæµ¹");
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        int i = 0;
        while(i < Repet)
        {
            ProjectileManager.instance.AttackProjectiles2(data, gameObject);
            i++;
        }
    }


}
