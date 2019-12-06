using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour, IDashing
{
    [SerializeField]
    PlayerStats stats;
    bool dashed = false;
    CameraShake cameraShake;
    Rigidbody2D rb;

    [SerializeField]
    float dashTime = 0.15f;

    IGroundedChecking groundedChecker;

    [SerializeField]
    int maxDashes = 1;
    int remainingDashes;

    PlayerDamageable playerDamageable;

    void Start()
    {
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        groundedChecker = GetComponent<IGroundedChecking>();
        rb = GetComponent<Rigidbody2D>();
        remainingDashes = maxDashes;
        playerDamageable = GetComponent<PlayerDamageable>();
    }

    void Update()
    {
        if(groundedChecker.IsGrounded())
        {
            RefillDashes();
        }
    }
    public void Dash()
    {       
        if (!dashed && remainingDashes > 0)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        cameraShake.Shake(0.01f, 0.01f);
        AudioManager.instance.PlaySound("Dash");
        stats.speed.Value += 605;      
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 5.45f);
        dashed = true;
        remainingDashes--;
        playerDamageable.StartInvincibilityTimer(dashTime);
        yield return new WaitForSeconds(dashTime);
        stats.speed.Value -= 605;
        yield return new WaitForSeconds(0.03f);
        dashed = false;        
    }

    public void RefillDashes()
    {
        remainingDashes = maxDashes;
    }
}
