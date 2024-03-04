using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattan2 : LookBoss
{
    [SerializeField] AttackSO data;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Transform[] targetPos; 
    int _index = 0;

    private Animator animator;
    private Rigidbody _rigidbody;
    private bool MoveOn;

    AudioSource audioSource;
    public AudioClip audioBossDie2;

    protected override void Start()
    {
        base.Start();

        MoveOn = true;
        data.targetTransform = target;
        StartCoroutine(Move());
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        for (int i=0; i<GameManager.instance.targetPos.Length;i++)
        {
            targetPos[i] = GameManager.instance.targetPos[i];
        }
    }


    private void FixedUpdate()
    {
        LookTarget();

        if (Input.GetKeyDown(KeyCode.E))
        {
            BossDie();
        }
    }


    IEnumerator Move()
    {
        while (MoveOn)
        {
            yield return new WaitForFixedUpdate();
            Vector3 targetPosition = targetPos[_index].transform.position;
            targetPosition.y = transform.position.y; 
            Vector3 direction = (targetPosition - transform.position).normalized;

            //Vector3.Distance는 두점사이의 거리를 나타내는 메소드
            float distance = Vector3.Distance(transform.position, targetPos[_index].transform.position);
            if (distance < 0.5f)
            {
                transform.position = new Vector3(targetPos[_index].position.x, transform.position.y, targetPos[_index].position.z);
                _index++;

                animator.SetTrigger("@Jump");
                ProjectileManager.instance.AttackProjectiles1(data,gameObject);

                if (_index >= targetPos.Length)
                {
                    _index = 0;
                }
                yield return new WaitForSeconds(1.3f);
            }
            else
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }
    }
    public void BossDie()
    {
        MoveOn = false;
        StopCoroutine(Move());
        _rigidbody.velocity = Vector3.zero;
        animator.SetTrigger("Die");
        audioSource.PlayOneShot(audioBossDie2);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            GameManager.instance.PlayerDie();
        }
    }
}
