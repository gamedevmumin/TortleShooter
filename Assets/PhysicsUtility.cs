﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsUtility {
    public static void ApplyForceToReachVelocity (Rigidbody2D rigidbody, Vector3 velocity, float force = 1, ForceMode2D mode = ForceMode2D.Force) {
        if (force == 0 || velocity.magnitude == 0) return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        force = Mathf.Clamp (force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        if (rigidbody.velocity.magnitude == 0) {
            rigidbody.AddForce (velocity * force, mode);
        } else {
            var velocityProjectedToTarget = (velocity.normalized * Vector2.Dot (velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce ((velocity + velocityProjectedToTarget) * force, mode);
        }
    }
}