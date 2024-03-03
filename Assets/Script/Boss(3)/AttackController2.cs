using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController2 : MonoBehaviour
{

    public float jumpForce = 30f;
    public float moveForce = 40f;
    public float DestroyTime = 5f;

    public Rigidbody _rigidbody;

    private void Start()
    {
        Invoke("DestroyOn", DestroyTime);
    }
    private void Update()
    {
        Vector3 moveDirection = _rigidbody.velocity.normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = targetRotation;
        }
    }

    public void Shoot(Transform target, GameObject cur,AttackSO data)
    {
        moveForce = Random.Range(data.minMoveForce,data.maxMoveForce);
        _rigidbody.useGravity = true;
        Vector3 minusForward = SetForward(target, cur);
        _rigidbody.AddForce(minusForward, ForceMode.Impulse);
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    Vector3 SetForward(Transform target, GameObject cur)
    {
        float randomX = Random.Range(-0.2f, 0.2f);
        float randomZ = Random.Range(-0.2f, 0.2f);

        Vector3 randomSet = new Vector3(randomX, 0f, randomZ);

        float distance = Vector3.Distance(transform.position, target.transform.position);

        return (cur.transform.forward + randomSet).normalized * moveForce;
    }

    void TriggerOff()
    {
        gameObject.GetComponent<Collider>().isTrigger = false;
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Boss"))
        {
            Invoke("TriggerOff",0.5f);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            DestroyOn();
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                GameManager.instance.PlayerDie();
            }
        }
    }

    private void DestroyOn()
    {
        Destroy(gameObject);
    }

}
