using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof (BulletTouchDamage))]
public class Bullet : MonoBehaviour {
    [SerializeField]
    float speed;
    [SerializeField]
    float lifeTime = 0.5f;

    IMovement movement;
    IDestructible destructible;

    public void Initialize (int minDamage, int maxDamage, int criticalStrikeChance) {
        BulletTouchDamage bulletTouchDamage = GetComponent<BulletTouchDamage>();
        bulletTouchDamage.Initialize(minDamage, maxDamage, criticalStrikeChance);
    }

    private void Awake () {
        movement = GetComponent<IMovement> ();
        destructible = GetComponent<IDestructible> ();
    }

    void Start () {
        Invoke ("DestroyProjectile", lifeTime);
    }

    void DestroyProjectile () {
        destructible.Destroy (null);
    }

    private void FixedUpdate () {
        movement.Move (transform.right, speed);
    }

    void OnTriggerEnter2D (Collider2D coll) {
        if (coll.gameObject.tag == "Ground") {
            DestroyProjectile ();
        }

    }
}