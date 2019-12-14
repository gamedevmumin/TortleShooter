using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyDamageable : MonoBehaviour, IDamageable
{
    EnemyStats stats;
    [SerializeField]
    DamageIndicatorSpawner effectSpawner;
    [SerializeField]
    VisualSpriteEffect damageEffect;
    [SerializeField]
    SpriteRenderer sR;
    void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        if (!stats.IsDead)
        {
            Vector2 scale = new Vector2((float)damageInfo.damageDone / (float)damageInfo.maxDamage,
                (float)damageInfo.damageDone / (float)damageInfo.maxDamage);
            if(effectSpawner) effectSpawner.SpawnEffect(damageInfo.damageDealer.position, scale, damageInfo);
            //cameraShake.Shake(0.05f, 0.1f);
            stats.CurrentHP -= damageInfo.damageDone;
            if (stats.CurrentHP > 0f) AudioManager.instance.PlaySound("Hurt");
            StartCoroutine(damageEffect.PlayEffect(sR));
        }
    }

}
