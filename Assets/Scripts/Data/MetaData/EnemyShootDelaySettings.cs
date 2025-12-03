using Components.Players;
using Components.Enemies;
using UnityEngine;
using Controllers;
using System;
using AYellowpaper;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyShootDelaySettings", menuName = "ZenjectExample/EnemyShootDelaySettings", order = 0)]
public class EnemyShootDelaySettings : ScriptableObject
{
    public List<EnemyShootDelayStruct> EnemyShootDelayList;

}

[System.Serializable]
public struct EnemyShootDelayStruct
{
    public float MinDelay;
    public float MaxDelay;
}