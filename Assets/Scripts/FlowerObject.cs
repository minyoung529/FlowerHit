using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlowerObject : MonoBehaviour
{
    public BoxCollider2D col = null;
    public ParticleSystem particle = null;
    private ParticleSystem.MainModule pm;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Flower flower;

    private void Start()
    {
        pm = particle.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            if (GameManager.Instance.currentKnife.isTouch) return;

            if (flower.flowerName != GameManager.Instance.currentFlower.flowerName)
            {
                GameManager.Instance.GameOver();
                GameManager.Instance.UIManager.Failure();
                StartCoroutine(RedLight());
                return;
            }

            GameManager.Instance.currentKnife.isTouch = true;
            particle.Play();
            GameManager.Instance.UIManager.CheckFlowerIcons(GameManager.Instance.flowerIndex);
            GameManager.Instance.flowerIndex++;
            if(GameManager.Instance.flowerIndex < GameManager.Instance.UIManager.currentFlowers.Count)
            {
                GameManager.Instance.currentFlower = GameManager.Instance.UIManager.currentFlowers[GameManager.Instance.flowerIndex];
            }
            GameManager.Instance.scoreCount++;
            GameManager.Instance.UIManager.ChangeCurrentFlowerImage();
            StartCoroutine(Active());
        }
    }

    public void SetFlower(Flower flower)
    {
        float radius = GameManager.Instance.radius;
        this.flower = flower;

        spriteRenderer.sprite = GameManager.Instance.flowerSprites[flower.index];
        gameObject.SetActive(true);

        float rotZ = 180 - (180f * (transform.localPosition.y + radius) * (1 / (radius * 2)))
            + GameManager.Instance.GetCircle().transform.rotation.z;

        if (transform.localPosition.x > 0)
        {
            rotZ *= -1f;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotZ));
        transform.DOScale(0f, 0f);
        transform.DOScale(1f, 0.5f);
        EditColliderSize();
    }

    private void EditColliderSize()
    {
        switch (flower.flowerName)
        {
            case "µ¥ÀÌÁö":
                col.offset = new Vector2(0f, 0.5f);
                col.size = new Vector2(0.7f, 1f);
                break;

            case "Æ«¸³":
                col.offset = new Vector2(0, 0.42f);
                col.size = new Vector2(0.46f, 0.85f);
                break;

            case "Å¬·Î¹ö":
                col.offset = new Vector2(-0.01f, 0.27f);
                col.size = new Vector2(0.38f, 0.55f);
                break;

            case "ÇÏ´Ã²É":
                col.offset = new Vector2(0.02f, 0.37f);
                col.size = new Vector2(0.6f, 0.76f);
                break;

        }
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
        transform.SetParent(GameManager.Instance.pool);
        transform.position = Vector2.zero;
        particle.transform.localPosition = Vector2.one;
        spriteRenderer.color = Color.white;
    }

    IEnumerator RedLight()
    {
        for (int i = 0; i < 5; i++)
        {
            if (!GameManager.Instance.isGameOver)
            {
                spriteRenderer.color = Color.white;
                yield break;
            }

            spriteRenderer.DOColor(Color.red, 0.2f);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.DOColor(Color.white, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }

        spriteRenderer.color = Color.white;
    }

    private IEnumerator Active()
    {
        spriteRenderer.color = Color.clear;
        yield return new WaitForSeconds(pm.duration+1f);
        gameObject.SetActive(false);
        spriteRenderer.color = Color.white;
        yield break;
    }
}
