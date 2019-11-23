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
            PlayerController player = coll.GetComponent<PlayerController>();
            player.TakeDamage(3000, transform);
        }
        Destroy(coll.gameObject);
    }
}
