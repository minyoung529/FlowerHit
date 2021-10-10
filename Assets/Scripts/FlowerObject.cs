using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerObject : MonoBehaviour
{
    public Collider2D col = null;
    public ParticleSystem particle = null;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Flower flower;

    void Start()
    {
        col = GetComponent<Collider2D>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            particle.Play();
            GameManager.Instance.scoreCount++;
            GameManager.Instance.UIManager.UpdateApple();
            StartCoroutine(AppleActive());
        }
    }

    private IEnumerator AppleActive()
    {
        transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public void SetFlower(Flower flower)
    {
        this.flower = flower;
        float radius = GameManager.Instance.radius;

        spriteRenderer.sprite = GameManager.Instance.flowerSprites[flower.index];
        gameObject.SetActive(true);

        float rotZ = 180 - (180f * (transform.localPosition.y + radius) * (1 / (radius * 2)));


        if (transform.localPosition.x > 0)
        {
            rotZ *= -1f;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotZ));
    }
}
