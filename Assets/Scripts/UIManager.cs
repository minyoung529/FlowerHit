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
    [SerializeField] private Image orderPanel;

    private List<GameObject> knifeUIs = new List<GameObject>();
    public List<Flower> currentFlowers = new List<Flower>();

    [SerializeField] private Image orderImageTemplate;
    private List<Image> orderImages = new List<Image>();

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

        orderPanel.transform.DOScale(0f, 0f);
        orderPanel.transform.DOScale(0f, 0.5f).OnComplete(() => orderPanel.transform.DOScale(1f, 0.5f));
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
            GameManager.Instance.spawnFlowers.FlowerSpawn(currentFlowers[i]);
        }

        for(int i = 0; i < orderImages.Count; i++)
        {
            if(i<count)
            {
                orderImages[i].color = Color.white;
                orderImages[i].sprite = GameManager.Instance.flowerSprites[currentFlowers[i].index];
            }

            else
            {
                orderImages[i].color = Color.clear;
            }

        }
    }

    public void FirstSetting()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject obj = Instantiate(orderImageTemplate.gameObject, orderImageTemplate.transform.parent);
            orderImages.Add(obj.GetComponent<Image>());
            obj.SetActive(true);
        }
    }
}
