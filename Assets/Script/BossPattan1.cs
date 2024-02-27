using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattan1 : MonoBehaviour
{
    [SerializeField] AttackSO data;
    [Header("look")]
    public Transform target; //�÷��̾�
    public float LookSpeed = 5f;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Transform[] targetPos; 
    int _index = 0;


    void Start()
    {
        data.targetTransform = target;
        StartCoroutine(Move());
    }


    private void FixedUpdate()
    {
        LookTarget();
    }

    void LookTarget()
    {
        //�ٶ󺸴� ���⸸ ��������
        Vector3 targetDirection = (data.targetTransform.position - transform.position).normalized;

        //�ٶ� ������ ���ʹϿ� ������ ���� (Lerp�� ����ϱ� ���ؼ� ���ʹϿ� ���� ���°͵� ����)
        Quaternion SetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, SetRotation, LookSpeed * Time.deltaTime);
    }


    IEnumerator Move()
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();
            Vector3 targetPosition = targetPos[_index].transform.position;
            targetPosition.y = transform.position.y; 
            Vector3 direction = (targetPosition - transform.position).normalized;

            //Vector3.Distance�� ���������� �Ÿ��� ��Ÿ���� �޼ҵ�
            float distance = Vector3.Distance(transform.position, targetPos[_index].transform.position);
            if (distance < 0.5f)
            {
                transform.position = new Vector3(targetPos[_index].position.x, transform.position.y, targetPos[_index].position.z);
                _index++;

                ProjectileManager.instance.AttackProjectiles(data,gameObject);

                if (_index >= targetPos.Length)
                {
                    _index = 0;
                }
                yield return new WaitForSeconds(1.3f);
            }
            else
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }
    }
}
