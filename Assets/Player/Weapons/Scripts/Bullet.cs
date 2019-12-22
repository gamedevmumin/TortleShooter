using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime=0.5f;

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

    IMovement movement;  

    [SerializeField]
    List<string> targetedTags;

    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        hitPoint = transform.Find("HitPoint").transform;
       
    }

    private void Update()
    {
        if (isPhysical) movement.Move(transform.right, speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (canDoDamage == true)
        {
            foreach(string tag in targetedTags)
            {
                if(coll.gameObject.tag == tag)
                {
                    IDamageable d = coll.GetComponent<IDamageable>();
                    if (d.IsDamageable)
                    {
                        Damage(d);
                        DestroyProjectile();
                    }
                    break;
                }
            }
            if (coll.gameObject.tag == "Ground")
            {
                DestroyProjectile();
            }
        }
    }

    void Damage(IDamageable e)
    {
        DamageInfo damageInfo = new DamageInfo(minDamage, maxDamage, transform);
        e.TakeDamage(damageInfo);
        canDoDamage = false;          
    }

    void DestroyProjectile()
    {
        Instantiate(hitParticle, hitPoint.position, hitPoint.rotation);
        Destroy(gameObject);
    }
}
