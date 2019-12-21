using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime=0.5f;

    Rigidbody2D rb;

    [SerializeField]
    int maxDamage = 15;
    [SerializeField]
    int minDamage = 10;

    Transform hitPoint;

    [SerializeField]
    GameObject hitParticle;

    bool canDoDamage = true;

    [SerializeField]
    bool isPlayersBullet = true;

    [SerializeField]
    bool isPhysical = true;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        rb = GetComponent<Rigidbody2D>();
        if(isPhysical) rb.velocity = transform.right * speed;
        hitPoint = transform.Find("HitPoint").transform;
       
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (canDoDamage == true)
        {
            if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Runner")
            {
                if (isPlayersBullet)
                {
                    IDamageable e = coll.GetComponent<IDamageable>();
                    if(!coll.GetComponent<EnemyStats>().IsDead)
                    DamageEnemy(e);
                }
            }
            else if (coll.gameObject.tag == "Ground")
            {
                DestroyProjectile();
            }
            else if(coll.gameObject.tag == "Player")
            {
                if (!isPlayersBullet)
                {
                    PlayerDamageable p = coll.GetComponent<PlayerDamageable>();
                    if (p && p.InvincibilityTimer <= 0f)
                    {
                        DamagePlayer(p);
                    }
                }
            }
        }
    }

    void DamageEnemy(IDamageable e)
    {
        DamageInfo damageInfo = new DamageInfo();
        damageInfo.minDamage = minDamage;
        damageInfo.maxDamage = maxDamage;
        damageInfo.damageDone = Random.Range(minDamage, maxDamage);
        damageInfo.damageDealer = transform;
        e.TakeDamage(damageInfo);

            canDoDamage = false;
            DestroyProjectile();
        
    }

    void DamagePlayer(IDamageable p)
    {
        DamageInfo damageInfo = new DamageInfo();
        damageInfo.minDamage = minDamage;
        damageInfo.maxDamage = maxDamage;
        damageInfo.damageDone = Random.Range(minDamage, maxDamage);
        damageInfo.damageDealer = transform;
        p.TakeDamage(damageInfo);
      
        canDoDamage = false;
        DestroyProjectile();
        
    }
    void DestroyProjectile()
    {
        Instantiate(hitParticle, hitPoint.position, hitPoint.rotation);
        Destroy(gameObject);
    }
}
