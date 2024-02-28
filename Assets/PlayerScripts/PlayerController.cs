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
    public float dashTime;//������ӽð�
    public float DashCoolTime;//��Ÿ��
    public PlayerState playerState;
    public float dashSpeed;

    void Start()
    {
        playerState = PlayerState.Move;
        DashCoolTime = 2.2f;
    }

    void Update()
    {
        if (DashCoolTime <= 2.2f)
        {
            DashCoolTime += Time.deltaTime;
        }

        switch (playerState)
        {
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Dash:
                Dash();
                break;
        }
    }

    public void Move()
    {
        if (moveDirection == Vector3.zero)
            return;
        transform.rotation = Quaternion.LookRotation(moveDirection);
        transform.position += (moveDirection * moveSpeed * Time.deltaTime);
    }

    public void Dash()
    {
        //DashCoolTime += Time.deltaTime;//�ð��� ������
        transform.position += (dashDirection * dashSpeed * Time.deltaTime);//��÷� �̵�

        if(DashCoolTime > dashTime)//�ð� ������ Move���·�.
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
        Debug.Log("���");
        if (playerState == PlayerState.Dash)
        {
            return;
        }

        dashDirection = moveDirection;//moveDirection ��������
        playerState = PlayerState.Dash;

        if (DashCoolTime >= 2.2f)
        {
            DashCoolTime = 0; //�ʱ�ȭ
        }
    }
}
