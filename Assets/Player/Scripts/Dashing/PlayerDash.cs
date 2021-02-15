using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour, IDashing
{
    [SerializeField] private PlayerStats stats;

    private bool dashed = false;
    private CameraShake cameraShake;
    private Rigidbody2D rb;

    [SerializeField] private float dashTime = 0.15f;

    private IGroundedChecking groundedChecker;

    [SerializeField] private int maxDashes = 1;

    private int remainingDashes;

    PlayerDamageable playerDamageable;
    [SerializeField] private ParticleSystem dashEffect;

    [SerializeField] private float rememberTime = 1f;
    private float rememberTimer;

    [SerializeField] private float distanceBetweenImages;
    private float lastImageXPos;
    private void Awake()
    {
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        groundedChecker = GetComponent<IGroundedChecking>();
        rb = GetComponent<Rigidbody2D>();        
        playerDamageable = GetComponent<PlayerDamageable>();
    }

    private void Start()
    {
        remainingDashes = maxDashes;
    }

    private void Update()
    {
        if (dashed)
        {
            if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBetweenImages)
            {
                PlayerAfterImagePool.Instance.GetFromPool();
                lastImageXPos = transform.position.x;
            }
        }
        if(groundedChecker.IsGrounded() && dashed == false)
        {
            RefillDashes();
        }
        if (rememberTimer > 0f && Mathf.Abs(rb.velocity.x) > 0f)
        {
            rememberTimer = 0f;
            StartCoroutine(DashCoroutine());
        }
        rememberTimer -= Time.deltaTime;
    }
    public void Dash()
    {       
        if (!dashed && remainingDashes > 0)
        {
            rememberTimer = rememberTime;            
        }
    }

    IEnumerator DashCoroutine()
    {
        //dashEffect.Play();
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXPos = transform.position.x;
        cameraShake.Shake(0.018f, 0.015f);
        AudioManager.instance.PlaySound("Dash");
        stats.speed.Value += 605;      
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 5.45f);
        dashed = true;
        remainingDashes--;
        playerDamageable.StartInvincibilityTimer(dashTime+0.015f);      
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
