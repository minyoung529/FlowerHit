using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    [SerializeField] private GameObject knife;
    public Vector2 knifePosition { get; private set; } = Vector2.zero;
    [SerializeField] private GameObject circle = null;
    public Transform pool;

    public bool isGameOver = false;

    private List<KnifeMove> knifeMoves = new List<KnifeMove>();

    public int curCount = 0;
    public int maxCount = 0;
    public int scoreCount = 0;

    public GameObject apple;
    public SpawnFlowers spawnFlowers;
    public KnifeMove currentKnife;

    private User user;
    public User CurrentUser { get { return user; } }

    public UIManager UIManager { get; private set; }

    public List<Flower> flowers = new List<Flower>();
    public Sprite[] flowerSprites { get; private set; }

    public float radius;

    #region 데이터저장
    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        //SAVE_PATH = Application.dataPath + "/Save";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson();
        flowerSprites = Resources.LoadAll<Sprite>("Flowers");
    }

    private void LoadFromJson()
    {
        string json;

        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }

        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }

    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";

        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    #endregion
    private void Start()
    {
        spawnFlowers = circle.GetComponent<SpawnFlowers>();
        radius = circle.GetComponent<CircleCollider2D>().radius;
        knifePosition = new Vector2(0, -3);
        UIManager = GetComponent<UIManager>();
        UIManager.InstantiateKnifeUI();
        UIManager.FirstSetting();
        SpaOIns();
        ResetCount();
    }

    public void SpawnKnife()
    {
        GameObject knifeObject;
        knifeObject = Instantiate(knife);
        KnifeMove knifeMove = knifeObject.GetComponent<KnifeMove>();
        currentKnife = knifeMove;

        knifeMoves.Add(knifeMove);

        knifeObject.transform.position = knifePosition;
        knifeObject.SetActive(true);
    }

    public GameObject GetCircle()
    {
        return circle;
    }

    public void OnClickRestart()
    {
        isGameOver = true;
        DespawnKnives();
        isGameOver = false;
        ResetCount();
    }

    public void DespawnKnives()
    {
        for (int i = 0; i < knifeMoves.Count; i++)
        {
            knifeMoves[i].DespawnKnife();
        }

        SpaOIns();
    }

    public void SpaOIns()
    {
        if (pool.childCount > 0)
        {
            pool.GetChild(0).gameObject.SetActive(true);
            currentKnife = knifeMoves.Find(knife => knife.gameObject == pool.GetChild(0).gameObject);
        }

        else
        {
            SpawnKnife();
        }
    }

    public void Complete()
    {
        if (curCount == maxCount)
        {
            OnClickRestart();
            return;
        }

        SpaOIns();
    }

    private void ResetCount()
    {
        curCount = 0;
        UIManager.ResetGame();
        UIManager.RandomFlowerOrder();
    }

    public void OnClick()
    {
        currentKnife.GoGo();
    }

    public void GameOver()
    {
        UIManager.GameOver();
    }

    public void ResetGame()
    {

    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }
}