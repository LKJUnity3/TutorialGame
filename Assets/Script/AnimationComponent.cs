using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private GameObject target;

    private void Start()
    {
        target = transform.parent.gameObject;
    }

    public void DestroyBoss()
    {
        Destroy(target);
    }

}
