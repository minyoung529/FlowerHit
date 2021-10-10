using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleRotation : MonoBehaviour
{
    private bool isRot = false;

    private IEnumerator CirRotation()
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

    private void OnEnable()
    {
        isRot = false;
        StopCoroutine("CirRotation");
        transform.DOScale(0f, 0f);
        transform.DOScale(1f, 0.3f);
        transform.rotation = Quaternion.identity;
        StartCoroutine(CirRotation());
    }
}
