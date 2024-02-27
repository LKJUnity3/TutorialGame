using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public enum PlayerState
{
    None,
    Move,
    Dash,
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    private Vector3 dashDirection;
    public float dashTime = 0.5f;//대시지속시간
    public float calDashTime;//대시누적시간
    public PlayerState playerState;
    public float dashSpeed = 8f;

    void Start()
    {
        playerState = PlayerState.Move;
    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Move:
                if (moveDirection == Vector3.zero)
                    return;
                transform.rotation = Quaternion.LookRotation(moveDirection);
                transform.position += (moveDirection * moveSpeed * Time.deltaTime);
                break;
            case PlayerState.Dash:
                Dash();
                break;
        }
    }

    public void Dash()
    {
        calDashTime += Time.deltaTime;//시간이 누적됨
        transform.position += (dashDirection * dashSpeed * Time.deltaTime);//대시로 이동

        if(calDashTime > dashTime)//0/5초 지나면 Move상태로.
        {
            playerState = PlayerState.Move;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("작동");
        //if (context.phase == InputActionPhase.Performed)
        //{
            moveInput = context.ReadValue<Vector2>(); //입력받아서 움직임
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); //방향설정
        //}
        //else if (context.phase == InputActionPhase.Canceled)
        //{
        //    moveInput = Vector2.zero;
        //    moveDirection = Vector3.zero;
        //}
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (playerState == PlayerState.Dash)
        {
            return;
        }

        dashDirection = moveDirection;//moveDirection 방향으로
        playerState = PlayerState.Dash;

        calDashTime = 0; //초기화
    }
}
