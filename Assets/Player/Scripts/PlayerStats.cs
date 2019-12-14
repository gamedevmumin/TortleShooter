using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject{

    [HideInInspector]
    public int currentHP;
    [SerializeField]
    public  int maxHP;
    [SerializeField]
    public FloatReference speed;
    [SerializeField]
    public float jumpHeight;
    [SerializeField]
    public float invincibilityTime;

    public void Set(PlayerStats stats)
    {
        
        currentHP = stats.currentHP;
        maxHP = stats.maxHP;
        speed.Value = stats.speed.Value;
        jumpHeight = stats.jumpHeight;
        invincibilityTime = stats.invincibilityTime;
    }
}
