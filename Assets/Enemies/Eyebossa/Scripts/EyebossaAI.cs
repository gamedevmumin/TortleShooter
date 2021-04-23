using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EyebossaAI : MonoBehaviour
{
    [SerializeField] private ScreeneFreezer screenFreezer;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private List<Transform> bulletsSpawningPositions;
    
    [SerializeField] private List<Transform> bulletsPositionsWhenNearGround;
    [SerializeField] private List<Transform> restOfTheBullets;
    
    [SerializeField] private List<Transform> downBulletsSpawningPositions;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform groundScanPoint;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Animator anim;

    [SerializeField] private AcceleratedMovement acceleratedMovement;
    
    private int counter = 0;
    private int bounceCounter = 0;

    private bool hitTheGround = false;
    
    private bool isGroundBeneath = false;
    
    private Rigidbody2D rb;
    private int direction = 1;
    
    private float rotationCounter = 1;
    private float rotationChange = 1f;
    private enum State
    {
        DoingNothing,
        ShootingAround,
        ShootingDown,
        ChargingDown
    }

    private State currentState;
    private static readonly int UpDownPhase = Animator.StringToHash("UpDownPhase");
    private static readonly int RotatingPhase = Animator.StringToHash("RotatingPhase");
    private static readonly int Anger1 = Animator.StringToHash("Anger");
    private static readonly int ChargeDown = Animator.StringToHash("ChargeDown");

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        currentState = State.DoingNothing;
        anim.SetBool(RotatingPhase, true);
        StartCoroutine(StartBattle());
    }


    private void FixedUpdate()
    {
        var rotation = transform.rotation;
        switch (currentState)
        {
            case State.DoingNothing:
                break;
            case State.ShootingAround:
                rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z + rotationCounter);
                transform.rotation = rotation;
                rotationCounter -= rotationChange;
                if (rotationChange > 0f && rotationCounter < -60)
                {
                    rotationCounter = -59f;
                    rotationChange *= -1f;
                    counter++;
                }
                else if (rotationChange < 0f && rotationCounter > 80)
                {
                    rotationCounter = 79f;
                    rotationChange *= -1f;
                    counter++;
                }

                if (counter == 3)
                {
                    currentState = State.ShootingDown;
                    anim.SetBool(UpDownPhase, true);
                    counter = 0;
                }
                break;
            case State.ShootingDown:
                isGroundBeneath =  IsGroundBeneath();
                rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z + rotationCounter);
                transform.rotation = rotation;
                rotationCounter -= rotationChange;
                if (rotationChange > 0f && rotationCounter < -35)
                {
                    rotationCounter = -34f;
                    rotationChange *= -1f;
                }
                else if (rotationChange < 0f && rotationCounter > 35)
                {
                    rotationCounter = 34f;
                    rotationChange *= -1f;
                }

                if (bounceCounter == 3 && rotationCounter < 0.1f && rotationCounter > -0.1f)
                {
                    rb.velocity = Vector2.zero;
                    currentState = State.DoingNothing;
                    anim.SetTrigger(Anger1);
                    anim.SetBool(RotatingPhase, false);
                    anim.SetBool(ChargeDown, true);
                    break;
                }
                rb.velocity = new Vector2(0f, direction*1.3f);
                break;
            case State.ChargingDown:
                if (hitTheGround) break;
                if (IsGroundBeneath())
                {
                    hitTheGround = true;
                    break;
                }
                rb.velocity = new Vector2(0f, -40f); 
                //acceleratedMovement.Move(Vector2.down, 500);
                break;
            
        }
    }

    public void SwitchToChargeDown()
    {
        currentState = State.ChargingDown;
        acceleratedMovement.enabled = true;
    }
    
    public void ShootAround()
    {
       // if (!isGroundBeneath)
        {
            foreach (var bsPos in bulletsSpawningPositions)
            {
                Instantiate(bulletPrefab, bsPos.position, bsPos.rotation);
            }
        }
      //  else
        {
            /*
            foreach (var restBullet in restOfTheBullets)
            {
                Instantiate(bulletPrefab, restBullet.position, restBullet.rotation);
            }

            foreach (var nearGroundBullet in bulletsPositionsWhenNearGround.Where(nearGroundBullet => counter%2==0))
            {
                Instantiate(bulletPrefab, nearGroundBullet.position, nearGroundBullet.rotation);
            }

            counter++;*/
        }

    }

    private IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(1f);
        currentState = State.ShootingAround;
    }
    
    
    public void ShootDown()
    {
        foreach (var bsPos in downBulletsSpawningPositions)
        {
            Instantiate(bulletPrefab, bsPos.position, bsPos.rotation);
        }
        
    }

    private bool IsGroundBeneath()
    {
        var position = groundScanPoint.position;
        var hit = Physics2D.Raycast(position, -Vector2.up, 1.25f, whatIsGround);
        Debug.DrawLine(position, position - Vector3.up * 1.25f, Color.yellow, 0.01f);
        return hit.collider != null;
    }

    public void Anger()
    {
        screenFreezer.Freeze(0.03f);
        cameraShake.Shake(0.122f, 0.2f);
    }

    public Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"DirectionChanger"))
        {
            if (currentState != State.ChargingDown)
            {
                bounceCounter++;
                direction *= -1;
            }
            else
            {
                cameraShake.Shake(0.2f, 0.2f);
            }

        }
    }
}
