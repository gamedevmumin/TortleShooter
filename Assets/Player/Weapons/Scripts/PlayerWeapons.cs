using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    Weapon activeWeapon;
    Weapon activeWeaponObject;
    Weapon inactiveWeapon;
    Weapon inactiveWeaponObject;
    [SerializeField]
    Transform weaponSlot;
    [SerializeField] PickableWeapon pickableWeaponPrefab;
    WeaponsUI wUI;
    // Start is called before the first frame update
    void Start()
    {
       if(activeWeapon) Instantiate(activeWeapon, weaponSlot);
        wUI = GameObject.Find("Canvas/StatPanel/WeaponsUI").GetComponent<WeaponsUI>();
        if(wUI)
        {
            if(activeWeapon && inactiveWeapon)
            {
                wUI.UpdateState(activeWeapon.Icon, inactiveWeapon.Icon);
            }
            else if(activeWeapon)
            {
                wUI.UpdateState(activeWeapon.Icon, null);
            }
            else
            {
                wUI.UpdateState(null, null);
            }          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWeapon()
    {      
        if(activeWeapon && inactiveWeapon)
        {
            activeWeaponObject.gameObject.SetActive(false);
            inactiveWeaponObject.gameObject.SetActive(true);
            Weapon tmp = activeWeapon;
            activeWeapon = inactiveWeapon;
            inactiveWeapon = tmp;
            tmp = activeWeaponObject;
            activeWeaponObject = inactiveWeaponObject;
            inactiveWeaponObject = tmp;
            wUI.UpdateState(activeWeapon.Icon, inactiveWeapon.Icon);
        }
    }

    public void PickUpWeapon(Weapon weapon)
    {
        if(activeWeapon == null)
        {
            activeWeaponObject = Instantiate(weapon, weaponSlot) as Weapon;
            activeWeapon = weapon;
            wUI.UpdateState(activeWeapon.Icon, null);
        }
        else if(inactiveWeapon == null)
        {
      
            inactiveWeapon = activeWeapon;
            inactiveWeaponObject = activeWeaponObject;
            inactiveWeaponObject.gameObject.SetActive(false);
            activeWeaponObject = Instantiate(weapon, weaponSlot) as Weapon;           
            activeWeapon = weapon;
            wUI.UpdateState(activeWeapon.Icon, inactiveWeapon.Icon);
        }
        else
        {
            PickableWeapon pickableWeapon = Instantiate(pickableWeaponPrefab) as PickableWeapon;
            pickableWeapon.Initialize(activeWeapon);
            Destroy(activeWeaponObject.gameObject);
            activeWeaponObject = Instantiate(weapon, weaponSlot) as Weapon;
            activeWeapon = weapon;
            wUI.UpdateState(activeWeapon.Icon, inactiveWeapon.Icon);
        }
        
    }
}
