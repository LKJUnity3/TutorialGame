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
    public float dashTime;//대시지속시간
    public float DashCoolTime;//쿨타임
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
        //DashCoolTime += Time.deltaTime;//시간이 누적됨
        transform.position += (dashDirection * dashSpeed * Time.deltaTime);//대시로 이동

        if (DashCoolTime > dashTime)//시간 지나면 Move상태로.
        {
            playerState = PlayerState.Move;
        }
    }


    public void ShootSword()
    {
        if (TestPrefab == null)
        {
            TestPrefab = Instantiate(swordPrefab, swordSpawnPoint.transform.position, transform.rotation);//스폰포지션에서 발사
        }
        else
        {

        }

        playerState = PlayerState.Move;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); //입력받아서 움직임
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); //방향설정
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (playerState == PlayerState.Dash)
        {
            return;
        }

        dashDirection = moveDirection;//moveDirection 방향으로
        playerState = PlayerState.Dash;

        if (DashCoolTime >= 2.2f)
        {
            DashCoolTime = 0; //초기화
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("투척");
            if (playerState == PlayerState.Dash)//Dash상태일땐 투척불가
                return;
            playerState = PlayerState.Shoot;
        }

    }
}
