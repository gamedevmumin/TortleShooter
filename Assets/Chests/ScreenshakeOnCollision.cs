using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeOnCollision : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private Rigidbody2D rb;
    
    private int counter = 0;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (cameraShake == null) cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        if (counter == 0)
        {
            cameraShake.Shake(0.12f, 0.02f);
        }
        else
        {
            cameraShake.Shake(0.04f, 0.1f);
        }

        if (counter < 3)
        {
            AudioManager.instance.PlaySound("MetalFall");
            counter++;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
