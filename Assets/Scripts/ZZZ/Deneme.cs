using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using Controllers;
using Managers;
using Enums;
using Signals;
using Zenject;

public class Deneme : MonoBehaviour
{
    [Inject] IDeneme denemeClass;
    private void Start()
    {
        denemeClass.Write();
    }

    public class Factory : PlaceholderFactory<Deneme>, IPool
    {
        GameObject IPool.OnCreate()
        {
            return base.Create().gameObject;
        }
    }
}