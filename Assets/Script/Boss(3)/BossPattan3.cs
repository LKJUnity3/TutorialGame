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


    private Animator animator;
    private Rigidbody _rigidbody;



    protected override void Start()
    {
        base.Start();

        bombTrue = true;
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            BossDie();
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
        //����� �޼ҵ�

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
        //�ణ�� ������ �ֱ����� ó��
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

    public void BossDie()
    {
        _rigidbody.velocity = Vector3.zero;
        animator.SetTrigger("Die");
    }

    protected void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            GameManager.instance.PlayerDie();
        }
    }
}
