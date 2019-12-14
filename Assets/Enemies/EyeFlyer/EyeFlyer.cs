﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFlyer : Enemy
{

    CameraShake cameraShake;
    Rigidbody2D rb;

    Transform playerPosition;

    [SerializeField] float speed;

    Animator anim;

    [SerializeField]
    float attackTime;
    float attackTimer;

    [SerializeField]
    Bullet bullet;

    Transform firePoint;

    bool isOnRight = true;

    [SerializeField]
    DamageIndicatorSpawner effectSpawner;

    ScreeneFreezer screenFreezer;
    [SerializeField]
    FloatVariable freezeTime;

    Vector2 moveDirection;

    private void Awake()
    {
        screenFreezer = FindObjectOfType<ScreeneFreezer>();
        sR = GetComponent<SpriteRenderer>();
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        anim = GetComponent<Animator>();
        firePoint = transform.Find("FirePoint").transform;
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        if (player) playerPosition = player.transform;
    }

    // Use this for initialization
    void Start()
    {
        attackTimer = attackTime;
        currentHealth = maxHealth;
        isDead = false;               
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && !isDead) Die();
        if(!isDead)
        {
            ManageMovement();
            ManageAttack();
        }
    }

    override public void TakeDamage(DamageInfo damageInfo)
    {
        if (!isDead)
        {
            Vector2 scale = new Vector2((float)damageInfo.damageDone/ (float)damageInfo.maxDamage , 
                (float)damageInfo.damageDone / (float)damageInfo.maxDamage);
           effectSpawner.SpawnEffect(damageInfo.damageDealer.position, scale, damageInfo);
            //cameraShake.Shake(0.05f, 0.1f);
            currentHealth -= damageInfo.damageDone;
            if (currentHealth > 0f) AudioManager.instance.PlaySound("Hurt");
            StartCoroutine("Blink");
        }

    }

    void ManageAttack()
    {
        if(attackTimer<=0f)
        {
            attack();
            attackTimer = attackTime;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void attack()
    {
        if (playerPosition)
        {
            anim.SetTrigger("attack");
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(0.08f);
        Vector2 moveDirection = -transform.position + playerPosition.position;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Instantiate(bullet, firePoint.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle)));
        AudioManager.instance.PlaySound("FireBolt");
    }

    void ManageMovement()
    {
        if (playerPosition!=null)
        {
            moveDirection = -transform.position + playerPosition.position;
            //czasem sie bedzie bugowac
            if (moveDirection.x > 0 && !isOnRight)
            {
                isOnRight = !isOnRight;
                transform.Rotate(0f, 180f, 0f);
            }
            else if (moveDirection.x < 0 && isOnRight)
            {
                isOnRight = !isOnRight;
                transform.Rotate(0f, 180f, 0f);
            }
           
        }
    }

    private void FixedUpdate()
    {
        if(!isDead)
        rb.velocity = moveDirection.normalized * speed * Time.fixedDeltaTime;
    }

    override public void Die()
    {
        if (!isDead)
        {
            isDead = true;
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