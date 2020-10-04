using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultMagazineManager : MonoBehaviour, IWeaponMagazineManager {
    private WeaponStats stats;
    private int bulletsInMagazine;
    private bool isReloading;
    private AmmoUI ammoUI;
    Animator anim;

    public bool IsReloading { get { return isReloading; } }

    void OnEnable () {
        isReloading = false;
        ammoUI?.UpdateBulletsInMagazine (bulletsInMagazine);
        ammoUI?.UpdateMagazineSize (stats.MagazineSize);
    }

    void Awake () {
        GameObject ammoUIObject = GameObject.Find ("Ammo");
        if (ammoUIObject != null) ammoUI = ammoUIObject.GetComponent<AmmoUI> ();
        anim = GetComponent<Animator> ();
    }

    void Update () {
        if (Input.GetButtonDown ("Reload") && isReloading == false && IsMagazineFull () == false) {
            StartCoroutine (Reload ());
        }
    }

    public void Initialize (WeaponStats stats) {
        this.stats = stats;
        bulletsInMagazine = stats.MagazineSize;
    }

    public bool IsMagazineEmpty () {
        return bulletsInMagazine <= 0;
    }

    public bool IsMagazineFull () {
        return bulletsInMagazine == stats.MagazineSize;
    }

    public void ChangeBulletsAmountByNumber (int amount) {
        bulletsInMagazine += amount;
        ammoUI?.UpdateBulletsInMagazine (bulletsInMagazine);
    }

    IEnumerator Reload () {
        AudioManager.instance.PlaySound ("ReloadingStart");
        anim.SetTrigger ("Reloading");
        isReloading = true;
        yield return new WaitForSeconds (stats.ReloadTime);
        bulletsInMagazine = stats.MagazineSize;
        ammoUI?.UpdateBulletsInMagazine (bulletsInMagazine);
        isReloading = false;
    }
}