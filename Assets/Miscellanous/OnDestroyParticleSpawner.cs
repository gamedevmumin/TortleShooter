using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyParticleSpawner : MonoBehaviour, IDestructible
{

    [SerializeField]
    Transform particleSpawnPlace;
    [SerializeField]
    ParticleSystem particle;

    public void Destroy(Transform destructionReason)
    {
        Instantiate(particle, particleSpawnPlace.position, particleSpawnPlace.rotation);
        Destroy(gameObject);
    }

}
