using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour {
    public List<Item> EquippedItems { get; private set; }
    List<IPlayerStatsChanger> statsChangingItems = new List<IPlayerStatsChanger>();
    public delegate void ReloadItems();
    public ReloadItems reloadItems;
    private PlayerController playerController;
    private void Awake()
    {
        EquippedItems = new List<Item>();
        playerController = GetComponent<PlayerController>();
        reloadItems += () =>
        {
            statsChangingItems.Clear();
            List<Item> temp = EquippedItems.FindAll(item => item is IPlayerStatsChanger);
            foreach (var item1 in temp)
            {
                var item = (IPlayerStatsChanger) item1;
                statsChangingItems.Add(item);
                if(!item.WasActivated) item.ChangeStats();
            }
        }; 
    }

    public void PickUpItem(Item item)
    {
        EquippedItems.Add(item);
        reloadItems?.Invoke();
    }

    

}
