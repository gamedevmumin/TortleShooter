using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedMovement : MonoBehaviour, IMovement
{
    Rigidbody2D rb;
    float acceleration =1f;
    [SerializeField]
    float accelerationMax;
    [SerializeField]
    float accelerationStamp;
    [SerializeField]
    float timeToAccelerate;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        InvokeRepeating("Accelerate", 0.45f, timeToAccelerate);
    }
    public void Move(Vector2 input, float speed)
    {
        rb.velocity = input * speed * Time.fixedDeltaTime * acceleration;
    }

    void Accelerate()
    {
        if (acceleration + accelerationStamp >= accelerationMax)
        {
            CancelInvoke();
            return;
        }
        acceleration += accelerationStamp;
    }
}

