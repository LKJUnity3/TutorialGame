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
    public float dashTime = 0.5f;//������ӽð�
    public float calDashTime;//��ô����ð�
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
        calDashTime += Time.deltaTime;//�ð��� ������
        transform.position += (dashDirection * dashSpeed * Time.deltaTime);//��÷� �̵�

        if(calDashTime > dashTime)//0/5�� ������ Move���·�.
        {
            playerState = PlayerState.Move;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("�۵�");
        //if (context.phase == InputActionPhase.Performed)
        //{
            moveInput = context.ReadValue<Vector2>(); //�Է¹޾Ƽ� ������
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); //���⼳��
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

        dashDirection = moveDirection;//moveDirection ��������
        playerState = PlayerState.Dash;

        calDashTime = 0; //�ʱ�ȭ
    }
}
