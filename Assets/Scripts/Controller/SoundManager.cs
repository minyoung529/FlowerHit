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
    [SerializeField] AudioClip ddiringSound;

    [SerializeField] AudioClip shovelSound;
    [SerializeField] AudioClip okaySound;

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

    public void ButtonSound(int index)
    {
        if (index == 0)
        {
            effectSoundAudio.PlayOneShot(malangSound);
        }

        else if (index == 1)
        {
            effectSoundAudio.PlayOneShot(closeSound);
        }
    }

    public void AngrySound()
    {
        effectSoundAudio.PlayOneShot(angrySound);
    }

    public void FailSound()
    {
        effectSoundAudio.PlayOneShot(failSound);
    }

    public void HappySound()
    {
        effectSoundAudio.PlayOneShot(happySound);
    }

    public void SuccessSound()
    {
        effectSoundAudio.PlayOneShot(successSound);
    }

    public void DdiringSound()
    {
        effectSoundAudio.PlayOneShot(ddiringSound); 
    }

    public void ShovelSound()
    {
        effectSoundAudio.PlayOneShot(shovelSound);
    }

    public void OkaySound()
    {
        effectSoundAudio.PlayOneShot(okaySound);
    }
}
