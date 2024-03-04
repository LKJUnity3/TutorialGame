using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class DotoriDie : MonoBehaviour
{
    private PlayerController playerController;
    private BossPattan3 bossPattan3;
    private void OnTriggerEnter(Collider collision)
    {
        playerController = collision.transform.GetComponent<PlayerController>();
        if (playerController != null)
        {
            GameManager.instance.PlayerDie();
        }

        bossPattan3 = collision.GetComponent<BossPattan3>();
        if (bossPattan3 != null)
        {
            // ���� ���丮 �浹��
        }
        
    }
}
