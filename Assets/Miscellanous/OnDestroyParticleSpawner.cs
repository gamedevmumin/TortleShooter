using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyParticleSpawner : MonoBehaviour, IDestructible
{

    [SerializeField]
    Transform particleSpawnPlace;
    [SerializeField]
    GameObject particle;
    [SerializeField]
    string destructionSound = "";

    [SerializeField]
    bool isShakingOnDestruction = false;
    [SerializeField]
    float shakeAmount = 0.05f;

    CameraShake cameraShake;

    public void Destroy(Transform destructionReason)
    {
        Instantiate(particle, particleSpawnPlace.position, particleSpawnPlace.rotation);
        if (destructionSound != "") AudioManager.instance.PlaySound(destructionSound);
        if (isShakingOnDestruction)
        {
            cameraShake = FindObjectOfType<CameraShake>();
            if (cameraShake) cameraShake.Shake(shakeAmount, 0.05f);
        }
        Destroy(gameObject);
    }

}
