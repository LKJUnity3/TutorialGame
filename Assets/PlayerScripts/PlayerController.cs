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
    public PlayerAnimController animController;
    public float comebackTime;

    public GameObject swordPrefab;
    public GameObject swordSpawnPoint;

    public ThrowSword throwSword;

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
        animController = GetComponent<PlayerAnimController>();
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
                
                if(throwSword != null)
                {
                    Debug.Log("���ƿ�");
                    if (isReturnSword == false)//�����̱� ����
                    {
                        //�ڷ�ƾ�� �ѹ��� �۵��ǵ��� ����.
                        isReturnSword = true;
                        playerState = PlayerState.Move;
                        StartCoroutine(ReturnSword());
                        throwSword.BringTheBoss3();
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
        if (throwSword == null)
        {
            animController.ShootSword();
            
            GameObject go = Instantiate(swordPrefab, swordSpawnPoint.transform.position, transform.rotation);//���������ǿ��� �߻�
            throwSword = go.GetComponent<ThrowSword>();
            comebackTime = 0;
        }
        playerState = PlayerState.Move;
    }

    IEnumerator ReturnSword()
    {
        throwSword.swordSpeed = 1000f;
        float _distance = Vector3.Distance(throwSword.transform.position, transform.position);//�Ÿ����

        Debug.Log(_distance);

        while (_distance > 1.5f)
        {
            _distance = Vector3.Distance(throwSword.transform.position, transform.position);
            comebackTime += Time.deltaTime / 10;
            throwSword.transform.position = Vector3.Lerp(throwSword.transform.position, transform.position, comebackTime);
            throwSword.transform.rotation = Quaternion.LookRotation(transform.position - throwSword.transform.position);//���ƿö� �÷��̾� ��������
            yield return null;
        }
        animController.ShootSword();
        Destroy(throwSword.gameObject);
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
            animController.Move();
        }

        if (context.canceled)
        {
            animController.Standing();
            animController.ExitMove();
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
