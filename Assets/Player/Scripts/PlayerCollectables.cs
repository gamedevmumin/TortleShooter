using UnityEngine;

[CreateAssetMenu]
public class PlayerCollectables : ScriptableObject
{
    public int KeysAmount { get; private set; }
    public int CoinsAmount { get; private set; }

    [SerializeField] 
    private GameEvent keysStateChanged;

    public void Initialize()
    {
        KeysAmount = 0;
        CoinsAmount = 0;       
    }

    public void IncreaseKeysAmount(int amount)
    {
        KeysAmount+=amount;
        keysStateChanged.Raise();
    }

    public void IncreaseCoinsAmount(int amount)
    {
        CoinsAmount+=amount;
        keysStateChanged.Raise();
    }
}
