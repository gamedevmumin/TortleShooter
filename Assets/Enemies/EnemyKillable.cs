using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
public class EnemyKillable : MonoBehaviour, IKillable
{
    EnemyStats stats;
    Rigidbody2D rb;
    ScreeneFreezer screenFreezer;
    [SerializeField]
    FloatVariable freezeTime;
    SpriteRenderer sR;
    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        sR = GetComponent<SpriteRenderer>();
        screenFreezer = FindObjectOfType<ScreeneFreezer>();
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
                transform.Rotate(0, 0, -90);
                screenFreezer.Freeze(freezeTime.Value);
                AudioManager.instance.PlaySound("Death");
                if (rb)
                {
                    rb.AddForce(new Vector2(-rb.velocity.x * 100f, 400f));
                    rb.gravityScale = 3.5f;
                }
                sR.material.color = new Color32(65, 58, 58, 255);
            }
        
    }

    
}
