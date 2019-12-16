using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EyeFlyerMovementManager : MonoBehaviour, IMovementManager
{

    EnemyStats stats;
    IMovement movement;
    Vector2 movementDirection;
    Transform playerTransform;
    bool isPlayerOnRight;

    [SerializeField]
    SceneContents sceneContents;

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        movement = GetComponent<IMovement>();
        GameObject player = GameObject.Find("Player");
        if (player) playerTransform = player.transform;
    }

    void Start()
    {
        isPlayerOnRight = true;
    }

    void FixedUpdate()
    {
        if (!stats.IsDead)
            movement.Move(movementDirection.normalized);

    }

    public void ManageMovement()
    {
        if (playerTransform != null)
        {
            movementDirection = GetMovementDirection();

                if (movementDirection.x > 0 && !isPlayerOnRight)
                {
                    isPlayerOnRight = !isPlayerOnRight;
                    transform.Rotate(0f, 180f, 0f);
                }
                else if (movementDirection.x < 0 && isPlayerOnRight)
                {
                    isPlayerOnRight = !isPlayerOnRight;
                    transform.Rotate(0f, 180f, 0f);
                }
            
        }
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 movementDirection = Vector3.zero;
        
        foreach(PlayerController player in sceneContents.Players)
        {
            Vector3 dir = -transform.position + player.transform.position;
            float distance = dir.magnitude;
            float springStrength = 1f;
            movementDirection += dir * springStrength;
        }
        if (movementDirection.magnitude > 2f)
        {
            foreach (Enemy enemy in sceneContents.Enemies)
            {
                Vector3 dir = -transform.position + enemy.transform.position;
                float distance = dir.magnitude;
                float springStrength = 2.2f / (1f + distance * distance * distance);
                movementDirection -= dir * springStrength;
            }
        }
        
        return movementDirection;
    }
}
