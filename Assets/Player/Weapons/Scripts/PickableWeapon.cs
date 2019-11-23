using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableWeapon : MonoBehaviour
{
    [SerializeField]
    string weaponName;

    [SerializeField]
    Weapon weapon;
    SpriteRenderer sR;

    Text nameText;

    bool isInRange = false;

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
        Transform textObject = transform.Find("Canvas").Find("NameText");
        nameText = textObject.GetComponent<Text>();
        nameText.text = weaponName;
        nameText.transform.parent.gameObject.SetActive(false);
        playerWeapons = GameObject.Find("Player").GetComponent<PlayerWeapons>();
    }

    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                weapon.OnPickUp(this);
                playerWeapons.PickUpWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            nameText.transform.parent.gameObject.SetActive(true);
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            nameText.transform.parent.gameObject.SetActive(false);
            isInRange = false;
        }
    }
}
