using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShooting : MonoBehaviour, IShooting
{
    [SerializeField]
    WeaponStats stats;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform firePoint;
    CameraShake cameraShake;

    void Awake()
    {
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
    }

    public void Shoot()
    {                
                anim.SetTrigger("Shot");
                cameraShake.Shake(stats.ShakeAmount, 0.1f);
                Bullet shotBullet = Instantiate(stats.Bullet, firePoint.position, transform.rotation) as Bullet;
                shotBullet.gameObject.transform.Rotate(new Vector3(0f, 0f, 1f), Random.Range(-stats.Spread, stats.Spread));
                AudioManager.instance.PlaySound(stats.ShotSound);            
       
    }

}
