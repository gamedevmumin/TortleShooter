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
    GameObject groundCheck;

    IMovement movementController;
    IJumpingController jumpingController;

    void Awake () {
            stats.Set(startingStats);
            stats.currentHP = stats.maxHP;
            instance = this;
            isDead = false;          
            sR = GameObject.Find("PlayerGraphics").GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();                                          
            colliders = transform.Find("Colliders").gameObject;
            fallingTimer = fallingTime;
            playerWeapons = GetComponent<PlayerWeapons>();            
            groundCheck = gameObject.transform.Find("GroundCheck").gameObject;
            if (groundCheck == null) Debug.LogError("Can't find ground check!");
            movementController = GetComponent<IMovement>();
            jumpingController = GetComponent<IJumpingController>();
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
  
    void ManageMovement()
    {
        float movementInput = Input.GetAxisRaw("Horizontal");
        movementController.Move(movementInput);
        jumpingController.ManageJumping();
        ManageDirection();
        ManagePlatforms();
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
        if(Input.GetButton("Down"))
        {          
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, 0.2f, whatIsPlatform);            
            if(hit)
            {
                StartCoroutine(DisableCollider(hit.collider, 0.6f));            
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
            //invincibilityTimer = 9999f;
            inertia = true;
            colliders.SetActive(false);
        }
    }  
}
