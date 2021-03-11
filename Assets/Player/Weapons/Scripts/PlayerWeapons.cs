using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField]
    EquippedWeapons equippedWeapons;
    Weapon activeWeaponObject;
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
        if (BetweenLevelDataContainer.instance.FirstScene)
        {
            equippedWeapons.ActiveWeapon = null;
            equippedWeapons.InactiveWeapon = null;
            if (playerStartingWeapons.startingInactiveWeapon) PickUpWeapon(playerStartingWeapons.startingInactiveWeapon, transform.position);
            if (playerStartingWeapons.startingActiveWeapon) PickUpWeapon(playerStartingWeapons.startingActiveWeapon, transform.position);
        }
        else
        {
            Weapon activeWeapon = equippedWeapons.ActiveWeapon;
            equippedWeapons.ActiveWeapon = null;
            Weapon inactiveWeapon = equippedWeapons.InactiveWeapon;
            equippedWeapons.InactiveWeapon = null;
            if (activeWeapon) PickUpWeapon(activeWeapon, transform.position);
            if (inactiveWeapon) PickUpWeapon(inactiveWeapon, transform.position);
        }

        if(wUI)
        {
            if(equippedWeapons.ActiveWeapon && equippedWeapons.InactiveWeapon)
            {
                wUI.UpdateState(equippedWeapons.ActiveWeapon.Icon, equippedWeapons.InactiveWeapon.Icon);
            }
            else if(equippedWeapons.ActiveWeapon)
            {
                wUI.UpdateState(equippedWeapons.ActiveWeapon.Icon, null);
            }
            else
            {
                wUI.UpdateState(null, null);
            }          
        }
    }
    
    public void SwitchWeapon()
    {      
        if(equippedWeapons.ActiveWeapon && equippedWeapons.InactiveWeapon)
        {
            activeWeaponObject.gameObject.SetActive(false);
            inactiveWeaponObject.gameObject.SetActive(true);
            Weapon tmp = equippedWeapons.ActiveWeapon;
            equippedWeapons.ActiveWeapon = equippedWeapons.InactiveWeapon;
            equippedWeapons.InactiveWeapon = tmp;
            tmp = activeWeaponObject;
            activeWeaponObject = inactiveWeaponObject;
            inactiveWeaponObject = tmp;
            wUI.UpdateState(equippedWeapons.ActiveWeapon.Icon, equippedWeapons.InactiveWeapon.Icon);
            AudioManager.instance.PlaySound("FireBolt");
        }
    }

    public void PickUpWeapon(Weapon weapon, Vector3 position)
    {
        if(equippedWeapons.ActiveWeapon == null)
        {
            activeWeaponObject = Instantiate(weapon, weaponSlot) as Weapon;
            equippedWeapons.ActiveWeapon = weapon;
            if(wUI) wUI.UpdateState(equippedWeapons.ActiveWeapon.Icon, null);
        }
        else if(equippedWeapons.InactiveWeapon == null)
        {
      
            equippedWeapons.InactiveWeapon = equippedWeapons.ActiveWeapon;
            inactiveWeaponObject = activeWeaponObject;
            inactiveWeaponObject.gameObject.SetActive(false);
            activeWeaponObject = Instantiate(weapon, weaponSlot) as Weapon;           
            equippedWeapons.ActiveWeapon = weapon;
            if (wUI) wUI.UpdateState(equippedWeapons.ActiveWeapon.Icon, equippedWeapons.InactiveWeapon.Icon);
        }
        else
        {
            PickableWeapon pickableWeapon = Instantiate(pickableWeaponPrefab) as PickableWeapon;
            pickableWeapon.transform.position = position;
            pickableWeapon.Initialize(equippedWeapons.ActiveWeapon);
            Destroy(activeWeaponObject.gameObject);
            activeWeaponObject = Instantiate(weapon, weaponSlot) as Weapon;
            equippedWeapons.ActiveWeapon = weapon;
            if (wUI) wUI.UpdateState(equippedWeapons.ActiveWeapon.Icon, equippedWeapons.InactiveWeapon.Icon);
        }       
    }
}
