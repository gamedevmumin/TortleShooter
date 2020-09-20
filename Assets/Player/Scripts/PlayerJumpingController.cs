using System.Collections;
using System.Collections.Generic;
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
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundedChecker = GetComponent<IGroundedChecking>();
    }

    public void ManageJumping()
    {
        groundedRememberTimer -= Time.deltaTime;
        if (groundedChecker.IsGrounded())
            groundedRememberTimer = groundedRemember;

        jumpPressedRememberTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpPressedRememberTimer = jumpPressedRemember;

        if (groundedRememberTimer > 0f && jumpPressedRememberTimer > 0f)
        {
            jumpPressedRememberTimer = 0f;
            groundedRememberTimer = 0f;
            rb.velocity = new Vector2(rb.velocity.x, stats.jumpHeight);
            AudioManager.instance.PlaySound("Jump");
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (rb.velocity.y > 0f)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutOfJumpHeight);
        }
    }  
}
