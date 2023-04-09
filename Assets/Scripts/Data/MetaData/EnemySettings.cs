﻿using Components.Players;
using Components.Enemies;
using UnityEngine;
using Controllers;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ZenjectExample/EnemySettings", order = 0)]
public class EnemySettings : ScriptableObject
{
    [SerializeField] public EnemyMovementController.Settings EnemyMovementSettings;
    [SerializeField] public EnemyShootController.Settings EnemyShootSettings;
}