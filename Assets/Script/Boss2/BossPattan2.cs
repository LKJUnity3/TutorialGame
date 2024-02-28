using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattan2 : LookBoss
{
    public float jumpForce = 15f;
    public float moveForce = 5f;
    public float targetDistanse = 40f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(Move());
    }

    void FixedUpdate()
    {
        LookTarget();
    }


    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            Jump();
        }
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
        if (distance > targetDistanse)
        {
            return (transform.forward + randomSet).normalized * moveForce;
        }
        else
        {
            return (-transform.forward + randomSet).normalized * moveForce;
        }
    }
}
