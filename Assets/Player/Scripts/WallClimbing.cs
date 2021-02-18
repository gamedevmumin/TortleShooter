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
    private Animator anim;
    private bool isTouchingFront;

    public bool IsSliding { get; private set; }

    private Rigidbody2D rb;
    private static readonly int IsSlidingRight = Animator.StringToHash("isSlidingRight");
    private static readonly int IsSlidingLeft = Animator.StringToHash("isSlidingLeft");

    private void Start()
    {
        groundedChecking = GetComponent<IGroundedChecking>();
        wallCollisionChecker = GetComponent<IWallCollisionChecker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        anim.SetBool(IsSlidingRight, IsSliding && wallCollisionChecker.GetCollisionDirection() == CollisionDirection.Right);
        anim.SetBool(IsSlidingLeft, IsSliding && wallCollisionChecker.GetCollisionDirection() == CollisionDirection.Left);
        if (IsSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slidingSpeed, float.MaxValue));
        }
    }
    
}
