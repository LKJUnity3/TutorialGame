using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DotoriDie : MonoBehaviour
{
    private PlayerController playerController;
    private void OnTriggerEnter(Collider collision)
    {
        playerController = collision.transform.GetComponent<PlayerController>();
        if (playerController != null)
        {
            GameManager.instance.PlayerDie();
        }
    }
}
