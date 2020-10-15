using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTouchDamage : MonoBehaviour {
    int criticalStrikeChance = 1;
    int minDamage;
    int maxDamage;
    [SerializeField]
    List<string> targetedTags;
    IDestructible destructible;
    private void Awake () {
        destructible = GetComponent<IDestructible> ();
    }

    public void Initialize (int minDamage, int maxDamage, int criticalStrikeChance) {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.criticalStrikeChance = criticalStrikeChance;
    }

    void OnTriggerEnter2D (Collider2D coll) {
        foreach (string tag in targetedTags) {
            if (coll.gameObject.tag == tag) {
                IDamageable d = coll.GetComponent<IDamageable> ();
                if (d != null && d.IsDamageable) {
                    Damage (d);
                    if (destructible != null) destructible.Destroy (null);
                }
                break;
            }
        }
    }

    void Damage (IDamageable e) {
        int randomNumber = Random.Range(0, 100);
        bool wasCritical = randomNumber <= criticalStrikeChance;
        DamageInfo damageInfo = new DamageInfo (minDamage, maxDamage, transform, wasCritical);
        e.TakeDamage (damageInfo);
    }
}