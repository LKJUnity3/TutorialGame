using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattan2 : LookBoss
{
    public float jumpForce = 10f;
    public float moveForce = 5f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        LookTarget();
    }


    void Jump()
    {
        Vector3 minusForward = -transform.forward;
    }
}
