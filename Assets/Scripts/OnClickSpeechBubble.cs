using UnityEngine;
using UnityEngine.UI;

public class OnClickSpeechBubble : MonoBehaviour
{
    [SerializeField] MoveUpDown mainUI;
    [SerializeField] MoveUpDown mainObj;

    [SerializeField] MoveUpDown gameUI;
    [SerializeField] MoveUpDown gameObj;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClickButton());
    }

    public void OnClickButton()
    {
        if (GameManager.Instance.UIManager.isEnd) return;
        GameManager.Instance.OnClickBubble();
        mainUI.GoToGame_Main();
        mainObj.GoToGame_Main();
        gameUI.GoToGame_Game();
        gameObj.GoToGame_Game();
        SoundManager.Instance?.OkaySound();
        GameManager.Instance.GetCircleRotation().StartRot();
    }
}
