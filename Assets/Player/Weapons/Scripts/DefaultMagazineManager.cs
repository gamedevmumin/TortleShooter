using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultMagazineManager : MonoBehaviour, IWeaponMagazineManager {
    private WeaponStats stats;
    private int bulletsInMagazine;
    private bool isReloading;

    void Update () {
        if (Input.GetButton ("Reload") && isReloading == false) {
            StartCoroutine(Reload());
        }
    }

    public void Initialize (WeaponStats stats) {
        this.stats = stats;
        bulletsInMagazine = stats.MagazineSize;
    }

    public bool IsMagazineEmpty () {
        return bulletsInMagazine <= 0;
    }

    public void ChangeBulletsAmountByNumber (int amount) {
        bulletsInMagazine += amount;
    }

    IEnumerator Reload () {
        isReloading = true;
        yield return new WaitForSeconds(stats.ReloadTime);
        bulletsInMagazine = stats.MagazineSize;
        isReloading = false;
    }
}