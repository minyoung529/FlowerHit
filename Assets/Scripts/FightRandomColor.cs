using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FightRandomColor : MonoBehaviour
{
    private Image image;
    private bool isEnable;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(RandomColor());

    }

    private IEnumerator RandomColor()
    {
        while (gameObject.activeSelf)
        {
            image.DOColor(new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255), 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
