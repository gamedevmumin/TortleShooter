using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedChecker : MonoBehaviour, IGroundedChecking
{
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    GameObject groundCheck;
    bool isGrounded = true;

    void Start()
    {
        if (groundCheck == null) Debug.LogError("groundCheck variable isn't set");
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, whatIsGround);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
