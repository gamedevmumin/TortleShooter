using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    EnemyStats stats;

    private void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !stats.IsDead)
        {
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.damageDone = 1;
            coll.GetComponent<IDamageable>().TakeDamage(damageInfo);
        }
    }
}
