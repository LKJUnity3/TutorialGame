using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    public float Speed = 0.125f;
    Scene SceneName;

    public float minusZ = 30f;

    private void Start()
    {
        playerTransform = GameManager.instance.Player.transform;
        SceneName = SceneManager.GetActiveScene();
    }

    void FixedUpdate()
    {
        Vector3 PlayerPosition = playerTransform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, PlayerPosition, Speed);
        if (SceneName.name == "MainScene")
        {
            Debug.Log("º¸½º ¾À");
            smoothedPosition.y = transform.position.y;


            smoothedPosition.z = smoothedPosition.z - minusZ;
            transform.position = smoothedPosition;
            return;
        }
        smoothedPosition.y = transform.position.y;
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}