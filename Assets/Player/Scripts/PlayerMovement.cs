using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IMovement
{
    Rigidbody2D rb;
    [SerializeField]
    PlayerStats stats;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(float input)
    {
        if (Mathf.Abs(input) > 0f)
        {
            rb.velocity = new Vector2(input * stats.speed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
}
