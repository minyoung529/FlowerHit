using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text maxCountText;
    [SerializeField] private Text scoreTxt;
    public Image gameOverPanel;

    [SerializeField] private GameObject knifeUI;
    [SerializeField] private Transform knifeUITransform;
    [SerializeField] private Image orderPanel;

    private List<GameObject> knifeUIs = new List<GameObject>();
    public List<Flower> currentFlowers = new List<Flower>();

    [SerializeField] private Image orderImageTemplate;
    private List<Image> orderImages = new List<Image>();
    [SerializeField] private List<Sprite> flowerIcons = new List<Sprite>();
    [SerializeField] private Image currentFlowerImage;

    [SerializeField] private GameObject mainGameScene;
    [SerializeField] private GameObject mainUIScene;
    [SerializeField] private GameObject inGameScene;
    [SerializeField] private GameObject inGameUIScene;

    [SerializeField] private SpriteRenderer guest;

    private bool isEnd;

    public void InstantiateKnifeUI()
    {
        GameObject obj;

        for (int i = 0; i < 15; i++)
        {
            obj = Instantiate(knifeUI, knifeUITransform);
            knifeUIs.Add(obj);
        }
    }

    public void GameOver()
    {
        if (GameManager.Instance.isGameOver) return;

        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.transform.DOScale(1f, 0.3f);
        orderPanel.transform.DOScale(0f, 0.3f);
        gameOverPanel.transform.DOShakePosition(1.2f, 1).OnComplete(() => GoMainScene());

        for (int i = 0; i < orderImages.Count; i++)
        {
            orderImages[i].transform.GetChild(0).DOScale(0, 0.3f);
            orderImages[i].transform.GetChild(0).gameObject.SetActive(false);
        }
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
        orderPanel.transform.DOScale(1f, 0.3f);
    }

    public void OnClickRestart()
    {
        orderPanel.transform.DOScale(0f, 0f).OnComplete(() => orderPanel.transform.DOScale(1f, 0.5f));

        for (int i = 0; i < orderImages.Count; i++)
        {
            orderImages[i].transform.GetChild(0).DOScale(0, 0.3f);
            orderImages[i].transform.GetChild(0).gameObject.SetActive(false);
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

        for (int i = 0; i < GameManager.Instance.maxCount; i++)
        {
            currentFlowers.Add(GameManager.Instance.flowers[Random.Range(0, GameManager.Instance.flowers.Count)]);
            GameManager.Instance.spawnFlowers.FlowerSpawn(currentFlowers[i]);
        }

        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            if (i + GameManager.Instance.maxCount > 15) break;
            GameManager.Instance.spawnFlowers.FlowerSpawn(GameManager.Instance.flowers[Random.Range(0, GameManager.Instance.flowers.Count)]);
        }

        GameManager.Instance.currentFlower = currentFlowers[0];
        currentFlowerImage.sprite = flowerIcons[GameManager.Instance.currentFlower.index];

        for (int i = 0; i < orderImages.Count; i++)
        {
            if (i < GameManager.Instance.maxCount)
            {
                orderImages[i].color = Color.white;
                orderImages[i].sprite = flowerIcons[currentFlowers[i].index];
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

    public void CheckFlowerIcons(int index)
    {
        orderImages[index].transform.GetChild(0).gameObject.SetActive(true);
        orderImages[index].transform.GetChild(0).DOScale(1, 0.3f);
    }

    public void ChangeCurrentFlowerImage()
    {
        currentFlowerImage.sprite = flowerIcons[GameManager.Instance.currentFlower.index];
    }

    public void GoMainScene()
    {
        mainGameScene.transform.DOMove(Vector3.zero, 1f);
        mainUIScene.transform.DOMove(Vector3.zero, 1f);

        inGameUIScene.transform.DOMoveY(-13, 1f);
        inGameScene.transform.DOMoveY(-13, 1f);
    }

    public void Failure()
    {
        if (isEnd) return;

        StartCoroutine(AngryGuest(true));
    }

    private IEnumerator AngryGuest(bool isAngry)
    {
        isEnd = true;
        Debug.Log("Sdf");
        yield return new WaitForSeconds(2f);

        if(isAngry)
        {
            guest.DOColor(Color.red, 1.5f);
        }

        yield return new WaitForSeconds(1f);
        guest.transform.DOMove(new Vector3(5, -2, 0), 0.5f).OnComplete(() => guest.DOColor(Color.white, 0f));
        yield return new WaitForSeconds(2f);
        guest.transform.position = new Vector3(-5, -2, 0);
        guest.transform.DOMove(new Vector3(0, -2, 0), 0.3f);
        isEnd = false;
    }
}
