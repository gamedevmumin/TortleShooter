using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VerticalMovement : MonoBehaviour, IMovement
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 input, float speed)
    {
        rb.velocity = new Vector2(input.x * speed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
