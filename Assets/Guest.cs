using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public Material happyMaterial;
    public Material angryMaterial;

    private ParticleSystem particle;
    private ParticleSystemRenderer pr;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        pr = GetComponentInChildren<ParticleSystemRenderer>();
    }

    public void Angry()
    {
        pr.material = angryMaterial;
        particle.Play();
    }

    public void Happy()
    {
        pr.material = happyMaterial;
        particle.Play();
    }
}
