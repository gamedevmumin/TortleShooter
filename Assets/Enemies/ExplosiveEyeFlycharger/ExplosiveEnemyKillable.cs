using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
public class ExplosiveEnemyKillable : MonoBehaviour, IKillable
{
    EnemyStats stats;
    ScreeneFreezer screenFreezer;
    [SerializeField]
    FloatVariable freezeTime;
    Animator anim;
    [SerializeField]
    List<Collider2D> colls;

    [SerializeField]
    Transform firePoint;
    [SerializeField]
    Bullet bullet;

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        screenFreezer = FindObjectOfType<ScreeneFreezer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (stats.CurrentHP <= 0 && !stats.IsDead) Die();
    }

    public void Die()
    {
        if (!stats.IsDead)
        {
            stats.IsDead = true;
            anim.SetBool("isDead", stats.IsDead);
            transform.Rotate(0, 0, -90);
            screenFreezer.Freeze(freezeTime.Value);
            AudioManager.instance.PlaySound("Death");
            Destroy(gameObject);
            for(int i = 0; i<4; i++)
            {
                Bullet newBullet = Instantiate(bullet, firePoint.position, Quaternion.Euler(new Vector3(0, 0, 45f + 90f * i))) as Bullet;

                
            }
            foreach (Collider2D coll in colls)
            {
                if (coll) coll.enabled = false;
            }
        }
    }


}
