using System;
using Controllers;
using Data.MetaData;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "MySettings/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    public int Health = 100;
    public float DamageMultiplier = 1f;
    public float ReloadSpeed = 1f;
    public int MagazineCapacity = 2;
}