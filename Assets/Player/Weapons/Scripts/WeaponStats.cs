using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponStats : ScriptableObject {
    [SerializeField]
    int criticalStrikeChance;
    [SerializeField]
    int minDamage;
    [SerializeField]
    int maxDamage;
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    float shakeAmount;
    [SerializeField]
    float spread;
    [SerializeField]
    string shotSound;
    [SerializeField]
    float shotsInterval;
    [SerializeField]
    float reloadTime;
    [SerializeField]
    int magazineSize;

    public Bullet Bullet { get => bullet; private set => bullet = value; }
    public float ShakeAmount { get => shakeAmount; private set => shakeAmount = value; }
    public float Spread { get => spread; private set => spread = value; }
    public string ShotSound { get => shotSound; private set => shotSound = value; }
    public float ShotsInterval { get => shotsInterval; private set => shotsInterval = value; }
    public float ReloadTime { get => reloadTime; private set => reloadTime = value; }
    public int MagazineSize { get => magazineSize; private set => magazineSize = value; }
    public int MinDamage { get => minDamage; private set => minDamage = value; }
    public int MaxDamage { get => maxDamage; private set => maxDamage = value; }
    public int CriticalStrikeChance { get => criticalStrikeChance; set => criticalStrikeChance = value; }
}