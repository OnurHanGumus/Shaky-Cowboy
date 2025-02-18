using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using Controllers;
using Managers;
using Enums;
using Signals;
using DG.Tweening;

public class BankDoorController : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, -157.642f, 0), 5f).SetEase(Ease.Flash);
    }
}