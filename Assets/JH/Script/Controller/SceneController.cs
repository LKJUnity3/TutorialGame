using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    Scene SceneName;

    // bool값으로 나 입장했었다 값을 가져 온 뒤
    // position값 수정
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
