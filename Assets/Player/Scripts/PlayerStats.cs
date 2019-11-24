using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats{

    [HideInInspector]
    public int currentHP;
    [SerializeField]
    public  int maxHP = 3;
    [SerializeField]
    public float speed;
    [SerializeField]
    public float jumpHeight;
    [SerializeField]
    public float invincibilityTime;
}
