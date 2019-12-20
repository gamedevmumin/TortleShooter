using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    EnemyStats stats;
    [SerializeField]
    int amount = 1;
    private void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !stats.IsDead)
        {
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.damageDone = amount;
            coll.GetComponent<IDamageable>().TakeDamage(damageInfo);
        }
    }
}
