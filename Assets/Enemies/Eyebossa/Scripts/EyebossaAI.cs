using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EyebossaAI : MonoBehaviour
{

    [SerializeField] private List<Transform> bulletsSpawningPositions;
    [SerializeField] private List<Transform> downBulletsSpawningPositions;
    [SerializeField] private GameObject bulletPrefab;
    private Rigidbody2D rb;
    private int direction = 1;
    
    private float rotationCounter = 1;
    private float rotationChange = 1f;
    private enum State
    {
        DoingNothing,
        ShootingAround,
        ShootingDown
    }

    private State currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        currentState = State.ShootingDown;
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
                }
                else if (rotationChange < 0f && rotationCounter > 80)
                {
                    rotationCounter = 79f;
                    rotationChange *= -1f;
                }
                break;
            case State.ShootingDown:
                rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z + rotationCounter);
                transform.rotation = rotation;
                rotationCounter -= rotationChange;
                if (rotationChange > 0f && rotationCounter < -25)
                {
                    rotationCounter = -24f;
                    rotationChange *= -1f;
                }
                else if (rotationChange < 0f && rotationCounter > 25)
                {
                    rotationCounter = 24f;
                    rotationChange *= -1f;
                }
                rb.velocity = new Vector2(0f, direction*1.2f);
                break;
        }
    }

    public void ShootAround()
    {
        foreach (var bsPos in bulletsSpawningPositions)
        {
            Instantiate(bulletPrefab, bsPos.position, bsPos.rotation);
        }
        
    }
    
    public void ShootDown()
    {
        foreach (var bsPos in downBulletsSpawningPositions)
        {
            Instantiate(bulletPrefab, bsPos.position, bsPos.rotation);
        }
        
    }
    
    public Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"DirectionChanger"))
        {
            direction *= -1;
        }
    }
}
