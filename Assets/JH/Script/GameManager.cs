using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startPoint;
    public GameObject TargetPosition;

    public int[] colorList = new int[3];

    //Scene sceneName = SceneManager.GetActiveScene();

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void ThisScene()
    {
        switch (sceneName.name)
        {
            case "MainScene":
                break;

            case "JHScene":
                transform.position = TargetPosition.transform.position;
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "p" && sceneName.name == "MainScene")
        {
            SceneManager.LoadScene("JHScene");
        }
        else if (other.gameObject.name == "p" && sceneName.name == "JHScene")
        {
            SceneManager.LoadScene("MainScene");
        }
    }*/
}
