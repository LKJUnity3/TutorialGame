using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : MonoBehaviour
{
    public float swordDamage = 1f;
    public float swordSpeed=1000f;
    public float swordMoveTime;
    public float swordMoveStopTime=2f;

    //public float comebackTime;


    // Start is called before the first frame update
    void Start()
    {
        swordMoveTime = 0;
        GetComponent<Rigidbody>().AddForce(transform.forward*swordSpeed);
        //gameObject.transform.rotation = Quaternion.Euler(90, 0, -180);

    }

    // Update is called once per frame
    void Update()
    {
        swordMoveTime += Time.deltaTime;
        if( swordMoveTime > swordMoveStopTime)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;//�����ð��� ������ ���ڸ����� ����
            swordMoveTime = 0;
        }

        //Į������ �ٶ󺸴� �������� �߻��ϰ� ���󰡰���.
        Vector3 moveDirection = GetComponent<Rigidbody>().velocity.normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = targetRotation;
        }
    }

    //2�ʰ� ������ ������ ���� �ٶ󺸰�.
}
