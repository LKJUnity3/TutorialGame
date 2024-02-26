using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerControl;
    public float speed;
    private float gravity;
    private Vector3 moveDiretion;


    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        gravity = 10f;
        moveDiretion = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl == null) return;//예외처리

        if(playerControl.isGrounded)//플레이어가 땅에 붙어있으면
        {
            moveDiretion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDiretion = playerControl.transform.TransformDirection(moveDiretion);
            moveDiretion *= speed; 
        }
        else
        {
            moveDiretion.y -= gravity * Time.deltaTime;
        }

        playerControl.Move(moveDiretion*Time.deltaTime);
    }
}
