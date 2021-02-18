using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumpingController : MonoBehaviour, IJumpingController
{
    [SerializeField]
    float jumpPressedRemember = 0.2f;
    float jumpPressedRememberTimer;
    [SerializeField]
    float groundedRemember = 0.2f;
    float groundedRememberTimer;
    [SerializeField]
    PlayerStats stats;
    Rigidbody2D rb;
    [SerializeField] [Range(0, 1)]
    float cutOfJumpHeight = 0.85f;
    IGroundedChecking groundedChecker;
    private WallClimbing wallClimbing;
    private bool isWallJumping = false;
    [SerializeField] private float wallJumpTime = 0.1f;
    [SerializeField] private PlayerState playerState;
    private float wallJumpDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundedChecker = GetComponent<IGroundedChecking>();
        wallClimbing = GetComponent<WallClimbing>();
    }

    public void ManageJumping()
    {
        groundedRememberTimer -= Time.deltaTime;
        if (groundedChecker.IsGrounded() || wallClimbing.IsSliding)
            groundedRememberTimer = groundedRemember;

        jumpPressedRememberTimer -= Time.deltaTime;
        if (isWallJumping)
        {
            rb.velocity = new Vector2(wallJumpDirection*stats.jumpHeight*0.6f, stats.jumpHeight*0.6f);
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
                jumpPressedRememberTimer = jumpPressedRemember;

            if (groundedRememberTimer > 0f && jumpPressedRememberTimer > 0f)
            {
                jumpPressedRememberTimer = 0f;
                groundedRememberTimer = 0f;
                isWallJumping = isWallJumping || wallClimbing.IsSliding;
                if (isWallJumping)
                {
                    playerState.IsAbleToMove = false;
                    wallJumpDirection = Input.GetAxisRaw("Horizontal") * -1;
                    Invoke(nameof(FinishWallJump), wallJumpTime);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, stats.jumpHeight);
                }

                AudioManager.instance.PlaySound("Jump");
            }

            if (Input.GetButtonUp("Jump"))
            {
                if (rb.velocity.y > 0f)
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutOfJumpHeight);
            }
        }
    }

    private void FinishWallJump()
    {
        playerState.IsAbleToMove = true;
        isWallJumping = false;
    }
}
