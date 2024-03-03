using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBoss : MonoBehaviour
{
    [Header("look")]
    public Transform target; //�÷��̾�
    public float LookSpeed = 5f;

    protected virtual void Start()
    {
        target = GameManager.instance.Player.transform;
    }

    public void LookTarget()
    {
        //�ٶ󺸴� ���⸸ ��������
        Vector3 targetDirection = (target.position - transform.position).normalized;

        //�ٶ� ������ ���ʹϿ� ������ ���� (Lerp�� ����ϱ� ���ؼ� ���ʹϿ� ���� ���°͵� ����)
        Quaternion SetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, SetRotation, LookSpeed * Time.deltaTime);
    }
}
