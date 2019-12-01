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
    int damage = 10;

    Transform hitPoint;

    [SerializeField]
    GameObject hitParticle;

    bool canDoDamage = true;

    [SerializeField]
    bool isPlayersBullet = true;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        hitPoint = GetComponentInChildren<Transform>();
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (canDoDamage == true)
        {
            if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Runner")
            {
                if (isPlayersBullet)
                {
                    Enemy e = coll.GetComponent<Enemy>();
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
                    DamagePlayer(p);
                }
            }
        }
    }

    void DamageEnemy(Enemy e)
    {
        e.TakeDamage(damage, transform);
        if (!e.isDead)
        {
            canDoDamage = false;
            DestroyProjectile();
        }
    }

    void DamagePlayer(IDamageable p)
    {
        p.TakeDamage(damage);
      
            canDoDamage = false;
            DestroyProjectile();
        
    }
    void DestroyProjectile()
    {
        Instantiate(hitParticle, hitPoint.position, hitPoint.rotation);
        Destroy(gameObject);
    }
}
