using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneController.entering == false)
        {
            Move.instance.gameObject.transform.position = new Vector3(0, 2, -21);
        }
        else
        {
            Move.instance.gameObject.transform.position = transform.position;
        }

    }

}
