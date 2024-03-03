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
    public float dashTime;//������ӽð�
    public float DashCoolTime;//��Ÿ��
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
                    Debug.Log("���ƿ�");
                    if (isReturnSword == false)//�����̱� ����
                    {
                        //�ڷ�ƾ�� �ѹ��� �۵��ǵ��� ����.
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
            animComtroller.ShootSword();

            TestPrefab = Instantiate(swordPrefab, swordSpawnPoint.transform.position, transform.rotation);//���������ǿ��� �߻�
            comebackTime = 0;
        }
        playerState = PlayerState.Move;
    }

    IEnumerator ReturnSword()
    {
        float _distance = Vector3.Distance(TestPrefab.transform.position, transform.position);//�Ÿ����

        Debug.Log(_distance);

        while (_distance > 0.7f)
        {
            _distance = Vector3.Distance(TestPrefab.transform.position, transform.position);
            comebackTime += Time.deltaTime / 10;
            TestPrefab.transform.position = Vector3.Lerp(TestPrefab.transform.position, transform.position, comebackTime);
            TestPrefab.transform.rotation = Quaternion.LookRotation(transform.position - TestPrefab.transform.position);//���ƿö� �÷��̾� ��������
            yield return new WaitForSeconds(0.01f);//������
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
        moveInput = context.ReadValue<Vector2>(); //�Է¹޾Ƽ� ������
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); //���⼳��

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

            StartCoroutine(IBack());//0.5�� �� ctrl�ٽ� ���� �� ����.
        }
    }
}
