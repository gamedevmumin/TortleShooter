using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStartingStats : ScriptableObject
{
    public int MaxHP { get { return maxHP; } private set { maxHP = value; } }

    public float Speed { get { return speed; } private set { speed = value; } }

    [SerializeField]
    private int maxHP;
    [SerializeField]
    private float speed;
}