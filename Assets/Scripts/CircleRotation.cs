using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleRotation : MonoBehaviour
{
    private bool isRot = false;
    private IEnumerator[] coroutines = new IEnumerator[2];

    private void Start()
    {
        coroutines[0] = BasicCirRotation();
        coroutines[1] = NotRegular();
    }

    private IEnumerator BasicCirRotation()
    {
        if (isRot) yield break;

        isRot = true;

        while (!GameManager.Instance.isGameOver)
        {
            for (int i = 0; i < 360; i++)
            {
                if (GameManager.Instance.isGameOver) break;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));
                yield return new WaitForSeconds(Random.Range(0.001f, 0.01f));
            }

            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        }

        isRot = false;
    }

    private IEnumerator NotRegular()
    {
        isRot = true;

        while (!GameManager.Instance.isGameOver)
        {
            for (float i = 0; i < 360f; i += 1f)
            {
                if (GameManager.Instance.isGameOver) break;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));
                yield return new WaitForSeconds(Random.Range(0.007f, 0.0005f));
            }
        }

        isRot = false;
    }

    private IEnumerator Shake()
    {
        isRot = true;
        float time;

        while (!GameManager.Instance.isGameOver)
        {
            for (float i = 0; i < 360f; i += 1f)
            {
                if (GameManager.Instance.isGameOver) break;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));
                if(i>180)
                {
                    time = Random.Range(0.05f, 0.000001f);
                }
                else
                {
                    time = Random.Range(0.05f, 0.000001f);
                }

                yield return new WaitForSeconds(time);
            }
        }

        isRot = false;
    }

    private void OnEnable()
    {
        isRot = false;
        IEnumerator coroutine = /*coroutines[Random.Range(0, coroutines.Length)]*/ NotRegular();
        StopCoroutine(coroutine);
        transform.DOScale(0f, 0f);
        transform.DOScale(1f, 0.3f);
        StartCoroutine(coroutine);
    }
}
