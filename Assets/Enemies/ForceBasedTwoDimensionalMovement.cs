using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBasedTwoDimensionalMovement : MonoBehaviour, IMovement
{
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 input, float speed)
    {
        AccelerateTo(input * speed, 5f);
  
    }

    private void AccelerateTo(Vector2 targetVelocity, float maxAccel)
    {
        Vector2 deltaV = targetVelocity - rb.velocity;
        Vector2 accel = deltaV / Time.fixedDeltaTime;
        if (accel.sqrMagnitude > maxAccel * maxAccel)
        {
            accel = accel.normalized * maxAccel;
        }
        rb.AddForce(accel, ForceMode2D.Force);
    }
}


