using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjective : MonoBehaviour {

	[SerializeField] GameObject collectionEffect;

	CollectorManager collectorManager;

	public void initialize(CollectorManager collectorManager)
	{
		this.collectorManager = collectorManager;
	}

	bool collected = false;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (collected == false)
		{
			if (coll.CompareTag("Player"))
			{
				collected = true;
				AudioManager.instance.PlaySound("Collect");
				Instantiate(collectionEffect, transform.position, transform.rotation);
				collectorManager.OnCollection();
				Destroy(gameObject);
			}
		}

	}

}
