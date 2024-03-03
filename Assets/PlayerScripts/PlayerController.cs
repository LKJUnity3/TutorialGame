using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public enum PlayerState
{
    None,
    Move,
    Dash,
    Shoot,
}

public class PlayerController : MonoBehaviour
{
    public PlayerAnimController animComtroller;
    public float comebackTime;

    public GameObject swordPrefab;
    public GameObject swordSpawnPoint;
    private GameObject TestPrefab;

    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    private Vector3 dashDirection;
    public float dashTime;//대시지속시간
    public float DashCoolTime;//쿨타임
    public PlayerState playerState;
    public float dashSpeed;

    bool isReturnSword = false;

    void Start()
    {
        playerState = PlayerState.Move;
        DashCoolTime = 2.2f;
        animComtroller = GetComponent<PlayerAnimController>();
    }

    void Update()
    {
        if (DashCoolTime <= 2.2f)
        {
            DashCoolTime += Time.deltaTime;
        }

        switch (playerState)
        {
            //case PlayerState.None:
            //    animComtroller.Standing();
            //    break;
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Dash:
                Dash();
                break;
            case PlayerState.Shoot:
                
                if(TestPrefab != null)
                {
                    Debug.Log("돌아와");
                    if (isReturnSword == false)//움직이기 가능
                    {
                        //코루틴이 한번만 작동되도록 설정.
                        isReturnSword = true;
                        playerState = PlayerState.Move;
                        StartCoroutine(ReturnSword());
                    }
                }
                else
                {
                ShootSword();
                }
                break;
        }
    }

    public void Move()
    {
        if (moveDirection == Vector3.zero)
            return;
        transform.rotation = Quaternion.LookRotation(moveDirection);
        transform.position += (moveDirection * moveSpeed * Time.deltaTime);

        //animComtroller.Move();
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
            animComtroller.ShootSword();

            TestPrefab = Instantiate(swordPrefab, swordSpawnPoint.transform.position, transform.rotation);//스폰포지션에서 발사
            comebackTime = 0;
        }
        playerState = PlayerState.Move;
    }

    IEnumerator ReturnSword()
    {
        float _distance = Vector3.Distance(TestPrefab.transform.position, transform.position);//거리계산

        Debug.Log(_distance);

        while (_distance > 0.7f)
        {
            _distance = Vector3.Distance(TestPrefab.transform.position, transform.position);
            comebackTime += Time.deltaTime / 10;
            TestPrefab.transform.position = Vector3.Lerp(TestPrefab.transform.position, transform.position, comebackTime);
            TestPrefab.transform.rotation = Quaternion.LookRotation(transform.position - TestPrefab.transform.position);//돌아올때 플레이어 방향으로
            yield return new WaitForSeconds(0.01f);//딜레이
        }
        animComtroller.ShootSword();
        Destroy(TestPrefab);
        isReturnSword = false;

        yield break;
    }

    IEnumerator IBack()
    {
        yield return new WaitForSeconds(0.5f);

        //isSooting = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); //입력받아서 움직임
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); //방향설정

        if(context.started)
        {
            animComtroller.Move();
        }

        if (context.canceled)
        {
            animComtroller.Standing();
            animComtroller.ExitMove();
        }
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

            StartCoroutine(IBack());//0.5초 후 ctrl다시 누를 수 있음.
        }
    }
}
