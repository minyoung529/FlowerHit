using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public enum MusicType
    {
        BGM,
        EffectSound
    }

    public MusicType musicType;

    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        if (musicType == MusicType.BGM)
        {
            slider.value = SoundManager.Instance.backgroundAudio.volume;
        }

        else
        {
            slider.value = SoundManager.Instance.effectSoundAudio.volume;
        }
    }

    public void SetVolume()
    {
        if (musicType == MusicType.BGM)
        {
            SoundManager.Instance.backgroundAudio.volume = slider.value;
        }

        else
        {
            SoundManager.Instance.effectSoundAudio.volume = slider.value;
        }
    }
}
