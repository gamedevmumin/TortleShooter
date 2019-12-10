using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

	public Rigidbody2D rb { get; private set; }
	Animator anim;
	bool isRight = true;
	[SerializeField]
	LayerMask whatIsPlatform;   
	[SerializeField]
	TimerUI timer;
	[SerializeField]
	float fallingTime = 0.2f;
	float fallingTimer;
	[SerializeField]
	PlayerStats stats;
	[SerializeField]
	PlayerStats startingStats;
	public PlayerStats Stats { get { return stats; } }
	bool isDead;
	bool inertia = false;
	SpriteRenderer sR;
	GameObject colliders;
	static PlayerController instance;
	PlayerWeapons playerWeapons;
	[SerializeField]
	List<Transform> groundChecks;

	IMovement movementController;
	IJumpingController jumpingController;
	IDashing dashingController;
	PlayerDamageable playerDamageable;

	void Awake () {    
			sR = GameObject.Find("PlayerGraphics").GetComponent<SpriteRenderer>();
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();                                          
			colliders = transform.Find("Colliders").gameObject;			
			playerWeapons = GetComponent<PlayerWeapons>();            
			movementController = GetComponent<IMovement>();
			jumpingController = GetComponent<IJumpingController>();
			dashingController = GetComponent<IDashing>();
		playerDamageable = GetComponent<PlayerDamageable>();
	}

	private void Start()
	{
		if (BetweenLevelDataContainer.instance.FirstScene)
		{
			stats.Set(startingStats);
			stats.currentHP = stats.maxHP;
		}
		isDead = false;
		fallingTimer = fallingTime;
	}
	void Update () {
		if (stats.currentHP <= 0f) Die();
		if (!inertia)
		{
			if (!isDead)
			{
				ManageMovement();
				ManageAnimation();
				ManageSwitchingWeapons();
			}
		}       
	}

	void ManageSwitchingWeapons()
	{
		if(Mathf.Abs(Input.mouseScrollDelta.y)>0f)
		{
			playerWeapons.SwitchWeapon();
		}
	}

	void ManageAnimation()
	{
		anim.SetFloat("vSpeed", Mathf.Abs(rb.velocity.x));
	}

	float movementInput;
	void ManageMovement()
	{
		movementInput = Input.GetAxisRaw("Horizontal");       
	   jumpingController.ManageJumping();
		if (Input.GetKeyDown(KeyCode.LeftShift)) dashingController.Dash();
		ManageDirection();
		ManagePlatforms();
	}

	private void FixedUpdate()
	{
		movementController.Move(movementInput);
	}

	void Flip()
	{
		isRight = !isRight;
		sR.flipX = !sR.flipX;       
	}

	void ManageDirection()
	{
		if ((rb.velocity.x < 0 && isRight) || (rb.velocity.x > 0 && !isRight))
			Flip();
	}

	void ManagePlatforms()
	{
		if (Input.GetButton("Down"))
		{
			foreach (Transform groundCheck in groundChecks)
			{
				RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, 0.2f, whatIsPlatform);
				if (hit)
				{
					StartCoroutine(DisableCollider(hit.collider, 0.6f));
					break;
				}
			}
		}		       
	}

	IEnumerator DisableCollider(Collider2D collider, float time)
	{
		collider.enabled = false;
		yield return new WaitForSeconds(time);
		collider.enabled = true;
	}

	void Die()
	{
		AudioManager.instance.PlaySound("PlayerDeath");      
		isDead = true;
		if(timer!=null)  timer.isStopped = true;
		Destroy(gameObject);
	}

	public void resetLevel()
	{
		stats.currentHP = stats.maxHP;
	}

	public void finishLevel()
	{
		if (!isDead)
		{
			if (anim != null) anim.SetBool("isGettingToPortal", true);
			playerDamageable.StartInvincibilityTimer(9999f);
			//invincibilityTimer = 9999f;
			inertia = true;
			colliders.SetActive(false);
		}
	}  
}
