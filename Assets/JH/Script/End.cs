using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{

    public void EndGameOver()
    {
        GameManager.instance.GameOver();
    }

    public void EndGameClear()
    {
        GameManager.instance.GameClear();
    }
}
