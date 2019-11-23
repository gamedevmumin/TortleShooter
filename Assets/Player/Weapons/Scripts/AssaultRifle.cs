using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssaultRifle : Weapon {

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += OnSceneLoaded;
        shotsIntervalTimer = shotsInterval;
        firePoint = transform.Find("FirePoint").transform;
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        ManageRotation();
        ManageShooting();
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
    }

    override protected void ManageShooting()
    {
        if (shotsIntervalTimer <= 0f)
        {
            if (Input.GetButton("Fire1"))
            {
                shotsIntervalTimer = shotsInterval;
                StartCoroutine(tripleShot());
            }
        }
        else
        {
            shotsIntervalTimer -= Time.deltaTime;
        }
    }

    IEnumerator tripleShot()
    {       
        for (int i = 0; i < 3; i++)
        {
            anim.SetTrigger("Shot");
            cameraShake.Shake(shakeAmount, 0.1f);
            Instantiate(ChooseBulletToShoot(), firePoint.position, transform.rotation);
            AudioManager.instance.PlaySound("RifleShooting");
            yield return new WaitForSeconds(0.1f);
        }
    }

}
