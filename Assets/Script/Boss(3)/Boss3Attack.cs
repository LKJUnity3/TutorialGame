using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Attack : MonoBehaviour
{
    public int Repet = 8;
    [SerializeField] AttackSO data;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.6f);
        int i = 0;
        while(i < Repet)
        {
            ProjectileManager.instance.AttackProjectiles2(data, gameObject);
            i++;
        }
    }


}
