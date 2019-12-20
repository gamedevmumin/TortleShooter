using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRunnerMovementManager : MonoBehaviour, IMovementManager
{
    enum Direction { RIGHT, LEFT };
    Direction direction = Direction.RIGHT;

    IMovement movement;

    EnemyStats stats;

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        movement = GetComponent<IMovement>();
    }

    void Start()
    {
        double dir = Random.Range(-1f, 1f);
        if (dir > 0f)
        {
            direction = Direction.RIGHT;
        }
        else
        {
            transform.Rotate(0f, 180f, 0f);
            direction = Direction.LEFT;
        }
        ChooseDirection();
    }

    void ChooseDirection()
    {
        if (direction == Direction.RIGHT) movement.Move(new Vector2(1f, 0f), stats.Speed);
        else if (direction == Direction.LEFT) movement.Move(new Vector2(-1f, 0f), stats.Speed);
    }

    public void ChangeDirection()
    {
        if (direction == Direction.RIGHT) direction = Direction.LEFT;
        else direction = Direction.RIGHT;
        transform.Rotate(0f, 180f, 0f);
        //ChooseDirection();
    }

    public void ManageMovement()
    {
        ChooseDirection();
    }

}
