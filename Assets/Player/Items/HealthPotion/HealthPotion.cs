using System.Collections;
using System.Collections.Generic;
using ItemSystem.Scripts;
using UnityEngine;

public class HealthPotion : Consumable
{
    [SerializeField] private GameObject healingEffect;
    private Transform healingPosition;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private ActiveItemsManager activeItemsManager;

    void Start()
    {
        Debug.Log("Start!!!!");
    }

    public override void OnPickUp()
    {
        amount = maxAmount;
    }

    public override void Activate()
    {
        if (playerStats.IsFullHealth) return;
        healingPosition = GameObject.Find("Player/Effects/HealingPosition").transform; //needs change
        AudioManager.instance.PlaySound("HeartPickUp");
        var go = Instantiate(healingEffect, healingPosition.position, Quaternion.Euler(-90, 0, 0));
        go.transform.parent = healingPosition;
        go.transform.localScale = new Vector3(1, 1, 1);
        playerStats.Heal(1);
        --amount;
        if (amount == 0)
        { 
            activeItemsManager.RemoveChosenItem();
        }
    }
}
