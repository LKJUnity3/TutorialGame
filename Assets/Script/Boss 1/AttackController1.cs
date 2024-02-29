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

            //�ð������� ������Ʈ pool ����
            Destroy(gameObject);
        }

        _rigidbody.velocity = transform.forward * data.speed;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            //��
            Destroy(gameObject);
        }
        //�ð������� ������Ʈ ���� �ε�ġ�°ű���
    }


}
