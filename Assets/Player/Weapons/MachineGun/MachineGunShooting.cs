using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShooting : MonoBehaviour, IShooting {
    [SerializeField]
    WeaponStats weaponStats;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform firePoint;
    CameraShake cameraShake;
    IWeaponMagazineManager magazineManager;
    void Awake () {
        cameraShake = GameObject.Find ("CameraShake").GetComponent<CameraShake> ();
        magazineManager = GetComponent<IWeaponMagazineManager>();
    }

    public void Initialize(WeaponStats weaponStats) {
        this.weaponStats = weaponStats;
    }

    public void Shoot () {
        anim.SetTrigger ("Shot");
        cameraShake.Shake (weaponStats.ShakeAmount, 0.03f);
        Bullet shotBullet = Instantiate (weaponStats.Bullet, firePoint.position, transform.rotation) as Bullet;
        shotBullet.gameObject.transform.Rotate (new Vector3 (0f, 0f, 1f), Random.Range (-weaponStats.Spread, weaponStats.Spread));
        shotBullet.Initialize(weaponStats.MinDamage, weaponStats.MaxDamage, weaponStats.CriticalStrikeChance);
        AudioManager.instance.PlaySound (weaponStats.ShotSound);
        magazineManager.ChangeBulletsAmountByNumber(-1);
    }
}
