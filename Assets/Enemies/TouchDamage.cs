using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    EnemyStats stats;
    [SerializeField]
    int minDamage = 1;
    [SerializeField]
    int maxDamage = 1;

    bool canDoDamage = true;

    private void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !stats.IsDead)
        {
            Damage(coll.GetComponent<IDamageable>());
        }
    }

    void Damage(IDamageable e)
    {
        DamageInfo damageInfo = new DamageInfo(minDamage, maxDamage, transform);
        e.TakeDamage(damageInfo);
        canDoDamage = false;
    }
}
