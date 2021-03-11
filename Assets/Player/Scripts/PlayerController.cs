using System.Collections;
using System.Collections.Generic;
using Interfaces;
using ItemSystem;
using ItemSystem.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Animator))]
public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb { get; private set; }
    Animator anim;
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
    public PlayerStats Stats => stats;
    bool isDead;
    bool inertia = false;
    GameObject colliders;
    static PlayerController instance;
    [SerializeField]
    PlayerWeapons playerWeapons;
    [SerializeField]
    List<Transform> groundChecks;
    [FormerlySerializedAs("OnPlayerDied")] [SerializeField]
    GameEvent onPlayerDied;
    IMovement movementController;
    IJumpingController jumpingController;
    IDashing dashingController;
    PlayerDamageable playerDamageable;
    IDirectionManager directionManager;
    private WallClimbing wallClimbing;
    
    [SerializeField]
    SceneContents sceneContents;
    [SerializeField]
    PlayerCollectables playerCollectables;
    [SerializeField]
    Transform weaponSlot;

    [SerializeField] private ActiveItemsManager activeItemsManager;
    void Awake () {
        anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody2D> ();
        colliders = transform.Find ("Colliders").gameObject;
        playerWeapons = GetComponent<PlayerWeapons> ();
        movementController = GetComponent<IMovement> ();
        jumpingController = GetComponent<IJumpingController> ();
        dashingController = GetComponent<IDashing> ();
        playerDamageable = GetComponent<PlayerDamageable> ();
        directionManager = GetComponent<IDirectionManager> ();
        wallClimbing = GetComponent<WallClimbing>();
    }

    private void OnEnable () {
        sceneContents.RegisterPlayer (this);
    }

    private void OnDisable () {
        sceneContents.UnregisterPlayer (this);
    }

    private void Start () {
        if (BetweenLevelDataContainer.instance.FirstScene) {
            stats.Set (startingStats);
            stats.currentHP = stats.maxHP;
            playerCollectables.Initialize ();
        }

        isDead = false;
        fallingTimer = fallingTime;
    }

    void Update () {
        if (stats.currentHP <= 0f) Die ();
        if (!inertia) {
            if (!isDead) {
                ManageMovement ();
                ManageAnimation ();
                ManageSwitchingWeapons ();
                ManageActiveItem();;
            }
        }
    }

    private void ManageActiveItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activeItemsManager.ActivateChosenItem();
        }
    }
    
    void ManageSwitchingWeapons () {
        if (Mathf.Abs (Input.mouseScrollDelta.y) > 0f) {
            playerWeapons.SwitchWeapon ();
        }
    }

    void ManageAnimation () {
        anim.SetFloat ("vSpeed", Mathf.Abs (rb.velocity.x));
    }

    Vector2 movementInput;
    void ManageMovement () {
        movementInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), movementInput.y);
        jumpingController.ManageJumping ();
        if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetMouseButtonDown (1)) dashingController.Dash ();
        directionManager.ManageDirection (rb.velocity);
        ManagePlatforms ();
        wallClimbing.ManageWallClimbing(movementInput.x);
    }

    private void FixedUpdate () {      
        movementController.Move (movementInput, stats.speed.Value);
    }

    void ManagePlatforms () {
        if (Input.GetButton ("Down")) {
            foreach (Transform groundCheck in groundChecks) {
                RaycastHit2D hit = Physics2D.Raycast (groundCheck.transform.position, Vector2.down, 0.2f, whatIsPlatform);
                if (hit) {
                    StartCoroutine (DisableCollider (hit.collider, 0.6f));
                    break;
                }
            }
        }
    }

    IEnumerator DisableCollider (Collider2D collider, float time) {
        collider.enabled = false;
        yield return new WaitForSeconds (time);
        collider.enabled = true;
    }

    void Die () {
        AudioManager.instance.PlaySound ("PlayerDeath");
        onPlayerDied.Raise ();
        isDead = true;
        if (timer != null) timer.isStopped = true;
        Destroy (gameObject);
    }

    public void resetLevel () {
        stats.currentHP = stats.maxHP;
    }

    public void finishLevel () {
        if (!isDead) {
            if (anim != null) anim.SetBool ("isGettingToPortal", true);
            playerDamageable.StartInvincibilityTimer (9999f);
            inertia = true;
            colliders.SetActive (false);
        }
    }
}