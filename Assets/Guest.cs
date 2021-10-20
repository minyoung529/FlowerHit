using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public Material happyMaterial;
    public Material angryMaterial;

    private ParticleSystem particle;
    private ParticleSystem.MainModule pm;
    private ParticleSystemRenderer pr;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        pm = particle.main;
        pr = GetComponentInChildren<ParticleSystemRenderer>();
    }

    public void Angry()
    {
        pr.material = angryMaterial;
        pm.startColor = new ParticleSystem.MinMaxGradient(Color.white, new Color32(255, 47, 50, 255));
        particle.Play();
    }

    public void Happy()
    {
        pr.material = happyMaterial;
        pm.startColor = new ParticleSystem.MinMaxGradient(Color.white, new Color32(255, 122, 207, 255));
        particle.Play();
    }
}
