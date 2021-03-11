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
    public FloatReference maxSpeed;
    [SerializeField]
    public float jumpHeight;
    [SerializeField]
    public float invincibilityTime;
    public bool IsFullHealth => currentHP == maxHP;
    
    [SerializeField] private GameEvent onHeal;
    
    public void Heal(int amount)
    {
        if (IsFullHealth) return;
        currentHP += amount;
        onHeal.Raise();
    }

    public void Set(PlayerStats stats)
    {        
        currentHP = stats.currentHP;
        maxHP = stats.maxHP;
        maxSpeed.Value = stats.maxSpeed;
        speed.Value = stats.maxSpeed;
        jumpHeight = stats.jumpHeight;
        invincibilityTime = stats.invincibilityTime;
    }
}
