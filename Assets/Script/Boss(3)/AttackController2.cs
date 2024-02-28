using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController2 : MonoBehaviour
{
    public float jumpForce = 30f;
    public float moveForce = 40f;

    public Rigidbody _rigidbody;
    void Start()
    {
        //_rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot(Transform target)
    {
        StartCoroutine(ShootStart(target));
    }

    Vector3 SetForward(Transform target)
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        Vector3 randomSet = new Vector3(randomX, 0f, randomZ);

        float distance = Vector3.Distance(transform.position, target.transform.position);

        return (transform.forward + randomSet).normalized * moveForce;
    }

    public IEnumerator ShootStart(Transform target)
    {

        Debug.Log("¼¼ÆÃ");
        Vector3 minusForward = SetForward(target);
        _rigidbody.AddForce(minusForward, ForceMode.Impulse);
        _rigidbody.AddForce(Vector3.up * moveForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.6f);

        Shoot(target);
    }

}
