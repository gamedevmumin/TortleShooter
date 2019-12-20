using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRunnerAIManager : MonoBehaviour
{

    IMovementManager movementManager;

    private void Awake()
    {
        movementManager = GetComponent<IMovementManager>();
    }

    void Update()
    {
        movementManager.ManageMovement();
    }
}
