using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFlyerMovement : MonoBehaviour, IMovement
{
    Rigidbody2D rb;
    EnemyStats stats;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
    }

    public void Move(Vector2 input)
    {
        rb.velocity = input * stats.Speed * Time.fixedDeltaTime;
    }
}
