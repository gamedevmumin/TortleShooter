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
            player.TakeDamage(3000);
        }
        Destroy(coll.gameObject);
    }
}
