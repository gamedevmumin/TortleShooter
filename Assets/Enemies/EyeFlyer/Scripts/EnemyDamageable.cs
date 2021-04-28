using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EnemyStats))]
[RequireComponent (typeof (Rigidbody2D))]
public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private GameEvent enemyDamaged;
    EnemyStats stats;
    [SerializeField]
    DamageIndicatorSpawner effectSpawner;
    [SerializeField]
    VisualSpriteEffect damageEffect;
    [SerializeField]
    SpriteRenderer sR;
    Rigidbody2D rb;
    CameraShake cameraShake;
    [SerializeField]
    Animator anim;
    public bool IsDamageable => !stats.IsDead;

    void Awake () {
        stats = GetComponent<EnemyStats> ();
        rb = GetComponent<Rigidbody2D> ();
        cameraShake = FindObjectOfType<CameraShake> ();
    }

    public void TakeDamage (DamageInfo damageInfo) {
        if (!stats.IsDead) {
            Vector2 scale = new Vector2 ((float) damageInfo.DamageDone / (float) damageInfo.MaxDamage,
                (float) damageInfo.DamageDone / (float) damageInfo.MaxDamage);
            if (effectSpawner) effectSpawner.SpawnEffect (damageInfo.DamageDealer.position, scale, damageInfo);
           
            stats.CurrentHP -= damageInfo.DamageDone;
            if(enemyDamaged) enemyDamaged.Raise();
            if (damageInfo.WasCritical) {
                AudioManager.instance.PlaySound ("CriticalStrike");
                 cameraShake.Shake (0.3f, 0.036f);
            } else {
                AudioManager.instance.PlaySound ("Hurt");
                 cameraShake.Shake (0.06f, 0.031f);
            }

            if (anim) {
                anim.SetTrigger ("TookDamage");
            }
            StartCoroutine (damageEffect.PlayEffect (sR));
        }
    }

}