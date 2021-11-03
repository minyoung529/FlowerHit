using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CircleRotation : MonoBehaviour
{
    private bool isRot = false;

    private float curTime = 0f;
    private float delayTime = 0f;

    private int patternIndex = 0;
    private const int patternIndexCount = 5;

    private float speed = 70f;

    private bool isReverse = false;

    private void Update()
    {
        if (!isRot) return;
        curTime += Time.deltaTime;
        if (curTime < delayTime) return;

        else
        {
            curTime = 0f;
            delayTime = 0f;
        }

        if (patternIndex == 0)
        {
            BasicRot();
        }

        else if (patternIndex == 1)
        {
            InRegular();
        }

        else if (patternIndex == 2)
        {
            TriggerMove();
        }

        else if (patternIndex == 3)
        {
            ReverseTriggerMove();
        }

        else if (patternIndex == 4)
        {
            TwoSpeed();
        }
    }

    private void BasicRot()
    {
        speed = 100f;

        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speed));

        if (Mathf.Abs(transform.rotation.eulerAngles.z) > 120f && (int)transform.rotation.eulerAngles.z % 120f == 0)
        {
            delayTime = Random.Range(0f, 0.5f);
        }
    }
    private void InRegular()
    {
        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speed));

        if (CheckRotZ(60f))
        {
            speed = Random.Range(70f, 180f);
        }

        if (CheckRotZ(120f))
        {
            delayTime = Random.Range(1f, 2f);
        }
    }
    private void TriggerMove()
    {
        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speed));

        if (GameManager.Instance.isShovel)
        {
            speed = 80;
            GameManager.Instance.isShovel = false;
        }

        if (speed < 230f)
        {
            speed += 1f;
        }
    }
    private void TwoSpeed()
    {
        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speed));

        if (Random.Range(0, 50) == 0)
        {
            speed = 150;
        }
        else
        {
            speed = 100;
        }
    }
    private void ReverseTriggerMove()
    {
        if (!isReverse)
        {
            transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speed));
        }

        else
        {
            transform.Rotate(new Vector3(0f, 0f, -(Time.deltaTime * speed)));
        }

        if (Random.Range(0, 280) == 0)
        {
            isReverse = !isReverse;
        }
    }
    private bool CheckRotZ(float z)
    {
        return (Mathf.Abs(transform.rotation.eulerAngles.z) > z && (int)transform.rotation.eulerAngles.z % z == 0);
    }

    public void StopRot()
    {
        isRot = false;
        gameObject.SetActive(false);

    }
    public void StartRot()
    {
        isRot = true;
        gameObject.SetActive(true);

        transform.DOScale(0f, 0f);
        transform.DOScale(1f, 0.3f);

        patternIndex = Random.Range(0, patternIndexCount);
    }



    //private bool isRot = false;
    //private IEnumerator[] coroutines = new IEnumerator[6];

    //int increase = 1;

    //const int patternIndexCount = 5;


    //private void Awake()
    //{
    //    coroutines[0] = BasicCirRotation();
    //    coroutines[1] = NotRegular();
    //    coroutines[2] = TriggerMove();
    //    coroutines[3] = ShakeMove();
    //    coroutines[4] = DiffSpeedMove();
    //    coroutines[5] = ReverseTriggerMove();

    //}
    //private bool CheckRotZ(float z)
    //{
    //    return (Mathf.Abs(transform.rotation.eulerAngles.z) > z && (int)transform.rotation.eulerAngles.z % z == 0);
    //}

    //public void StopRot()
    //{
    //    isRot = false;
    //    gameObject.SetActive(false);

    //}
    //private IEnumerator BasicCirRotation()
    //{
    //    isRot = true;

    //    while (true)
    //    {
    //        for (int i = 0; i < 360; i += increase)
    //        {
    //            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));
    //            yield return new WaitForSeconds(Random.Range(0.001f, 0.01f));
    //        }

    //        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
    //    }
    //}
    //private IEnumerator NotRegular()
    //{
    //    isRot = true;
    //    float time = Random.Range(0.005f, 0.000000005f);

    //    while (true)
    //    {
    //        for (float i = 0; i < 360f; i += increase)
    //        {
    //            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

    //            if (Mathf.RoundToInt(i % 60) == 0 && i > 59f)
    //            {
    //                time = Random.Range(0.005f, 0.0000000005f);
    //            }

    //            if (Mathf.RoundToInt(i % 120) == 0 && i > 119)
    //            {
    //                yield return new WaitForSeconds(Random.Range(0, 2));
    //            }

    //            yield return new WaitForSeconds(time);
    //        }
    //    }
    //}
    //private IEnumerator TriggerMove()
    //{
    //    isRot = true;
    //    float originTime = 0.01f;
    //    float time = originTime;
    //    float distance = 0;

    //    while (true)
    //    {
    //        for (float i = 0; i < 360f; i += increase)
    //        {
    //            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

    //            if (GameManager.Instance.isShovel)
    //            {
    //                time = 0.0000000005f;
    //                distance = originTime - time;
    //                GameManager.Instance.isShovel = false;
    //            }

    //            if (time < originTime)
    //            {
    //                time += distance / 360;
    //            }

    //            yield return new WaitForSeconds(time);
    //        }
    //    }
    //}
    //private IEnumerator ShakeMove()
    //{
    //    isRot = true;
    //    float time = 0.01f;
    //    bool isReverse = false;

    //    while (true)
    //    {
    //        for (float i = 0; i < 360f;)
    //        {
    //            if (isReverse)
    //            {
    //                i += increase;
    //            }

    //            else
    //            {
    //                i -= increase;
    //            }

    //            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

    //            if (Random.Range(0, 101) == 0)
    //            {
    //                isReverse = !isReverse;
    //            }

    //            yield return new WaitForSeconds(time);
    //        }
    //    }
    //}
    //private IEnumerator DiffSpeedMove()
    //{
    //    isRot = true;
    //    float time;
    //    bool isReverse = false;

    //    while (true)
    //    {
    //        for (float i = 0; i < 360f; i += increase)
    //        {
    //            if (isReverse)
    //            {
    //                time = 0.005f;
    //            }

    //            else
    //            {
    //                time = 0.00000005f;
    //            }

    //            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

    //            if (Random.Range(0, 250) == 0)
    //            {
    //                isReverse = !isReverse;
    //            }

    //            yield return new WaitForSeconds(time);
    //        }
    //    }
    //}
    //private IEnumerator ReverseTriggerMove()
    //{
    //    isRot = true;
    //    float originTime = 0.00000005f;
    //    float time = originTime;
    //    float distance = 0;

    //    while (true)
    //    {
    //        for (float i = 0; i < 360f; i += increase)
    //        {
    //            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, i));

    //            if (GameManager.Instance.isShovel)
    //            {
    //                time = 0.01f;
    //                distance = originTime - time;
    //                GameManager.Instance.isShovel = false;
    //            }

    //            if (time > originTime)
    //            {
    //                time += distance / 360;
    //            }

    //            yield return new WaitForSeconds(time);
    //        }
    //    }
    //}

    //public void StartRot()
    //{
    //    StopAllCoroutines();
    //    isRot = true;
    //    gameObject.SetActive(true);

    //    transform.DOScale(0f, 0f);
    //    transform.DOScale(1f, 0.3f);

    //    StartCoroutine(coroutines[Random.Range(0, coroutines.Length)]);
    //}
}
