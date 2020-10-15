using System.Collections;
using UnityEngine;

public class AssaultRifleShooting : MonoBehaviour, IShooting {
    WeaponStats weaponStats;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform firePoint;
    CameraShake cameraShake;
    [SerializeField]
    int shotsAmount = 3;
    IWeaponMagazineManager magazineManager;
    void Awake () {
        cameraShake = GameObject.Find ("CameraShake").GetComponent<CameraShake> ();
        magazineManager = GetComponent<IWeaponMagazineManager>();
    }

    public void Initialize(WeaponStats weaponStats) {
        this.weaponStats = weaponStats;
    }

    public void Shoot () {
        StartCoroutine (SerialShot ());
    }

    IEnumerator SerialShot () {
        for (int i = 0; i < shotsAmount; i++) {
            anim.SetTrigger ("Shot");
            cameraShake.Shake (weaponStats.ShakeAmount, 0.03f);
            Bullet bullet = Instantiate (weaponStats.Bullet, firePoint.position, transform.rotation) as Bullet;
            bullet.Initialize(weaponStats.MinDamage, weaponStats.MaxDamage, weaponStats.CriticalStrikeChance);
            AudioManager.instance.PlaySound (weaponStats.ShotSound);
            yield return new WaitForSeconds (0.1f);
            magazineManager.ChangeBulletsAmountByNumber(-1);
        }
    }
}
