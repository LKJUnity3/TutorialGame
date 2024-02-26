using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPattan1 : MonoBehaviour
{
    [Header("look")]
    public Transform target; //플레이어
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
        //바라보는 방향만 가져오기
        Vector3 targetDirection = (target.position - transform.position).normalized;

        //바라볼 방향을 쿼터니온 변수에 저장 (Lerp을 사용하기 위해서 쿼터니온 변수 쓰는것도 있음)
        Quaternion SetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, SetRotation, LookSpeed * Time.deltaTime);
    }


    IEnumerator Move()
    {

        while(true)
        {
            if (false)//탈출할 예정이 있으면 이거 사용
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
