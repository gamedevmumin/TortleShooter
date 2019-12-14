using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EyeFlyerMovementManager : MonoBehaviour, IMovementManager
{

    EnemyStats stats;
    IMovement movement;
    Vector2 moveDirection;
    Transform playerTransform;
    bool isPlayerOnRight;
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
            movement.Move(moveDirection);

    }

    public void ManageMovement()
    {
        if (playerTransform != null)
        {
            moveDirection = -transform.position + playerTransform.position;
            if (moveDirection.x > 0 && !isPlayerOnRight)
            {
                isPlayerOnRight = !isPlayerOnRight;
                transform.Rotate(0f, 180f, 0f);
            }
            else if (moveDirection.x < 0 && isPlayerOnRight)
            {
                isPlayerOnRight = !isPlayerOnRight;
                transform.Rotate(0f, 180f, 0f);
            }

        }
    }
}
