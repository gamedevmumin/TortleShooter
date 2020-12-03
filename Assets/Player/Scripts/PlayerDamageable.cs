using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    float invincibilityTimer;
    public float InvincibilityTimer { get { return invincibilityTimer; } private set { invincibilityTimer = value; } }
    public bool IsDamageable => InvincibilityTimer <= 0f;

    [SerializeField]
    PlayerStats stats;
    HeartBar heartBar;
    CameraShake cameraShake;
    [SerializeField]
    VisualSpriteEffect damageEffect;
    SpriteRenderer sR;

    void Start()
    {
        GameObject heartBarObj = GameObject.Find("StatPanel/HeartBar");
        if(heartBarObj) heartBar = heartBarObj.GetComponent<HeartBar>();
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        sR = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        invincibilityTimer -= Time.deltaTime;
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        if (invincibilityTimer <= 0f)
        {
            cameraShake.blind();
            stats.currentHP -= damageInfo.DamageDone;
            StartCoroutine(damageEffect.PlayEffect(sR));
            AudioManager.instance.PlaySound("PlayerDamaged");
            if (heartBar != null) heartBar.changeState();
            StartInvincibilityTimer(stats.invincibilityTime);
        }
        
    }

    public void StartInvincibilityTimer(float invincibilityTime)
    {
        invincibilityTimer = invincibilityTime;
    }
}

