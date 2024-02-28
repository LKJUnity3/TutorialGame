using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotoriColor : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform Boss;
    private Vector3 BossTrans;
    private float time;
    private void Start()
    {
        BossTrans = Boss.position;
        target.position = new Vector3(target.position.x, 0, target.position.z);
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
    }
    
    private void Setup(Transform player)
    {
        Boss = transform.parent.transform;
        target = player;
        
    }

    private void Update()
    {
        if ( Boss != null)
        {
            time += Time.deltaTime / 2;
            Vector3 v = Vector3.Lerp(BossTrans, target.position, time);
            transform.position = v;
        }
    }


}
