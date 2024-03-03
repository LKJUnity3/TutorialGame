using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float Speed = 0.125f;

    void FixedUpdate()
    {
        Vector3 PlayerPosition = playerTransform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, PlayerPosition, Speed);
        smoothedPosition.y = transform.position.y;
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}