using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBasedTwoDimensionalMovementV2 : MonoBehaviour, IMovement {
    Rigidbody2D rb;
    void Awake () {
        rb = GetComponent<Rigidbody2D> ();
    }

    public void Move (Vector2 input, float speed) {
        PhysicsUtility.ApplyForceToReachVelocity (rb, 6.2f * input, 100000);
    }

}