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
            GetComponent<Rigidbody>().velocity = Vector3.zero;//일정시간이 지나면 그자리에서 멈춤
            swordMoveTime = 0;
        }

        //칼방향을 바라보는 방향으로 발사하고 날라가게함.
        Vector3 moveDirection = GetComponent<Rigidbody>().velocity.normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = targetRotation;
        }
    }

    //2초가 됐을떄 방향을 나를 바라보게.
}
