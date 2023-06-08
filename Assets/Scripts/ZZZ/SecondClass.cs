using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using Controllers;
using Managers;
using Enums;
using Signals;

public class SecondClass : IDeneme
{
    public SecondClass()
    {
        Debug.Log("secondClass");
    }

    public void Write()
    {
        Debug.Log("write moethod second class");
    }
}
