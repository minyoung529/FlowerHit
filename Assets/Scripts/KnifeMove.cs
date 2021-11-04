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

    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool isTouch;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void GoGo()
    {
        rigid.AddForce(Vector2.up * spped * 55);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(NameManager.CIRCLE_TAG))
        {
            transform.SetParent(collision.transform);
            GameManager.Instance.PlusCurrentCount();
            GameManager.Instance.Complete();
            rigid.velocity = Vector3.zero;
            GameManager.Instance.isShovel = true;
            GameManager.Instance.isReady = false;
            SetZero();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTouch) return;

        if (collision.gameObject.CompareTag(NameManager.KNIFE_TAG))
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
        transform.position = GameManager.Instance.shovelPosition;
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

    private void OnEnable()
    {
        ChangeSprite();
    }
    public void ChangeSprite()
    {
        if (spriteRenderer.sprite == GameManager.Instance.shovelSprites[GameManager.Instance.CurrentUser.userShovel.index])
            return;

        spriteRenderer.sprite = GameManager.Instance.shovelSprites[GameManager.Instance.CurrentUser.userShovel.index];
    }
}