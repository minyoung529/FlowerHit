using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [HideInInspector]
    public AudioSource backgroundAudio;
    [HideInInspector]
    public AudioSource effectSoundAudio;

    [SerializeField] AudioClip inGameBGM;
    [SerializeField] AudioClip lobbyBGM;

    [SerializeField] AudioClip failSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] AudioClip happySound;
    [SerializeField] AudioClip angrySound;

    [SerializeField] AudioClip malangSound;
    [SerializeField] AudioClip closeSound;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
        effectSoundAudio = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void LobbyBGM()
    {
        backgroundAudio.Stop();
        backgroundAudio.clip = lobbyBGM;
        backgroundAudio.Play();
    }

    public void InGameBGM()
    {
        backgroundAudio.Stop();
        backgroundAudio.clip = inGameBGM;
        backgroundAudio.Play();
    }
}
