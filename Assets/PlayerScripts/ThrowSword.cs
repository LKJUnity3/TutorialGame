using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowSword : MonoBehaviour
{
    //public float swordDamage = 1f;
    public float swordSpeed=1000f;
    public float swordMoveTime;
    public float swordMoveStopTime=2f;

    public float comebackTime;

    //public float comebackTime;

    public GameObject RealBoss;
    public GameObject Player;

    public BossPattan3 boss;

    void Start()
    {
        swordMoveTime = 0;
        GetComponent<Rigidbody>().AddForce(transform.forward*swordSpeed);
        //gameObject.transform.rotation = Quaternion.Euler(90, 0, -180);

        //���ӸŴ����� �ִ� Boss3�� �����;� ��.
        RealBoss = GameManager.instance.Boss3;
        Player = GameManager.instance.Player;

    }

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
    private void OnTriggerEnter(Collider other)
    {
        boss = other.GetComponent<BossPattan3>();

        if (boss != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;//���ڸ����� ����
            //StartCoroutine(BringTheBoss());
        }
    }

    IEnumerator BringTheBoss()
    {
        float _distance = Vector3.Distance(RealBoss.transform.position, Player.transform.position);//������ �÷��̾��� �Ÿ����

        while (_distance > 2.5f)
        {
            _distance = Vector3.Distance(RealBoss.transform.position, Player.transform.position);
            comebackTime += Time.deltaTime / 10;
            RealBoss.transform.position = Vector3.Lerp(RealBoss.transform.position, Player.transform.position, comebackTime);
            RealBoss.transform.rotation = Quaternion.LookRotation(Player.transform.position - RealBoss.transform.position);//���ƿö� �÷��̾� ��������
            yield return null;
        }
        yield break;
    }

    public void BringTheBoss3()
    {
        if (boss != null)
        {
           StartCoroutine(BringTheBoss());
        }
    }

    //2�ʰ� ������ ������ ���� �ٶ󺸰�.
}
