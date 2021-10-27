using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image check;
    [SerializeField] private Image image;

    Shovel shovel;

    void Start()
    {
        button.onClick.AddListener(() => Choice());
    }

    public void SetValue(int index)
    {
        shovel = GameManager.Instance.CurrentUser.shovels[index];

        if(shovel.index==GameManager.Instance.CurrentUser.userShovel.index)
        {
            check.gameObject.SetActive(true);
        }

        image.sprite = GameManager.Instance.shovelSprites[index];
        UpdateData();
    }

    public void UpdateData()
    {
        if(!shovel.isHaving)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void Choice()
    {
        GameManager.Instance.UIManager.ActiveShovelsCheck();
        check.gameObject.SetActive(true);
        check.transform.DOScale(0f, 0f);
        check.transform.DOScale(1f, 0.3f);
        GameManager.Instance.CurrentUser.userShovel = shovel;
    }

    public void InactiveCheck()
    {
        check.transform.DOScale(1f, 0f);
        check.gameObject.SetActive(false);
    }
}
