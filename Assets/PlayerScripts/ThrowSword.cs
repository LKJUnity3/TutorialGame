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

    public BossPattan1 boss2;
    public BossPattan3 boss3;
    public Mushroom mushroom;

    [SerializeField]
    private MeshRenderer swordVariant;
    [SerializeField]
    private Rigidbody swordRigidbody;

    void Start()
    {
        swordMoveTime = 0;
        swordRigidbody.AddForce(transform.forward*swordSpeed);
        //gameObject.transform.rotation = Quaternion.Euler(90, 0, -180);

        //���ӸŴ����� �ִ� Boss3�� �����;� ��.
        RealBoss = GameManager.instance.CurBoss;
        Player = GameManager.instance.Player;
        
    }

    void Update()
    {
        swordMoveTime += Time.deltaTime;
        if( swordMoveTime > swordMoveStopTime)
        {
            swordRigidbody.velocity = Vector3.zero;//�����ð��� ������ ���ڸ����� ����/ ��ŸƮ
            swordMoveTime = 0;
        }

        //Į������ �ٶ󺸴� �������� �߻��ϰ� ���󰡰���.
        Vector3 moveDirection = swordRigidbody.velocity.normalized;//��ŸƮ
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = targetRotation;
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        boss3 = other.GetComponent<BossPattan3>();
        boss2 = other.GetComponent<BossPattan1>();
        mushroom = other.GetComponent<Mushroom>();

        if (boss3 != null)
        {
            Debug.Log("boss3");
            swordRigidbody.velocity = Vector3.zero;//���ڸ����� ����
            swordVariant.enabled = false;
        }
        if (boss2 != null)
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            swordSpeed = 0;
        }
        if(mushroom != null)
        {
            swordRigidbody.velocity = Vector3.zero;
        }
    }

    IEnumerator BringTheBoss()
    {
        float _distance = Vector3.Distance(RealBoss.transform.position, Player.transform.position);//������ �÷��̾��� �Ÿ����

        while (_distance > 0.5f)
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
        if (boss3 != null)
        {
           StartCoroutine(BringTheBoss());
        }
    }
}
