using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AddForceMovement : MonoBehaviour, IMovement
{
    [SerializeField] private PidController pidController;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pidController.Reset();
    }

    private static float GetWantedVelocity(Vector2 input, float speed)
    {
        if (input.x > 0) return speed;
        if (input.x < 0) return -speed;
        return 0;
    }
    
    
    public void Move(Vector2 input, float speed)
    {
        var wantedVelocity = GetWantedVelocity(input, speed);
        var change = pidController.Update(wantedVelocity - rb.velocity.x, Time.deltaTime);
        rb.AddForce(new Vector2(change, 0), ForceMode2D.Force);
        Debug.Log(rb.velocity.x);
        //var previousVelocity = rb.velocity;
        // Debug.Log(input.x);
        //rb.velocity = new Vector2(previousVelocity.x * input.x, previousVelocity.y);
    }
}
