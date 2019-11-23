using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRunner : Enemy {

    Rigidbody2D rb;
    [SerializeField]
    float speed = 10f;

    //bool isDead = false;

    Animator anim;

    CameraShake cameraShake;

    BoxCollider2D bC;
    CircleCollider2D cC;
    enum Direction {  RIGHT, LEFT };
    Direction direction = Direction.RIGHT;

 	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bC = transform.Find("Colliders").GetComponent<BoxCollider2D>();
        cC = GetComponentInChildren<CircleCollider2D>();
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
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
        currentHealth = maxHealth;
        isDead = false;
        ChooseDirection();
	}
	
	// Update is called once per frame
	void Update () {
        if(isDead)
        {
          //  rb.velocity = new Vector2(0f, rb.velocity.y);
            
        }
        else
        {
            ChooseDirection();
        }

        if(currentHealth<=0 && !isDead)
        {
            Die();
        }
	}

    void ChooseDirection()
    {
            if (direction == Direction.RIGHT) rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            else if (direction == Direction.LEFT) rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);       
    }

    public void ChangeDirection()
    {
        if (direction == Direction.RIGHT) direction = Direction.LEFT;
        else direction = Direction.RIGHT;
        transform.Rotate(0f, 180f, 0f);
        ChooseDirection();
    }

    void ManageMovement()
    {
       
    }

    override protected IEnumerator Blink()
    {
        sR.material.SetFloat("_FlashAmount", 1f);
        yield return new WaitForSeconds(0.03f);
        sR.material.SetFloat("_FlashAmount", 0f);
    }

    override public void TakeDamage(int damage, Transform damageDealer)
    {
        if (!isDead)
        {
            cameraShake.Shake(0.05f, 0.1f);
            currentHealth -= damage;
            if (currentHealth > 0f) AudioManager.instance.PlaySound("Hurt");
            StartCoroutine("Blink");
        }
       
    }

    override public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            transform.Rotate(0, 0, -90);
            //cC.enabled = false;
            //bC.enabled = false;
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
            coll.GetComponent<PlayerController>().TakeDamage(1, transform);
        }
    }

}

