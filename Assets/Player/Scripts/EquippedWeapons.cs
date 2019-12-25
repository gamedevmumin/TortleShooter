using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquippedWeapons : ScriptableObject
{
    public Weapon ActiveWeapon { get; set; }
    public Weapon InactiveWeapon { get; set; }
}
