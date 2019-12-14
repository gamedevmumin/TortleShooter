﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EyeFlyerAIManager : MonoBehaviour, IAIManager
{
    Transform playerTransform;

    Vector2 moveDirection;

    [SerializeField]
    float attackTime;
    float attackTimer;

    Animator anim;

    [SerializeField]
    Bullet bullet;
    [SerializeField]
    Transform firePoint;

    EnemyStats stats;

    IMovementManager movementManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        GameObject player = GameObject.Find("Player");
        if (player) playerTransform = player.transform;
        stats = GetComponent<EnemyStats>();
        movementManager = GetComponent<IMovementManager>();
    }

    public void ManageAI()
    {
        if (!stats.IsDead)
        {
            movementManager.ManageMovement();
            ManageAttack();
        }
    }

    void ManageAttack()
    {
        if (attackTimer <= 0f)
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
        if (playerTransform)
        {
            if(anim)anim.SetTrigger("attack");
            
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(0.08f);
        moveDirection = -transform.position + playerTransform.position;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        Instantiate(bullet, firePoint.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle)));
        AudioManager.instance.PlaySound("FireBolt");
    }

}
