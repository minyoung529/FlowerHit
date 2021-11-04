using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField] private GameObject knife;
    public Vector2 shovelPosition { get; private set; } = Vector2.zero;
    [SerializeField] private GameObject circle = null;
    private CircleRotation circleRotation;
    public Transform pool;

    public bool isGameOver = false;
    public bool isShovel = false;

    private List<KnifeMove> knifeMoves = new List<KnifeMove>();

    public int curCount { get; private set; } = 0;
    public int maxCount { get; private set; } = 0;

    public bool isReady = false;

    public GameObject apple;
    public SpawnFlowers spawnFlowers;
    public KnifeMove currentKnife;

    [SerializeField]
    private User user;
    public User CurrentUser { get { return user; } }
    public UIManager UIManager { get; private set; }

    public List<Flower> flowers = new List<Flower>();
    public Flower currentFlower;
    public int flowerIndex = 0;
    public Sprite[] flowerSprites { get; private set; }

    public float radius;

    [TextArea]
    public string[] guestOrder;
    [TextArea]
    public string[] happyScript;
    [TextArea]
    public string[] angryScript;

    public Sprite[] shovelSprites;

    private bool isStop;

    #region 데이터저장
    private void Awake()
    {
#if DEVELOPMENT_BUILD
        SAVE_PATH = Application.dataPath + "/Save";
#else
        SAVE_PATH = Application.persistentDataPath + "/Save";
#endif
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
#if DEVELOPMENT_BUILD
        SAVE_PATH = Application.dataPath + "/Save";
#else
        SAVE_PATH = Application.persistentDataPath + "/Save";
#endif

        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    #endregion
    private void Start()
    {
        for (int i = 0; i < user.shovels.Count; i++)
        {
            user.shovels[i].index = i;
        }

        if (user.userShovel == null)
        {
            user.userShovel = user.shovels[0];
        }

        spawnFlowers = circle.GetComponent<SpawnFlowers>();
        circleRotation = circle.GetComponent<CircleRotation>();
        radius = circle.GetComponent<CircleCollider2D>().radius;
        shovelPosition = new Vector2(0, -4.3f);
        UIManager = GetComponent<UIManager>();

        UIManager.FirstSetting();
    }

    public void SpawnKnife()
    {
        GameObject knifeObject;
        knifeObject = Instantiate(knife);
        KnifeMove knifeMove = knifeObject.GetComponent<KnifeMove>();
        currentKnife = knifeMove;

        knifeMoves.Add(knifeMove);

        knifeObject.transform.position = shovelPosition;
        knifeObject.SetActive(true);
    }

    public GameObject GetCircle()
    {
        return circle;
    }

    public CircleRotation GetCircleRotation()
    {
        return circleRotation;
    }

    public void OnClickBubble()
    {
        if (UIManager.isEnd) return;

        StartCoroutine(WaitStart());
    }
    public void OnClickRestart()
    {
        if (UIManager.isEnd) return;

        StartCoroutine(WaitReady());
        isGameOver = false;
        flowerIndex = 0;
        UIManager.gameOverPanel.gameObject.SetActive(false);
        UIManager.OnClickRestart();

        SpawnOrInstantiate();
        ResetCount();
    }

    public void Pooling()
    {
        spawnFlowers.DespawnFlowers();
        DespawnKnives();
    }

    private IEnumerator WaitReady()
    {
        isReady = true;
        yield return new WaitForSeconds(0.5f);
        isReady = false;
        yield break;
    }

    private IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(1f);
        OnClickRestart();
    }

    public void DespawnKnives()
    {
        for (int i = 0; i < knifeMoves.Count; i++)
        {
            knifeMoves[i].DespawnKnife();
        }
    }

    public void SpawnOrInstantiate()
    {
        if (CheckPool(NameManager.KNIFE_TAG))
        {
            GameObject obj = ReturnPoolObject(NameManager.KNIFE_TAG);
            obj.SetActive(true);
            obj.transform.SetParent(pool.parent);
            currentKnife = knifeMoves.Find(knife => knife.gameObject == obj);
        }

        else
        {
            SpawnKnife();
        }
    }

    public void Complete()
    {
        if (flowerIndex == UIManager.currentFlowers.Count)
        {
            UIManager.GoMainScene();
            isGameOver = true;

            UIManager.Success();
            SoundManager.Instance?.SuccessSound();
            return;
        }

        SpawnOrInstantiate();
    }

    private void ResetCount()
    {
        circle.SetActive(true);
        curCount = 0;
        maxCount = Random.Range(6, 10);

        UIManager.ResetGame();
        UIManager.RandomFlowerOrder();
    }

    public void OnClick()
    {
        if (isReady) return;
        if (isGameOver) return;
        currentFlower = UIManager.currentFlowers[flowerIndex];
        SoundManager.Instance?.ShovelSound();
        currentKnife.GoGo();
        isReady = true;
    }

    public void GameOver()
    {
        UIManager.GameOver();
        SoundManager.Instance?.FailSound();
        isGameOver = true;
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }

    public bool CheckPool(string objName)
    {
        for (int i = 0; i < pool.childCount; i++)
        {
            if (pool.childCount > 0 && pool.GetChild(i).name.Contains(objName))
            {
                return true;
            }
        }
        return false;
    }

    public GameObject ReturnPoolObject(string objName)
    {
        for (int i = 0; i < pool.childCount; i++)
        {
            if (pool.childCount > 0 && pool.GetChild(i).name.Contains(objName))
            {
                return pool.GetChild(i).gameObject;
            }
        }
        return null;
    }

    public void OnClickLobby()
    {
        SoundManager.Instance?.LobbyBGM();
        SceneManager.LoadScene(NameManager.MAIN_SCENE);
    }

    public void StopGame()
    {
        if (isStop) return;
        isStop = true;
        Time.timeScale = 0;
    }

    public void GoBackToGame()
    {
        isStop = false;
        Time.timeScale = 1;
    }

    public void GoToLobby()
    {
        isStop = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(NameManager.MAIN_SCENE);
    }

    public void PlusCurrentCount()
    {
        curCount++;
    }
}