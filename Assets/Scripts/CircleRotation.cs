using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotation : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(CirRotation());
    }

    private IEnumerator CirRotation()
    {
        while (true)
        {
            for (int i = 0; i < 360; i++)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));
                yield return new WaitForSeconds(Random.Range(0.001f, 0.01f));
            }

            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        }
    }
}
