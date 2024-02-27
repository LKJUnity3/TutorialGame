using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class MaterialColor : MonoBehaviour
{
    private int[] set = new int[3];
    [SerializeField]
    private Renderer rendererColor;

    private void SetColor()
    {
        for (int i = 0; i < set.Length; i++)
        {
            set[i] = Random.Range(0, 3);
        }
    }
    private void Start()
    {
        rendererColor = GetComponent<Renderer>();
        SetColor();
        StartCoroutine("ColorSwitch");
    }

    private void SwitchColor(int index)
    {
        Debug.Log(index);
        switch (index)
        {
            case 0:
                rendererColor.material.color = Color.red; break;
            case 1:
                rendererColor.material.color= Color.green; break;
            case 2:
                rendererColor.material.color= Color.blue; break;
        }
    }

    private IEnumerator ColorSwitch()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        for (int i = 0;i < set.Length;i++)
        {
            SwitchColor(set[i]);
            yield return waitForSeconds;
        }
    }
}
