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
    CameraShake cameraShake;
    bool isRight = true;
    bool isGrounded = true;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    LayerMask whatIsPlatform;
    GameObject groundCheck;
    [SerializeField]
    float jumpPressedRemember = 0.2f;
    float jumpPressedRememberTimer;
    [SerializeField]
    float groundedRemember = 0.2f;
    float groundedRememberTimer;
    [SerializeField] [Range(0, 1)]
    float cutOfJumpHeight;
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
    float invincibilityTimer;
    [SerializeField]
    Congratz congratz;
    [SerializeField]
    GameObject press;
    HeartBar heartBar;
    GameObject colliders;
    BoxCollider2D trigger;
    bool first = true;
    static PlayerController instance;
    PlayerWeapons playerWeapons;

    IMovement movementController;

    void Start () {
            stats.Set(startingStats);
            stats.currentHP = stats.maxHP;
            instance = this;
            isDead = false;
            trigger = GetComponent<BoxCollider2D>();
            heartBar = GameObject.Find("StatPanel/HeartBar").GetComponent<HeartBar>();
            sR = GameObject.Find("PlayerGraphics").GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            groundCheck = gameObject.transform.Find("GroundCheck").gameObject;
            cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();           
            if (groundCheck == null) Debug.LogError("Can't find ground check!");
            colliders = transform.Find("Colliders").gameObject;
            fallingTimer = fallingTime;
            playerWeapons = GetComponent<PlayerWeapons>();
            movementController = GetComponent<IMovement>();
	}
	
	// Update is called once per frame
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
        invincibilityTimer -= Time.deltaTime;
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

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, whatIsGround);
    }

    void ManageMovement()
    {
        float movementInput = Input.GetAxisRaw("Horizontal");
        movementController.Move(movementInput);
        ManageJumping();
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
        if (rb.velocity.x < 0 && isRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && !isRight)
        {
            Flip();
        }
    }

    void ManageJumping()
    {
        groundedRememberTimer -= Time.deltaTime;
        if (isGrounded)
        {
            groundedRememberTimer = groundedRemember;
        }

        jumpPressedRememberTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedRememberTimer = jumpPressedRemember;
        }
   
        if (groundedRememberTimer>0f && jumpPressedRememberTimer > 0f)
        {
            jumpPressedRememberTimer = 0f;
            groundedRememberTimer = 0f;
            rb.velocity = new Vector2(rb.velocity.x, stats.jumpHeight);
            AudioManager.instance.PlaySound("Jump");
        }

        if(Input.GetButtonUp("Jump"))
        {
            if(rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutOfJumpHeight);
            }
        }
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

    public void TakeDamage(int damage, Transform damageDealer)
    {
        if (invincibilityTimer <= 0f)
        {
            cameraShake.blind();
            stats.currentHP -= damage;
            StartCoroutine("Blink");
            AudioManager.instance.PlaySound("PlayerDamaged");
            if (heartBar != null) heartBar.changeState(stats.currentHP, stats.maxHP);
            invincibilityTimer = stats.invincibilityTime;
        }
    }


    void Die()
    {
       AudioManager.instance.PlaySound("PlayerDeath");      
      // if(press!=null) press.SetActive(true);
      // if(congratz!=null) congratz.gameObject.SetActive(true);
        isDead = true;
        if(timer!=null)  timer.isStopped = true;
        //rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }

    IEnumerator Blink()
    {
        sR.material.SetFloat("_FlashAmount", 1f);
        yield return new WaitForSeconds(0.03f);
        sR.material.SetFloat("_FlashAmount", 0f);
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
            //BetweenLevelDataContainer.instance.playerStats = stats;
            invincibilityTimer = 9999f;
            inertia = true;
            colliders.SetActive(false);
        }
    }
    
}
