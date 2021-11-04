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
    private const int patternIndexCount = 6;

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

        else if(patternIndex == 5)
        {
            RealReverseTriggerMove();
        }
    }

    private void BasicRot()
    {
        speed = 100f;

        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speed));

        if (CheckRotZ(120f))
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
            speed = 230;
            GameManager.Instance.isShovel = false;
        }

        if (speed > 80)
        {
            speed -= 1f;
        }
    }

    private void RealReverseTriggerMove()
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
            speed = 130;
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
        return (Mathf.Abs(transform.rotation.eulerAngles.z) > z && (int)Mathf.Abs(transform.rotation.eulerAngles.z) % z == 0);
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
}
