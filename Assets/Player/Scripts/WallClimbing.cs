using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(IGroundedChecking))]
[RequireComponent(typeof(Rigidbody2D))]
public class WallClimbing : MonoBehaviour
{
    [SerializeField] private float slidingSpeed;

    private IGroundedChecking groundedChecking;
    private IWallCollisionChecker wallCollisionChecker;
    private bool isTouchingFront;

    public bool IsSliding { get; private set; }

    private Rigidbody2D rb;

    private void Start()
    {
        groundedChecking = GetComponent<IGroundedChecking>();
        wallCollisionChecker = GetComponent<IWallCollisionChecker>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void ManageWallClimbing(float input)
    {
        isTouchingFront = wallCollisionChecker.IsTouchingWall();

        if (isTouchingFront && !groundedChecking.IsGrounded() && input != 0)
        {
            IsSliding = true;
        }
        else
        {
            IsSliding = false;
        }

        if (IsSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slidingSpeed, float.MaxValue));
        }
    }
    
}
