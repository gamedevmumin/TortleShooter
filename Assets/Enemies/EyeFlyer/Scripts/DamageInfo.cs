using UnityEngine;

public class DamageInfo
{
    public int minDamage;
    public int maxDamage;
    public int damageDone;
    public Transform damageDealer;
    public DamageInfo()
    {
        minDamage = 0;
        maxDamage = 0;
        damageDone = 0;
        damageDealer = null;
    }
}