using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    public void DestroyBoss()
    {
        GameManager.instance.DestroyBoss(transform.parent.gameObject);
    }
}
