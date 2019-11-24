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
    public override string Name { get { return name; } protected set { name = value; } }
    public override Sprite Icon { get => icon; protected set => icon=value; }
    public override PickableItem PickableItem { get; protected set; }
    public bool WasActivated { get; set; }

    public void ChangeStats(PlayerStats playerStats)
    {
        WasActivated = true;
        playerStats.speed += speedBuff;
    }

    public override void OnPickUp(PickableItem pickableItem)
    {
        PickableItem = pickableItem;
    }
}
