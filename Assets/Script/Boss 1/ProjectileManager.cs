using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //시간 남으면 오브젝트 풀링도 하셈

    public static ProjectileManager instance;
    public GameObject attackPrefebParent;

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }

    public void AttackProjectiles1(AttackSO attackData, GameObject cur)
    {
        Vector3 directionTarget = (attackData.targetTransform.position - cur.transform.position).normalized;
        int numProjectiles = Random.Range(attackData.minProjectiles, attackData.maxProjectiles + 1);

        for (int i = 0; i < numProjectiles; i++)
        {
            // 현재 공격의 각도 계산
            float projectileAngle = attackData.angleBetweenProjectiles * (i - (numProjectiles - 1) / 2f);

            //Quaternion.AngleAxis는 주어진 백터만큼 회전하는 메소드
            //Vector3.up은 y축을 기준으로 회전하라는 뜻
            Quaternion rotation = Quaternion.AngleAxis(projectileAngle, Vector3.up);

            //쿼터니언 회전값과 타겟을바라보는 방향을 곱하기
            Vector3 projectileDirection = rotation * directionTarget;

            GameObject projectile = Instantiate(attackData.AttackPrefab, cur.transform.position, Quaternion.identity);
            projectile.transform.position = new Vector3(cur.transform.position.x, attackData.targetTransform.position.y,
                cur.transform.position.z);

            projectile.transform.parent = attackPrefebParent.transform;
            projectile.transform.rotation = Quaternion.LookRotation(projectileDirection);
        }
    }

    public void AttackProjectiles2(AttackSO attackData, GameObject cur)
    {
        Vector3 directionTarget = (attackData.targetTransform.position - cur.transform.position).normalized;
        int numProjectiles = Random.Range(attackData.minProjectiles, attackData.maxProjectiles + 1);

        for (int i = 0; i < numProjectiles; i++)
        {
            GameObject projectile = Instantiate(attackData.AttackPrefab, cur.transform.position, Quaternion.identity);

            projectile.transform.parent = attackPrefebParent.transform;
            projectile.GetComponent<AttackController2>().Shoot(attackData.targetTransform, cur,attackData);
        }
    }
}