using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public Transform player;
    public GameObject gameOver;
    public GameObject gameClear;
    public TMP_Text TimeNumTxt;
    public TMP_Text BestTimeNumTxt;

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

        //player = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameClear()
    {
        gameClear.SetActive(true);
        Time.timeScale = 0;
    }

}
