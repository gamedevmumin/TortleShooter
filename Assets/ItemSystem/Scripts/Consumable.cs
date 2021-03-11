using System.Collections;
using System.Collections.Generic;
using ItemSystem.Scripts;
using UnityEngine;

public abstract class Consumable : ActiveItem
{
    protected int amount;
    [SerializeField] protected int maxAmount;
}
