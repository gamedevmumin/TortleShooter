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
    [SerializeField] PlayerStartingWeapons playerStartingWeapons;
    WeaponsUI wUI;

    private void Awake()
    {
        GameObject wuiObject = GameObject.Find("Canvas/StatPanel/WeaponsUI");
        if (wuiObject) wUI = wuiObject.GetComponent<WeaponsUI>();
    }

    void Start()
    {
        if (playerStartingWeapons.startingInactiveWeapon) PickUpWeapon(playerStartingWeapons.startingInactiveWeapon);
        if (playerStartingWeapons.startingActiveWeapon) PickUpWeapon(playerStartingWeapons.startingActiveWeapon);
        
       
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
            pickableWeapon.transform.position = transform.position;
            pickableWeapon.Initialize(activeWeapon);
            Destroy(activeWeaponObject.gameObject);
            activeWeaponObject = Instantiate(weapon, weaponSlot) as Weapon;
            activeWeapon = weapon;
            wUI.UpdateState(activeWeapon.Icon, inactiveWeapon.Icon);
        }       
    }
}
