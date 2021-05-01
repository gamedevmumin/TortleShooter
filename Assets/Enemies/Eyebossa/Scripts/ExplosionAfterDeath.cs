using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAfterDeath : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float power = 40f;
    [SerializeField] private List<Rigidbody2D> chunksRigidbodies;

    private void OnEnable()
    {
        var explosionPos = transform.position;
        foreach (var chunkRb in chunksRigidbodies)
        {
            var chunkRbTransform = chunkRb.transform;
           // chunkRbTransform.parent = null;
            Vector2 dir =  chunkRbTransform.position - transform.position;
            Debug.Log(chunkRb.name + dir);
            chunkRb.AddForce(power * dir.normalized);

        }
    }
}
