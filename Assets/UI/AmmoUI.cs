using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour {
    [SerializeField]
    Text bulletsInMagazine;
    [SerializeField]
    Text magazineSize;
    [SerializeField]
    EquippedWeapons playerEquippedWeapons;

    public void UpdateBulletsInMagazine (int bulletsInMagazine) {

        this.bulletsInMagazine.text = GetSpaces(bulletsInMagazine) + bulletsInMagazine.ToString ();
    }

    public void UpdateMagazineSize (int magazineSize) {
        this.magazineSize.text = magazineSize.ToString () + GetSpaces(magazineSize);
    }

    string GetSpaces (int number) {
        string spaces = "";
        for (int i = 0; i < 3 - number.ToString ().Length; i++) {
            spaces += " ";
        }
        return spaces;
    }
}