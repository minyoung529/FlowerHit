using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProductPanel : MonoBehaviour
{
    public Image image;
    public Text priceText;

    public Shovel shovel;
    public Button button;

    private int index;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Purchase());
    }
    public void SetValue(int index)
    {
        this.index = index;
        shovel = GameManager.Instance.CurrentUser.shovels[index];
        image.sprite = GameManager.Instance.shovelSprites[index];
        priceText.text = string.Format("{0}원", GameManager.Instance.CurrentUser.shovels[index].price);
        CheckIsHaving();
    }

    public void CheckIsHaving()
    {
        if(shovel.isHaving)
        {
            priceText.text = "보유 상품";
        }
    }

    public void Purchase()
    {
        if (GameManager.Instance.CurrentUser.coin < shovel.price) return;

        shovel = GameManager.Instance.CurrentUser.shovels.Find(x => x.index == index);
        shovel.isHaving = true;
        GameManager.Instance.CurrentUser.coin -= shovel.price;
        GameManager.Instance.UIManager.UpdatePanel();
        GameManager.Instance.UIManager.UpdateShovels();
        CheckIsHaving();
    }
}
