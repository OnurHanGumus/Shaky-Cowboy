using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using Controllers;
using Managers;
using Enums;
using Signals;

public class DenemeClass : IDeneme
{
    public DenemeClass()
    {
        Debug.Log("denemeClass");
    }

    public void Write()
    {
        Debug.Log("write moethod");
    }
}
