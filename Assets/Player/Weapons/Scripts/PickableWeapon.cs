using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableWeapon : MonoBehaviour, IInteractable
{
    [SerializeField]
    Weapon weapon;
    SpriteRenderer sR;

    PlayerWeapons playerWeapons;

    void Awake()
    {
        if (weapon != null)
        {
            Set();
        }
    }

    public void Initialize(Weapon weapon)
    {
        this.weapon = weapon;
        Set();     
    }

    private void Set()
    {
        Transform graphic = transform.Find("Graphic");
        sR = graphic.GetComponent<SpriteRenderer>();
        sR.sprite = weapon.Icon;
        playerWeapons = GameObject.Find("Player").GetComponent<PlayerWeapons>();
    }

    public void Interact()
    {
        weapon.OnPickUp(this);
        playerWeapons.PickUpWeapon(weapon);
        Destroy(gameObject);
    }
     
    
}
