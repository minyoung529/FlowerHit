using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public Image gameOverPanel;

    [SerializeField] private Image orderPanel;

    public List<Flower> currentFlowers = new List<Flower>();

    [SerializeField] private Image orderImageTemplate;
    private List<Image> orderImages = new List<Image>();
    [SerializeField] private List<Sprite> flowerIcons = new List<Sprite>();
    [SerializeField] private Image currentFlowerImage;

    [SerializeField] private GameObject mainGameScene;
    [SerializeField] private GameObject mainUIScene;
    [SerializeField] private GameObject inGameScene;
    [SerializeField] private GameObject inGameUIScene;
    [SerializeField] private Button speechBubble;

    [SerializeField] private SpriteRenderer guest;

    [SerializeField] private Text guestText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text helpText;

    public ParticleSystem money;

    public Sprite[] guests;

    Guest guestScript;

    public bool isEnd { get; private set; }

    public void UpdatePanel()
    {
        moneyText.text = string.Format("{0}¿ø", GameManager.Instance.CurrentUser.coin);
    }
    public void GameOver()
    {
        if (GameManager.Instance.isGameOver) return;
        if (isEnd) return;

        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.transform.DOScale(1f, 0.3f);
        orderPanel.transform.DOScale(0f, 0.3f);
        gameOverPanel.transform.DOShakePosition(1.5f, 30).OnComplete(() => GoMainScene());

        for (int i = 0; i < orderImages.Count; i++)
        {
            orderImages[i].transform.GetChild(0).DOScale(0, 0.3f);
            orderImages[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ResetGame()
    {
        gameOverPanel.gameObject.SetActive(false);
        orderPanel.gameObject.SetActive(true);
        orderPanel.transform.DOScale(0f, 0f);
        orderPanel.transform.DOScale(1f, 0.3f);
    }

    public void OnClickRestart()
    {
        orderPanel.gameObject.SetActive(true);
        orderPanel.transform.DOScale(0f, 0f).OnComplete(() => orderPanel.transform.DOScale(1f, 0.5f));

        for (int i = 0; i < orderImages.Count; i++)
        {
            orderImages[i].transform.GetChild(0).DOScale(0, 0.3f);
            orderImages[i].transform.GetChild(0).gameObject.SetActive(false);
        }
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
        guestScript = FindObjectOfType<Guest>();
        UpdatePanel();

        guestText.text = "";
        guestText.DOText(GameManager.Instance.guestOrder[Random.Range(0, GameManager.Instance.guestOrder.Length)], 1f);
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

    public void Success()
    {
        if (isEnd) return;
        StartCoroutine(AngryGuest(false));
    }

    public void Failure()
    {
        if (isEnd) return;

        StartCoroutine(AngryGuest(true));
    }

    private IEnumerator AngryGuest(bool isAngry)
    {
        isEnd = true;
        helpText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        guestText.text = "";

        if (isAngry)
        {
            guest.DOColor(Color.red, 1.5f);
        }

        if (isAngry)
        {
            guestText.DOText(GameManager.Instance.angryScript[Random.Range(0, GameManager.Instance.angryScript.Length)], 1f);
            guestScript.Angry();
        }
        else
        {
            guestText.DOText(GameManager.Instance.happyScript[Random.Range(0, GameManager.Instance.happyScript.Length)], 1f);
            guestScript.Happy();
            GameManager.Instance.CurrentUser.coin += 100;
            money.Play();
            UpdatePanel();
        }

        yield return new WaitForSeconds(2.6f);
        guest.transform.DOMove(new Vector3(5, -2, 0), 0.5f).OnComplete(() => guest.DOColor(Color.white, 0f));

        speechBubble.transform.DOScale(0f, 0.3f);
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        guestText.text = "";
        guestText.DOText(GameManager.Instance.guestOrder[Random.Range(0, GameManager.Instance.guestOrder.Length)], 1f);
        speechBubble.transform.DOScale(1f, 0.3f);
        guest.transform.position = new Vector3(-5, -2, 0);
        guest.sprite = guests[Random.Range(0, guests.Length)];
        guest.transform.DOMove(new Vector3(0, -2, 0), 0.3f);

        gameOverPanel.gameObject.SetActive(false);
        orderPanel.gameObject.SetActive(false);
        GameManager.Instance.Pooling();
        helpText.gameObject.SetActive(true);

        isEnd = false;
    }
}
