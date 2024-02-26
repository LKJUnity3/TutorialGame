using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPattan1 : MonoBehaviour
{
    [Header("look")]
    public Transform target; //�÷��̾�
    public float LookSpeed = 5f;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Transform[] targetPos; 
    int _index = 0;


    void Start()
    {
        StartCoroutine(Move());
    }


    private void FixedUpdate()
    {
        LookTarget();
    }

    void LookTarget()
    {
        //�ٶ󺸴� ���⸸ ��������
        Vector3 targetDirection = (target.position - transform.position).normalized;

        //�ٶ� ������ ���ʹϿ� ������ ���� (Lerp�� ����ϱ� ���ؼ� ���ʹϿ� ���� ���°͵� ����)
        Quaternion SetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, SetRotation, LookSpeed * Time.deltaTime);
    }


    IEnumerator Move()
    {

        while(true)
        {
            if (false)//Ż���� ������ ������ �̰� ���
            {
                break;
            }

            yield return new WaitForFixedUpdate();
            Vector3 direction = (targetPos[_index].transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, targetPos[_index].transform.position);


            if (distance > 0.1f)
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                gameObject.transform.position = targetPos[_index].position;
                _index++;

                if (_index >= targetPos.Length)
                {
                    _index = 0;
                }
                yield return new WaitForSeconds(1f);
            }


        }

    }




}
