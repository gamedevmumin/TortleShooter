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
    [SerializeField]
    LayerMask whatIsGround;
    Rigidbody2D rb;
    GameObject groundCheck;
    bool isGrounded = true;
    [SerializeField] [Range(0, 1)]
    float cutOfJumpHeight = 0.85f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck").gameObject;
        if (groundCheck == null) Debug.LogError("Can't find ground check!");
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, whatIsGround);
    }
    public void ManageJumping()
    {
        groundedRememberTimer -= Time.deltaTime;
        if (isGrounded)
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
