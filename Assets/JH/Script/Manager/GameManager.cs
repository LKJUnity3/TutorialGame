using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController controller;

    public GameObject Player;
    public GameObject[] Boss = new GameObject[3];
    public int bossSequence= 0;

    public int[] colorList = new int[3];

    public GameObject GameOverObj;
    public GameObject GameClearObj;

    public TMP_Text TimeTxt;
    public TMP_Text BestTimeTxt;

    //[HideInInspector]
    public GameObject Boss3;

    float time = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Player)
        {

        }
        else
        {

        }
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
    private string SetTime(int time)
    {
        string min = (time / 60).ToString();

        if (int.Parse(min) < 10)
        {
            min = "0" + min;
        }

        string sec = (time % 60).ToString();

        if (int.Parse(sec) < 10)
        {
            sec = "0" + sec;
        }

        return min + " : " + sec;
    }

    private void SetBestTime()
    {
        if (PlayerPrefs.HasKey("BEST"))
        {
            int bestTime = PlayerPrefs.GetInt("BEST");

            if ((int)time > bestTime)
            {
                PlayerPrefs.SetInt("BEST", bestTime = (int)time);
            }

            BestTimeTxt.text = SetTime(bestTime);
        }
        else
        {
            PlayerPrefs.SetInt("BEST", (int)time);
            BestTimeTxt.text = SetTime((int)time);
        }
    }

    public void GameOver()
    {
        GameOverObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameClear()
    {
        TimeTxt.text = SetTime((int)time);
        SetBestTime();
        GameClearObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void DestroyBoss(GameObject target)
    {
        Destroy(target);
        bossSequence++;
        SpawnBoss();
    }

    public void SpawnBoss()
    {
        if(bossSequence==2)
        {
            Boss3 = Instantiate(Boss[bossSequence]);
            return;
        }
        Instantiate(Boss[bossSequence]);
    }

    public void DestroyCube(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    void PlayerDie()
    {
        if (controller.playerState == PlayerState.Dash)
        {
            return;
        }

        // 1. 보스랑 충돌하면 죽음
        // 2. 클리어 못하면 죽음
        // 보스 or 파티클에 충돌하면 죽음
    }

}
