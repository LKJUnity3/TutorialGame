using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //�ð� ������ ������Ʈ Ǯ���� �ϼ�

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
            // ���� ������ ���� ���
            float projectileAngle = attackData.angleBetweenProjectiles * (i - (numProjectiles - 1) / 2f);

            //Quaternion.AngleAxis�� �־��� ���͸�ŭ ȸ���ϴ� �޼ҵ�
            //Vector3.up�� y���� �������� ȸ���϶�� ��
            Quaternion rotation = Quaternion.AngleAxis(projectileAngle, Vector3.up);

            //���ʹϾ� ȸ������ Ÿ�����ٶ󺸴� ������ ���ϱ�
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