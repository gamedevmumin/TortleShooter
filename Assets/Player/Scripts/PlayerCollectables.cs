using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu]
public class PlayerCollectables : ScriptableObject
{
    public int KeysAmount { get; private set; }
    public int CoinsAmount { get; private set; }

    [SerializeField]
    GameEvent KeysStateChanged;

    public void Initialize()
    {
        KeysAmount = 0;
        CoinsAmount = 0;       
    }

    public void IncreaseKeysAmount(int amount)
    {
        KeysAmount+=amount;
        KeysStateChanged.Raise();
    }

    public void IncreaseCoinsAmount(int amount)
    {
        CoinsAmount+=amount;
        KeysStateChanged.Raise();
    }
}
