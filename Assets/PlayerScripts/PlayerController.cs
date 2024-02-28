using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SocialPlatforms.Impl;

public enum PlayerState
{
    None,
    Move,
    Dash,
    Shoot,
}

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject swordSpawnPoint;
    public GameObject TestPrefab;

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
            case PlayerState.Shoot:
                ShootSword();
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

        if (DashCoolTime > dashTime)//�ð� ������ Move���·�.
        {
            playerState = PlayerState.Move;
        }
    }


    public void ShootSword()
    {
        if (TestPrefab == null)
        {
            TestPrefab = Instantiate(swordPrefab, swordSpawnPoint.transform.position, transform.rotation);//���������ǿ��� �߻�
        }
        else
        {

        }

        playerState = PlayerState.Move;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); //�Է¹޾Ƽ� ������
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); //���⼳��
    }

    public void OnDash(InputAction.CallbackContext context)
    {
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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("��ô");
            if (playerState == PlayerState.Dash)//Dash�����϶� ��ô�Ұ�
                return;
            playerState = PlayerState.Shoot;
        }

    }
}
