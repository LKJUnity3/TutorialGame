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
    bool isSooting = false;
    public float comebackTime;

    public GameObject swordPrefab;
    public GameObject swordSpawnPoint;
    private GameObject TestPrefab;
    //public ThrowSword throwSword;

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
                if(TestPrefab != null)
                {
                    Debug.Log("돌아와");
                    comebackTime += Time.deltaTime / 5;
                    TestPrefab.transform.position = Vector3.Lerp(TestPrefab.transform.position, transform.position, comebackTime);

                    if (TestPrefab.transform.position == transform.position)
                    {
                        Destroy(TestPrefab);//돌아왔다면 없어져

                        playerState= PlayerState.Move;
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
            comebackTime = 0;
        }
        //else//프리팹이 있으면
        //{
        //        Debug.Log("돌아와");
        //        comebackTime += Time.deltaTime / 5;
        //        TestPrefab.transform.position = Vector3.Lerp(TestPrefab.transform.position, transform.position, comebackTime);
                
        //    if(TestPrefab.transform.position==transform.position)
        //        Destroy(TestPrefab);//돌아왔다면 없어져
        //}
        playerState = PlayerState.Move;
    }

    IEnumerator IBack()
    {
        yield return new WaitForSeconds(0.5f);

        isSooting = true;
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

            StartCoroutine(IBack());//0.5초 후 ctrl다시 누를 수 있음.
        }
    }
}
