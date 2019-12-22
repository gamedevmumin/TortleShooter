using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRunner : MonoBehaviour {

	Rigidbody2D rb;
	[SerializeField]
	float speed = 10f;

	bool isDead = false;

	Animator anim;

	CameraShake cameraShake;

	BoxCollider2D bC;
	CircleCollider2D cC;
	enum Direction { RIGHT, LEFT };
	Direction direction = Direction.RIGHT;

	[SerializeField]
	DamageIndicatorSpawner effectSpawner;
	ScreeneFreezer screenFreezer;
	[SerializeField]
	FloatVariable freezeTime;


	protected SpriteRenderer sR;

	protected EnemyStats stats;

	private void Awake()
	{
		screenFreezer = FindObjectOfType<ScreeneFreezer>();
		rb = GetComponent<Rigidbody2D>();
		sR = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		bC = transform.Find("Colliders").GetComponent<BoxCollider2D>();
		cC = GetComponentInChildren<CircleCollider2D>();
		cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>(); ;
		stats = GetComponent<EnemyStats>();
	}

	// Use this for initialization
	void Start () {
		
		double dir = Random.Range(-1f, 1f);
		if(dir>0f)
		{
			direction = Direction.RIGHT;
		}
		else
		{
			transform.Rotate(0f, 180f, 0f);
			direction = Direction.LEFT;
		}       
		isDead = false;
		ChooseDirection();
	}

	private void FixedUpdate()
	{
		if(!isDead) ChooseDirection();
	}

	void Update () { 

		if(stats.CurrentHP<=0 && !isDead)
		{
			Die();
		}
	}

	void ChooseDirection()
	{
			if (direction == Direction.RIGHT) rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
			else if (direction == Direction.LEFT) rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, rb.velocity.y);       
	}

	public void ChangeDirection()
	{
		if (direction == Direction.RIGHT) direction = Direction.LEFT;
		else direction = Direction.RIGHT;
		transform.Rotate(0f, 180f, 0f);
		ChooseDirection();
	}

	protected IEnumerator Blink()
	{
		sR.material.SetFloat("_FlashAmount", 1f);
		yield return new WaitForSeconds(0.03f);
		sR.material.SetFloat("_FlashAmount", 0f);
	}

	public void TakeDamage(DamageInfo damageInfo)
	{
		if (!isDead)
		{
			Vector2 scale = new Vector2((float)damageInfo.DamageDone / (float)damageInfo.MaxDamage,
				(float)damageInfo.DamageDone / (float)damageInfo.MaxDamage);
			effectSpawner.SpawnEffect(damageInfo.DamageDealer.position, scale, damageInfo);

			stats.CurrentHP -= damageInfo.DamageDone;
			if (stats.CurrentHP > 0f) AudioManager.instance.PlaySound("Hurt");
			StartCoroutine("Blink");
		}
	   
	}

	public void Die()
	{
		if (!isDead)
		{
			isDead = true;
			transform.Rotate(0, 0, -90);
			screenFreezer.Freeze(freezeTime.Value);
			AudioManager.instance.PlaySound("Death");
			rb.AddForce(new Vector2(-rb.velocity.x * 100f, 400f));
			anim.SetBool("isDead", isDead);

			sR.material.color = new Color32(65, 58, 58, 255);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.CompareTag("Player") && !isDead)
		{
			DamageInfo damageInfo = new DamageInfo();
			damageInfo.DamageDone = 1;
			coll.GetComponent<IDamageable>().TakeDamage(damageInfo);
		}
	}

}

