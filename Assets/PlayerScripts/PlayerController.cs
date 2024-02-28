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
                if(TestPrefab != null)
                {
                    Debug.Log("���ƿ�");
                    comebackTime += Time.deltaTime / 5;
                    TestPrefab.transform.position = Vector3.Lerp(TestPrefab.transform.position, transform.position, comebackTime);

                    if (TestPrefab.transform.position == transform.position)
                    {
                        Destroy(TestPrefab);//���ƿԴٸ� ������

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
            comebackTime = 0;
        }
        //else//�������� ������
        //{
        //        Debug.Log("���ƿ�");
        //        comebackTime += Time.deltaTime / 5;
        //        TestPrefab.transform.position = Vector3.Lerp(TestPrefab.transform.position, transform.position, comebackTime);
                
        //    if(TestPrefab.transform.position==transform.position)
        //        Destroy(TestPrefab);//���ƿԴٸ� ������
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

            StartCoroutine(IBack());//0.5�� �� ctrl�ٽ� ���� �� ����.
        }
    }
}
