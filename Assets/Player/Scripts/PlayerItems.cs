using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour {
    public List<Item> EquippedItems { get; private set; }
    public delegate void ReloadItems();
    public ReloadItems reloadItems;
    private void Awake()
    {
        EquippedItems = new List<Item>();
    }

    public void PickUpItem(Item item)
    {
        EquippedItems.Add(item);
        reloadItems?.Invoke();
    }

}
