using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particl : MonoBehaviour
{
    public ParticleSystem particle;

    void Awake()
    {
        particle.Play();

    }
}
