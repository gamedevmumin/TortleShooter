using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public bool isDead;

	protected SpriteRenderer sR;

	protected int  currentHealth;
	[SerializeField]
	protected int maxHealth = 100;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	virtual protected IEnumerator Blink()
	{
		sR.material.SetFloat("_FlashAmount", 1f);
		yield return new WaitForSeconds(0.03f);
		sR.material.SetFloat("_FlashAmount", 0f);
	}

	virtual public void TakeDamage(DamageInfo damageInfo)
	{
		AudioManager.instance.PlaySound("Hurt");
	}

	virtual public void Die()
	{
		isDead = true;
	}
}

