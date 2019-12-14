﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public bool IsDead { get; set; }
    public int CurrentHP { get; set; }
    public float Speed { get; set; }
    [SerializeField]
    EnemyStartingStats enemyStartingStats;

    private void Start()
    {
        CurrentHP = enemyStartingStats.MaxHP;
        Speed = enemyStartingStats.Speed;
        IsDead = false;
    }
}
