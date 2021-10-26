using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnifeMove : MonoBehaviour
{
    [SerializeField]
    private float spped = 20f;
    private Rigidbody2D rigid = null;
    private Collider2D col = null;

    public bool isTouch;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void GoGo()
    {
        rigid.AddForce(Vector2.up * spped * 65);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            transform.SetParent(collision.transform);
            GameManager.Instance.curCount++;
            GameManager.Instance.Complete();
            rigid.velocity = Vector3.zero;
            GameManager.Instance.isReady = false;
            SetZero();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTouch) return;
        if (collision.gameObject.CompareTag("Knife"))
        {
            transform.SetParent(GameManager.Instance.GetCircle().transform);
            GameManager.Instance.GameOver();
            GameManager.Instance.UIManager.Failure();
        }

        GameManager.Instance.isReady = false;
        SetZero();
    }

    public void DespawnKnife()
    {
        gameObject.SetActive(false);
        transform.SetParent(GameManager.Instance.pool);
        transform.position = GameManager.Instance.knifePosition;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        col.enabled = true;
        isTouch = false;
        SetZero();
    }

    private void SetZero()
    {
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0f;
    }

}