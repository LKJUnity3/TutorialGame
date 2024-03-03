using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBoss : MonoBehaviour
{
    [Header("look")]
    public Transform target; //플레이어
    public float LookSpeed = 5f;

    protected virtual void Start()
    {
        target = GameManager.instance.Player.transform;
    }

    public void LookTarget()
    {
        //바라보는 방향만 가져오기
        Vector3 targetDirection = (target.position - transform.position).normalized;

        //바라볼 방향을 쿼터니온 변수에 저장 (Lerp을 사용하기 위해서 쿼터니온 변수 쓰는것도 있음)
        Quaternion SetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, SetRotation, LookSpeed * Time.deltaTime);
    }
}
