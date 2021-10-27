using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
