using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;
    private void Start()
    {
        SoundManager.Instance?.LobbyBGM();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
        }
    }

    public void Quit()
    {
        quitPanel.SetActive(false);
        Application.Quit();
    }
    public void OnClickGame()
    {
        quitPanel.SetActive(false);
        SceneManager.LoadScene(NameManager.GAME_SCENE);
        SoundManager.Instance?.InGameBGM();
    }
}
