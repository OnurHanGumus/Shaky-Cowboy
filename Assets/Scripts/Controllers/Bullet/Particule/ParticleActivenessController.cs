using System;
using System.Collections.Generic;
using UnityEngine;

class ParticleActivenessController : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particleSystems;
    [SerializeField] private GameObject bulletLight;
    private void OnEnable()
    {
        ChangeActiveness(true);
    }

    private void ChangeActiveness(bool isActive)
    {
        foreach (var ps in particleSystems)
        {
            var main = ps.main;
            main.loop = isActive;
        }

        bulletLight.SetActive(isActive);
    }


    public void Disable()
    {
        ChangeActiveness(false);

    }
}