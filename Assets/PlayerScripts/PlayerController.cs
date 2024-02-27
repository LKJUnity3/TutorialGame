using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += (moveDirection * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("작동");
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>(); //입력받아서 움직임
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); //방향설정
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
            moveDirection = Vector3.zero;   
        }
    }
}
