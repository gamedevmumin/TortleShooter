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
    // Start is called before the first frame update
    void Start()
    {       
       // UpdateState(null, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
