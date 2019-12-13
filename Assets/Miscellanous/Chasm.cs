using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            IDamageable player = coll.GetComponent<IDamageable>();
            Rigidbody2D rb = coll.GetComponent<Rigidbody2D>();
            if(rb)
            {
                Debug.Log("Chasmeeed");
                rb.velocity = new Vector2(rb.velocity.x, 12f);
            }
            if(player!=null)player.TakeDamage(1);
        }
        else Destroy(coll.gameObject);
    }
}
