using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleRotation : MonoBehaviour
{
    private bool isRot = false;
    private IEnumerator[] coroutines = new IEnumerator[6];

    private void Awake()
    {
        coroutines[0] = BasicCirRotation();
        coroutines[1] = NotRegular();
        coroutines[2] = TriggerMove();
        coroutines[3] = ShakeMove();
        coroutines[4] = DiffSpeedMove();
        coroutines[5] = ReverseTriggerMove();
    }

    private IEnumerator BasicCirRotation()
    {
        isRot = true;

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

    private IEnumerator NotRegular()
    {
        isRot = true;
        float time = Random.Range(0.005f, 0.000000005f);

        while (true)
        {
            for (float i = 0; i < 360f; i += 1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

                if (Mathf.RoundToInt(i % 60) == 0 && i > 59f)
                {
                    time = Random.Range(0.005f, 0.0000000005f);
                }

                if (Mathf.RoundToInt(i % 120) == 0 && i > 119)
                {
                    yield return new WaitForSeconds(Random.Range(0, 2));
                }

                yield return new WaitForSeconds(time);
            }
        }
    }

    private IEnumerator TriggerMove()
    {
        isRot = true;
        float originTime = 0.01f;
        float time = originTime;
        float distance = 0;

        while (true)
        {
            for (float i = 0; i < 360f; i += 1f)
            {
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
    }

    private IEnumerator ShakeMove()
    {
        isRot = true;
        float time = 0.01f;
        bool isReverse = false;

        while (true)
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
    }

    private IEnumerator DiffSpeedMove()
    {
        isRot = true;
        float time;
        bool isReverse = false;

        while (true)
        {
            for (float i = 0; i < 360f; i+=1f)
            {
                if (isReverse)
                {
                    time = 0.005f;
                }

                else
                {
                    time = 0.00000005f;
                }

                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

                if (Random.Range(0, 250) == 0)
                {
                    isReverse = !isReverse;
                }

                yield return new WaitForSeconds(time);
            }
        }
    }

    private IEnumerator ReverseTriggerMove()
    {
        isRot = true;
        float originTime = 0.00000005f;
        float time = originTime;
        float distance = 0;

        while (true)
        {
            for (float i = 0; i < 360f; i += 1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

                if (GameManager.Instance.isShovel)
                {
                    time = 0.01f;
                    distance = originTime - time;
                    GameManager.Instance.isShovel = false;
                }

                if (time > originTime)
                {
                    time += distance / 360;
                }

                yield return new WaitForSeconds(time);
            }
        }
    }

    public void StopRot()
    {
        isRot = false;
        StopAllCoroutines();
        gameObject.SetActive(false);

    }
    public void StartRot()
    {
        Debug.Log("start");
        isRot = false;

        gameObject.SetActive(true);

        transform.DOScale(0f, 0f);
        transform.DOScale(1f, 0.3f);

        IEnumerator coroutine = coroutines[Random.Range(0, coroutines.Length)];
        StartCoroutine(coroutine);
    }
}
