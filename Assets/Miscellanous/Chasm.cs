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
                rb.velocity = new Vector2(rb.velocity.x, 12f);
            }
            if (player != null)
            {
                DamageInfo damageInfo = new DamageInfo();
                damageInfo.damageDone = 1;
                player.TakeDamage(damageInfo);
            }
        }
        else Destroy(coll.gameObject);
    }
}
