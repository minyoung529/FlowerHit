using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleRotation : MonoBehaviour
{
    private bool isRot = false;
    private IEnumerator[] coroutines = new IEnumerator[4];

    private void Awake()
    {
        coroutines[0] = BasicCirRotation();
        coroutines[1] = NotRegular();
        coroutines[2] = TriggerMove();
        coroutines[3] = ShakeMove();
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
        float time = Random.Range(0.005f, 0.000000005f);

        while (!GameManager.Instance.isGameOver)
        {
            for (float i = 0; i < 360f; i += 1f)
            {
                if (GameManager.Instance.isGameOver) break;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

                if (Mathf.RoundToInt(i % 60) == 0 && i > 59f)
                {
                    time = Random.Range(0.005f, 0.0000000005f);
                    Debug.Log(time);
                }

                if (Mathf.RoundToInt(i % 120) == 0 && i > 119)
                {
                    yield return new WaitForSeconds(Random.Range(0, 2));
                }

                yield return new WaitForSeconds(time);
            }
        }

        isRot = false;
    }

    private IEnumerator TriggerMove()
    {
        isRot = true;
        float originTime = 0.01f;
        float time = originTime;
        float distance = 0;

        while (!GameManager.Instance.isGameOver)
        {
            for (float i = 0; i < 360f; i += 1f)
            {
                if (GameManager.Instance.isGameOver) break;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

                if (GameManager.Instance.isShovel)
                {
                    time = 0.0000000005f;
                    distance = originTime - time;
                    GameManager.Instance.isShovel = false;
                }

                if (time < originTime)
                {
                    time += distance / 360;
                }

                yield return new WaitForSeconds(time);
            }
        }

        isRot = false;
    }

    private IEnumerator ShakeMove()
    {
        isRot = true;
        float time = 0.01f;
        bool isReverse = false;

        while (!GameManager.Instance.isGameOver)
        {
            for (float i = 0; i < 360f;)
            {
                if (isReverse)
                {
                    i += 1f;
                }

                else
                {
                    i -= 1f;
                }

                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

                if (Random.Range(0, 101) == 0)
                {
                    isReverse = !isReverse;
                }

                yield return new WaitForSeconds(time);
            }
        }

        isRot = false;
    }

    private void OnEnable()
    {
        isRot = false;
        IEnumerator coroutine = coroutines[Random.Range(0, coroutines.Length)];
        StopCoroutine(coroutine);
        transform.DOScale(0f, 0f);
        transform.DOScale(1f, 0.3f);
        StartCoroutine(coroutine);
    }
}
