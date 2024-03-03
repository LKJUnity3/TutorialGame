using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject Player;
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;

    public int[] colorList = new int[3];

    public GameObject GameOverObj;
    public GameObject GameClearObj;

    public TMP_Text TimeTxt;
    public TMP_Text BestTimeTxt;

    float time = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        if (Player)
        {

        }
        else
        {

        }
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

}
