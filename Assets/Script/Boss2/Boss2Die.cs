using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Die : MonoBehaviour
{
    BossPattan2 _boss;

    private void Start()
    {
        _boss = GetComponentInParent<BossPattan2>();
    }

    private void OnTriggerEnter(Collider coll )
    {
        ThrowSword sword = coll.gameObject.GetComponent<ThrowSword>();

        if(sword != null)
        {
            Debug.Log("���� ����");
            _boss.BossDie();
        }
    }
}
