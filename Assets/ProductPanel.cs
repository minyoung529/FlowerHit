using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductPanel : MonoBehaviour
{
    public Image image;
    public Text priceText;

    public Shovel shovel;

    public void SetValue(int index)
    {
        image.sprite = GameManager.Instance.shovelSprites[index];
        priceText.text = string.Format("{0}¿ø", GameManager.Instance.CurrentUser.shovels[index].price);
    }
}
