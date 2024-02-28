using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattan3 : LookBoss
{
    [Header("Movement")]
    public float jumpForce = 15f;
    public float moveForce = 5f;
    public float MaxtargetDistanse = 20f;
    public float MintargetDistanse = 5f;
    [Header("Bomb")]
    bool bombTrue;


    private Rigidbody _rigidbody;

    void Start()
    {
        bombTrue = true;
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(Move());
    }

    void FixedUpdate()
    {
        LookTarget();

        if(transform.childCount == 0 && bombTrue)
        {
            bombTrue = false;
            Bomb();
        }
    }


    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            Jump();
        }
    }


    IEnumerator Bomb()
    {

        yield return new WaitForSeconds(5f);
        //광재님 메소드

        bombTrue = true;
        
    }


    void Jump()
    {
        Vector3 minusForward = SetForward();
        _rigidbody.AddForce(minusForward, ForceMode.Impulse);
        _rigidbody.AddForce(Vector3.up * moveForce, ForceMode.Impulse);

    }

    Vector3 SetForward()
    {
        //약간의 랜덤값 주기위한 처리
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        Vector3 randomSet = new Vector3(randomX, 0f, randomZ);

        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > MaxtargetDistanse)
        {
            return (transform.forward + randomSet).normalized * moveForce;
        }
        else if(distance< MintargetDistanse)
        {
            return (transform.forward + randomSet).normalized * moveForce;
        }
        else
        {
            return (-transform.forward + randomSet).normalized * moveForce;
        }
    }
}
