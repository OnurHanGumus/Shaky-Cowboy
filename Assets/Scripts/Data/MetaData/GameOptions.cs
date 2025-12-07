using Components.Players;
using Components.Enemies;
using UnityEngine;
using Controllers;
using System;
using Zenject;

[CreateAssetMenu(fileName = "GameOptions", menuName = "MySettings/GameOptions", order = 0)]
public class GameOptions : ScriptableObject
{
    [Header("Fader: Store Panel")]
    public float FaderStartDelay_StorePanel = 1f;
    public float FaderActiveDuration_StorePanel = 0.5f;
    public float FadingProcessDurationDelay_StorePanel = 0.5f;
}