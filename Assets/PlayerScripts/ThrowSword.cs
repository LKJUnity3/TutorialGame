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

        //게임매니저에 있는 Boss3를 가져와야 함.
        RealBoss = GameManager.instance.Boss3;
        Player = GameManager.instance.Player;

    }

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
    private void OnTriggerEnter(Collider other)
    {
        boss = other.GetComponent<BossPattan3>();

        if (boss != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;//그자리에서 멈춤
            //StartCoroutine(BringTheBoss());
        }
    }

    IEnumerator BringTheBoss()
    {
        float _distance = Vector3.Distance(RealBoss.transform.position, Player.transform.position);//보스와 플레이어의 거리계산

        while (_distance > 2.5f)
        {
            _distance = Vector3.Distance(RealBoss.transform.position, Player.transform.position);
            comebackTime += Time.deltaTime / 10;
            RealBoss.transform.position = Vector3.Lerp(RealBoss.transform.position, Player.transform.position, comebackTime);
            RealBoss.transform.rotation = Quaternion.LookRotation(Player.transform.position - RealBoss.transform.position);//돌아올때 플레이어 방향으로
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

    //2초가 됐을떄 방향을 나를 바라보게.
}
