using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime=0.5f;

    IMovement movement;
    IDestructible destructible;


    private void Awake()
    {
        movement = GetComponent<IMovement>();
        destructible = GetComponent<IDestructible>();
    }

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);              
    }

    void DestroyProjectile()
    {
        destructible.Destroy(null);
    }

    private void FixedUpdate()
    {
        movement.Move(transform.right, speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {       
            if (coll.gameObject.tag == "Ground")
            {
                DestroyProjectile();
            }
        
    }
}
