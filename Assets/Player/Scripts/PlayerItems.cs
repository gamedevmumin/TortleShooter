using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour {
    public List<Item> EquippedItems { get; private set; }
    List<IPlayerStatsChanger> statsChangingItems = new List<IPlayerStatsChanger>();
    public delegate void ReloadItems();
    public ReloadItems reloadItems;
    PlayerController playerController;
    private void Awake()
    {
        EquippedItems = new List<Item>();
        playerController = GetComponent<PlayerController>();
        reloadItems += () =>
        {
            statsChangingItems.Clear();
            List<Item> temp = EquippedItems.FindAll(item => item is IPlayerStatsChanger);
            foreach (IPlayerStatsChanger item in temp)
            {
                statsChangingItems.Add(item as IPlayerStatsChanger);
                if(!item.WasActivated) item.ChangeStats(playerController.Stats);
            }
        }; 
    }

    public void PickUpItem(Item item)
    {
        EquippedItems.Add(item);
        reloadItems?.Invoke();
    }

    

}
