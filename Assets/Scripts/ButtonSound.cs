using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public enum SoundType
    {
        malangSound,
        closeSound
    }

    [SerializeField] SoundType soundType;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClickButton());
    }

    public void OnClickButton()
    {
        SoundManager.Instance.ButtonSound((int)soundType);
    }
}
