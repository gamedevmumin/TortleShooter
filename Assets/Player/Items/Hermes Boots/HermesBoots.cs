using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesBoots : Item, IPlayerStatsChanger
{
    [SerializeField]
    new string name;
    [SerializeField]
    Sprite icon;
    [SerializeField]
    float speedBuff;
    [SerializeField]
    PlayerStats playerStats;
    public override string Name { get { return name; } protected set { name = value; } }
    public override Sprite Icon { get => icon; protected set => icon=value; }
    public override PickableItem PickableItem { get; protected set; }
    public bool WasActivated { get; set; }

    public void ChangeStats()
    {
        WasActivated = true;
        Debug.Log("Changing stats...");
        playerStats.speed.Value += speedBuff;
    }

    public override void OnPickUp()
    {
        WasActivated = false;
    }
}
