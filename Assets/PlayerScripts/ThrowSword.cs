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
    }
}
