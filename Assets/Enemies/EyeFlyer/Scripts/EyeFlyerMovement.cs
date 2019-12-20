using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFlyerMovement : MonoBehaviour, IMovement
{
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 input, float speed)
    {
        rb.velocity = input * speed * Time.fixedDeltaTime;
    }
}
