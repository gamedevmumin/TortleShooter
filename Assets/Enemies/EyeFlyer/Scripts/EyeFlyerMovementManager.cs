using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EyeFlyerMovementManager : MonoBehaviour, IMovementManager
{

    EnemyStats stats;
    IMovement movement;
    Vector2 movementDirection;
    bool isPlayerOnRight;

    [SerializeField]
    SceneContents sceneContents;

    bool needsToChangeDirection = false;

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        movement = GetComponent<IMovement>();
    }

    void Start()
    {
        isPlayerOnRight = true;
    }

    void FixedUpdate()
    {
        if (!stats.IsDead)
            movement.Move(movementDirection.normalized, stats.Speed);

    }

    public void ManageMovement()
    {

                if(needsToChangeDirection == false) movementDirection = GetMovementDirection();

                if (movementDirection.x > 0 && !isPlayerOnRight)
                {
                    isPlayerOnRight = !isPlayerOnRight;
                    if (needsToChangeDirection == false) StartCoroutine(changeDirection());
                }
                else if (movementDirection.x < 0 && isPlayerOnRight)
                {
                    isPlayerOnRight = !isPlayerOnRight;
                    if (needsToChangeDirection == false) StartCoroutine(changeDirection());
                }
            
        
    }

    IEnumerator changeDirection()
    {
        needsToChangeDirection = true;
        yield return new WaitForSeconds(0.1f);
        needsToChangeDirection = false;
        transform.Rotate(0f, 180f, 0f);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 movementDirection = Vector3.zero;
        float distance;
        float springStrength;
        Vector3 dir;
        foreach (PlayerController player in sceneContents.Players)
        {
            dir = -transform.position + player.transform.position;
            distance = dir.magnitude;
            springStrength = 1f;
            movementDirection += dir * springStrength;
        }
        if (movementDirection.magnitude > 2.5f)
        {
            foreach (Enemy enemy in sceneContents.Enemies)
            {
                dir = -transform.position + enemy.transform.position;
                distance = dir.magnitude;
                springStrength = 2.2f / (1f + distance * distance * distance);
                movementDirection -= dir * springStrength;
            }
        }
        
        return movementDirection;
    }
}
