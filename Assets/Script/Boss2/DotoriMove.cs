using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DotoriMove : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    private Vector3 startPos, endPos;
    protected float timer;
    protected float timeToFloor;
    private SpawnDotori spawn;
    protected float dotoriDestory;

    public void Setup(Transform transform,SpawnDotori spawn)
    {
        Player = transform;
        this.spawn = spawn;
    }

    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float time)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector3.Lerp(start, end, time);
        return new Vector3(mid.x, f(time) + Mathf.Lerp(start.y, end.y, time), mid.z);
    }

    protected IEnumerator DotoriMoves()
    {
        timer = 0;
        while (transform.position.y >= startPos.y)
        {
            timer += Time.deltaTime / 2;
            Vector3 temPos = Parabola(startPos, endPos,10, timer);
            transform.position = temPos;
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        dotoriDestory -= Time.deltaTime;
        if (dotoriDestory < 0)
        {
            spawn.DestroyDotori(gameObject);
        }
    }

    private void Start()
    {
        dotoriDestory = 4;
        startPos = transform.position;
        endPos = Player.position;
        StartCoroutine(DotoriMoves());
    }
}

