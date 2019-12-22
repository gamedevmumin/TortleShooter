using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    [SerializeField]
    int minDamage = 1;
    [SerializeField]
    int maxDamage = 1;
    [SerializeField]
    List<string> targetedTags;
    IDestructible destructible;
    EnemyStats stats;
    private void Awake()
    {
        destructible = GetComponent<IDestructible>();
        stats = GetComponent<EnemyStats>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (stats.IsDead == false)
        {
            foreach (string tag in targetedTags)
            {
                if (coll.gameObject.tag == tag)
                {
                    IDamageable d = coll.GetComponent<IDamageable>();
                    if (d.IsDamageable)
                    {
                        Damage(d);
                        if (destructible != null) destructible.Destroy(null);
                    }
                    break;
                }
            }
        }

    }

    void Damage(IDamageable e)
    {
        DamageInfo damageInfo = new DamageInfo(minDamage, maxDamage, transform);
        e.TakeDamage(damageInfo);
    }
}
