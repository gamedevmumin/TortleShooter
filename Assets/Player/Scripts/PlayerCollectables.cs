using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PlayerCollectables : ScriptableObject
{
    public int KeysAmount { get; private set; }
    public int CoinsAmount { get; private set; }

    [FormerlySerializedAs("KeysStateChanged")] [SerializeField]
    private GameEvent keysStateChanged;

    public void Initialize()
    {
        KeysAmount = 0;
        CoinsAmount = 0;       
    }

    public void IncreaseKeysAmount(int amount)
    {
        KeysAmount+=amount;
        if(keysStateChanged) keysStateChanged.Raise();
    }

    public void IncreaseCoinsAmount(int amount)
    {
        CoinsAmount+=amount;
        keysStateChanged.Raise();
    }
}
