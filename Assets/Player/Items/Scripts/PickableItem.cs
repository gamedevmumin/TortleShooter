using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    Item item;
    SpriteRenderer sR;

    bool isInRange = false;

    PlayerItems playerItems;

    void Awake()
    {
        if (item != null)
        {
            Set();
        }
    }

    public void Interact()
    {
        item.OnPickUp();
        playerItems.PickUpItem(item);
        Destroy(gameObject);
    }

    public void Initialize(Item item)
    {
        this.item = item;
        Set();
    }

    private void Set()
    {
        Transform graphic = transform.Find("Graphic");
        sR = graphic.GetComponent<SpriteRenderer>();
        sR.sprite = item.Icon;
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

}
