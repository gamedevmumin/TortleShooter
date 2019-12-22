using UnityEngine;

public class DamageInfo
{
    public int MinDamage;
    public int MaxDamage;
    public int DamageDone;
    public Transform DamageDealer;
    public DamageInfo(int minDamage, int maxDamage, Transform damageDealer)
    {
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        DamageDone = Random.Range(minDamage, maxDamage);
        DamageDealer = damageDealer;
    }

    public DamageInfo()
    {
        MinDamage = 0;
        MaxDamage = 0;
        DamageDone = 0;
        DamageDealer = null;
    }
}