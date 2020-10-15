using UnityEngine;

public class DamageInfo {
    public int MinDamage;
    public int MaxDamage;
    public int DamageDone;
    public Transform DamageDealer;
    public bool WasCritical;

    public DamageInfo (int minDamage, int maxDamage, Transform damageDealer, bool wasCritical) {
        WasCritical = wasCritical;
        MinDamage = WasCritical ? minDamage * 2 : minDamage;
        MaxDamage = WasCritical ? maxDamage * 2 : maxDamage;
        DamageDone = Random.Range (minDamage, maxDamage);
        DamageDone = WasCritical ? DamageDone * 2 : DamageDone;
        DamageDealer = damageDealer;      
    }

    public DamageInfo (int minDamage, int maxDamage, Transform damageDealer) {
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        DamageDone = Random.Range (minDamage, maxDamage);
        DamageDealer = damageDealer;
        WasCritical = false;
    }

    public DamageInfo () {
        MinDamage = 0;
        MaxDamage = 0;
        DamageDone = 0;
        DamageDealer = null;
        WasCritical = false;
    }
}