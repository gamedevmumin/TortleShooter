using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    float invincibilityTimer;
    [SerializeField]
    PlayerStats stats;
    HeartBar heartBar;
    CameraShake cameraShake;

    void Start()
    {
        heartBar = GameObject.Find("StatPanel/HeartBar").GetComponent<HeartBar>();
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
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
            //StartCoroutine("Blink");
            AudioManager.instance.PlaySound("PlayerDamaged");
            if (heartBar != null) heartBar.changeState(stats.currentHP, stats.maxHP);
            invincibilityTimer = stats.invincibilityTime;
        }
    }
}
