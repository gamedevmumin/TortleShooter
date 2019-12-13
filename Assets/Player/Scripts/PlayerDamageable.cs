using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    float invincibilityTimer;
    public float InvincibilityTimer { get { return invincibilityTimer; } private set { invincibilityTimer = value; } }
    [SerializeField]
    PlayerStats stats;
    HeartBar heartBar;
    CameraShake cameraShake;
    [SerializeField]
    VisualSpriteEffect damageEffect;
    SpriteRenderer sR;

    void Start()
    {
        GameObject heartBarObj = GameObject.Find("StatPanel/HeartBar");
        if(heartBarObj) heartBar = heartBarObj.GetComponent<HeartBar>();
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        sR = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        invincibilityTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damageValue)
    {
        if (invincibilityTimer <= 0f)
        {
            cameraShake.blind();
            stats.currentHP -= damageValue;
            StartCoroutine(damageEffect.PlayEffect(sR));
            AudioManager.instance.PlaySound("PlayerDamaged");
            if (heartBar != null) heartBar.changeState(stats.currentHP, stats.maxHP);
            StartInvincibilityTimer(stats.invincibilityTime);
        }
        
    }

    public void StartInvincibilityTimer(float invincibilityTime)
    {
        invincibilityTimer = invincibilityTime;
    }
}

