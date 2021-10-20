using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveUpDown : MonoBehaviour
{
    public void MoveY(int distance)
    {
        transform.DOMoveY(distance, 1f);
    }

    public void MoveX(int distance)
    {
        transform.DOMoveX(distance, 1f);
    }

    public void MoveStage()
    {
        transform.DOMove(Vector2.zero, 1f);
    }

    public void GoToGame_Main()
    {
        if (GameManager.Instance.UIManager.isEnd) return;
        transform.DOMoveY(13, 1f);
    }

    public void GoToGame_Game()
    {
        if (GameManager.Instance.UIManager.isEnd) return;
        transform.DOMove(Vector2.zero, 1f);
    }
}
