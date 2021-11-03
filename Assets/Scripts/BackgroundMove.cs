using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Vector2 offset = Vector2.zero;
    float speed = 0.1f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        offset.x += speed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
        Debug.Log(meshRenderer.materials[0].mainTextureOffset);
    }
}
