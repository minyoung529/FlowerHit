using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMove : MonoBehaviour
{
    [SerializeField]
    private float spped = 20f;
    private Rigidbody2D rigid = null;
    private Collider2D col = null;

    public int speeddddd = 10;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void GoGo()
    {
        GameManager.Instance.UIManager.UsingKnifeUI();
        rigid.AddForce(Vector2.up * spped, ForceMode2D.Impulse);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            transform.SetParent(collision.transform);
            GameManager.Instance.curCount++;
            GameManager.Instance.Complete();
        }

        if (collision.gameObject.CompareTag("Knife"))
        {
            transform.SetParent(GameManager.Instance.GetCircle().transform);
            GameManager.Instance.GameOver();
        }

        SetZero();
    }

    public void DespawnKnife()
    {
        if (GameManager.Instance.isGameOver)
        {
            gameObject.SetActive(false);
            transform.SetParent(GameManager.Instance.pool);
            transform.position = GameManager.Instance.knifePosition;
            transform.rotation = Quaternion.identity;

            SetZero();
        }
    }

    private void SetZero()
    {
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0f;
    }
}