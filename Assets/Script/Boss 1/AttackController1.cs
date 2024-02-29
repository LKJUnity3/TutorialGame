using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController1 : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public AttackSO data;
    public float _currentDuration = 5;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _currentDuration -= Time.deltaTime;

        if(_currentDuration<=0)
        {
            _currentDuration = 0;

            //시간남으면 오브젝트 pool 하자
            Destroy(gameObject);
        }

        _rigidbody.velocity = transform.forward * data.speed;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            //딜
            Destroy(gameObject);
        }
        //시간남으면 오브젝트 벽에 부딪치는거까지
    }


}
