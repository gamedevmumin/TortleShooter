using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EnemyStats))]
public class EnemyKillable : MonoBehaviour, IKillable {
    EnemyStats stats;
    Rigidbody2D rb;
    ScreeneFreezer screenFreezer;
    [SerializeField]
    FloatVariable freezeTime;
    [SerializeField]
    SpriteRenderer sR;
    [SerializeField]
    Animator anim;
    [SerializeField]
    List<Collider2D> colls;
    [SerializeField]
    GameObject deathParticle;

    private static readonly int Color = Shader.PropertyToID("_Color");

    void Awake () {
        stats = GetComponent<EnemyStats> ();
        screenFreezer = FindObjectOfType<ScreeneFreezer> ();
        rb = GetComponent<Rigidbody2D> ();
        if(anim == null) anim = GetComponent<Animator> ();
        if(sR == null) sR = GetComponent<SpriteRenderer> ();
    }

    void Update () {
        if (stats.CurrentHP <= 0 && !stats.IsDead) Die ();
    }

    public void Die () {
        if (!stats.IsDead) {
            stats.IsDead = true;
            anim.SetBool ("isDead", stats.IsDead);
            transform.Rotate (0, 0, -90);
            screenFreezer.Freeze (freezeTime.Value);
            AudioManager.instance.PlaySound ("Death");
            if (rb) {
                rb.AddForce (stats.LastDamageDealerDirection * 600f);
                rb.gravityScale = 3.5f;
            }
            var deadColor = new Color32 (65, 58, 58, 255);

            sR.material.SetColor(Color, deadColor);
            if (deathParticle) {
                Instantiate (deathParticle, transform.position, transform.rotation);
            }

            foreach (Collider2D coll in colls) {
                if (coll) coll.enabled = false;
            }
        }
    }

}