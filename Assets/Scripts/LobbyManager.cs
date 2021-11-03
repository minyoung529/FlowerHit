using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance?.LobbyBGM();
    }

    public void OnClickGame()
    {
        SceneManager.LoadScene(NameManager.GAME_SCENE);
        SoundManager.Instance?.InGameBGM();
    }
}
