using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOver;
    public GameObject gameClear;
    public TMP_Text timeNumTxt;

    float sec;
    int min;

    public int[] colorList = new int[3];


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
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        sec += Time.deltaTime;
   
        if ((int)sec > 59)
        {
            sec = 0;
            min++;
        }
    }

   public void GameOver()
   {
        gameOver.SetActive(true);
        Time.timeScale = 0;
   }

    public void GameClear()
    {
        timeNumTxt.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        gameClear.SetActive(true);       
        Time.timeScale = 0;
    }
}
