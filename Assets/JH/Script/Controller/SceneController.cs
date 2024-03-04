using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    Scene SceneName;

    // bool������ �� �����߾��� ���� ���� �� ��
    // position�� ����
    public static bool entering = false;

    // Start is called before the first frame update
    void Start()
    {
        SceneName = SceneManager.GetActiveScene();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(SceneName.name == "MainScene" && GameObject.Find("Player"))
        {
            SceneManager.LoadScene("BossScene");
        }
        else if(SceneName.name == "BossScene")
        {
            entering = true;
            SceneManager.LoadScene("MainScene");
            Time.timeScale = 1;
        }
    }

    public void GameStartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MoveStartSceneBtn()
    {
        SceneManager.LoadScene("StartScene");
    }

}
