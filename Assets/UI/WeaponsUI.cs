using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUI : MonoBehaviour
{
    [SerializeField]
    Image activeWeapon;
    [SerializeField]
    Image inactiveWeapon;

    public void UpdateState(Sprite activeWeaponSprite, Sprite inactiveWeaponSprite)
    {
        if (activeWeaponSprite)
        {
            activeWeapon.sprite = activeWeaponSprite;
            activeWeapon.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            activeWeapon.color = new Color32(255, 255, 255, 0);
        }
        if (inactiveWeaponSprite)
        {
            inactiveWeapon.sprite = inactiveWeaponSprite;
            inactiveWeapon.color = new Color32(73, 73, 73, 255);
        }
        else
        {
            inactiveWeapon.color = new Color32(73, 73, 73, 0);
        }

    }
}
