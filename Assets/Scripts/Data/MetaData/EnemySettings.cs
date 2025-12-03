using Components.Players;
using Components.Enemies;
using UnityEngine;
using Controllers;
using System;
using Zenject;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ZenjectExample/EnemySettings", order = 0)]
public class EnemySettings : ScriptableObject
{
    public int Health = 100;
    public float DamageMultiplier = 1f;
    public float ReloadSpeed = 1f;
    public int MagazineCapacity = 2;
    public EnemyShootDelayEnums ShootDelayEnum;
}