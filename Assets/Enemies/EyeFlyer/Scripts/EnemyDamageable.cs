using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyDamageable : MonoBehaviour, IDamageable
{
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

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        if (!stats.IsDead)
        {
            Vector2 scale = new Vector2((float)damageInfo.DamageDone / (float)damageInfo.MaxDamage,
                (float)damageInfo.DamageDone / (float)damageInfo.MaxDamage);
            Debug.Log(damageInfo.DamageDealer.right);   
            if(effectSpawner) effectSpawner.SpawnEffect(damageInfo.DamageDealer.position, scale, damageInfo);
            cameraShake.Shake(0.06f, 0.031f);
            stats.CurrentHP -= damageInfo.DamageDone;
            if (stats.CurrentHP > 0f) AudioManager.instance.PlaySound("Hurt");
            if (anim)
            {
                anim.SetTrigger("TookDamage");
                Debug.Log("XD!");
            }
            StartCoroutine(damageEffect.PlayEffect(sR));
        }
    }

}
