using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTouchDamage : MonoBehaviour
{
    [SerializeField]
    int minDamage = 1;
    [SerializeField]
    int maxDamage = 1;
    [SerializeField]
    List<string> targetedTags;
    IDestructible destructible;
    private void Awake()
    {
        destructible = GetComponent<IDestructible>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        foreach (string tag in targetedTags)
        {
            if (coll.gameObject.tag == tag)
            {
                IDamageable d = coll.GetComponent<IDamageable>();
                if (d!=null&& d.IsDamageable)
                {
                    Damage(d);
                    if (destructible != null) destructible.Destroy(null);
                }
                break;
            }
        }
    }

    void Damage(IDamageable e)
    {
        DamageInfo damageInfo = new DamageInfo(minDamage, maxDamage, transform);
        e.TakeDamage(damageInfo);
    }
}
