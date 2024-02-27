using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController controller;

    void PlayerDie()
    {
        if (controller.playerState == PlayerState.Dash)
            return;
        
    }


}
