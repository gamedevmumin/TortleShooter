using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleShooting : MonoBehaviour, IShooting {
    [SerializeField]
    WeaponStats stats;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform firePoint;
    CameraShake cameraShake;
    [SerializeField]
    int shotsAmount = 3;
    void Awake () {
        cameraShake = GameObject.Find ("CameraShake").GetComponent<CameraShake> ();
    }

    public void Shoot (ref int bulletsInMagazine) {
        StartCoroutine (SerialShot ());
        bulletsInMagazine -= 3;
    }

    IEnumerator SerialShot () {
        for (int i = 0; i < shotsAmount; i++) {
            anim.SetTrigger ("Shot");
            cameraShake.Shake (stats.ShakeAmount, 0.03f);
            Instantiate (stats.Bullet, firePoint.position, transform.rotation);
            AudioManager.instance.PlaySound (stats.ShotSound);
            yield return new WaitForSeconds (0.1f);
        }
    }
}