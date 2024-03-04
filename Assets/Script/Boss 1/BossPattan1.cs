using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattan1 : LookBoss
{
    [SerializeField] AttackSO data;


    protected override void Start()
    {
        base.Start();
        data.targetTransform = target;
        StartCoroutine(Attack());
    }

    private void FixedUpdate()
    {
        LookTarget();

    }

    IEnumerator Attack()
    {
        {
            while(true)
            {
                yield return new WaitForSeconds(3f);
                ProjectileManager.instance.AttackProjectiles1(data, gameObject);
            }
        }
    }
}
