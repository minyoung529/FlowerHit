using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text maxCountText;
    [SerializeField] private Text message;
    [SerializeField] private Text scoreTxt;
    public Image gameOverPanel;

    [SerializeField] private GameObject knifeUI;
    [SerializeField] private Transform knifeUITransform;
    private List<GameObject> knifeUIs = new List<GameObject>();
    public List<Flower> currentFlowers = new List<Flower>();


    public void InstantiateKnifeUI()
    {
        GameObject obj;

        for (int i = 0; i < 15; i++)
        {
            obj = Instantiate(knifeUI, knifeUITransform);
            knifeUIs.Add(obj);
        }
    }

    private IEnumerator GameOverEffect()
    {
        float time = 0.35f;
        gameOverPanel.gameObject.SetActive(true);

        while (true)
        {
            if (!GameManager.Instance.isGameOver)
            {
                gameOverPanel.transform.DOScale(new Vector3(0, 0, 0), 0.1f);
                gameOverPanel.gameObject.SetActive(false);
                yield break;
            }

            gameOverPanel.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), time);
            yield return new WaitForSeconds(0.2f);
            gameOverPanel.transform.DOScale(new Vector3(1f, 1f, 1f), time);
            yield return new WaitForSeconds(0.2f);
        }

    }

    public void GameOver()
    {
        if (GameManager.Instance.isGameOver) return;

        message.text = "´Ô ÆÐ¹èž ¤»¤» ÀßÇØ¾¹´Ï´ç ¤»¤»~~~";
        GameManager.Instance.scoreCount = 0;
        scoreTxt.text = GameManager.Instance.scoreCount.ToString();
        GameManager.Instance.isGameOver = true;
        StartCoroutine(GameOverEffect());
    }

    public void UpdateUI()
    {
        maxCountText.text = string.Format("{0} / {1}", GameManager.Instance.curCount, GameManager.Instance.maxCount);

    }

    public void ResetGame()
    {
        maxCountText.text = string.Format("{0} / {1}", GameManager.Instance.curCount, GameManager.Instance.maxCount);

        for (int i = 0; i < Random.Range(4, 9); i++)
        {
            knifeUIs[i].SetActive(true);
        }
    }

    public void UsingKnifeUI()
    {
        knifeUIs[GameManager.Instance.curCount].SetActive(false);
    }

    public void UpdateApple()
    {
        scoreTxt.text = GameManager.Instance.scoreCount.ToString();
    }

    public void RandomFlowerOrder()
    {
        currentFlowers.Clear();
        int count = Random.Range(7, 12);

        for (int i = 0; i < count; i++)
        {
            currentFlowers.Add(GameManager.Instance.flowers[Random.Range(0, GameManager.Instance.flowers.Count)]);
        }

        for (int i = 0; i < count; i++)
        {
            GameManager.Instance.spawnFlowers.FlowerSpawn(currentFlowers[i]);
        }
    }
}
