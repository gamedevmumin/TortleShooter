﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Weapon : MonoBehaviour {
	[SerializeField]
	Sprite icon;
	public Sprite Icon { get { return icon; } private set { icon = value; } }
	protected float shotsIntervalTimer;
	protected IRotatable rotator;
	protected IShooting shooting;
	public PickableWeapon PickableWeapon { private set; get; }

	[SerializeField]
	protected WeaponStats stats;
	[SerializeField]
	PlayerStats playerStats;
	private int bulletsInMagazine;
	private void Awake () {
		rotator = GetComponent<IRotatable> ();
		shooting = GetComponent<IShooting> ();
	}

	void Start () {
		shotsIntervalTimer = 0f;
		bulletsInMagazine = stats.MagazineSize;
	}

	void Update () {
		rotator.Rotate ();
		ManageShooting ();
	}

	virtual protected void ManageShooting () {
		if (shotsIntervalTimer <= 0f) {
			if (Input.GetButton ("Fire1")) {
				if (bulletsInMagazine > 0) {
					shooting.Shoot (ref bulletsInMagazine);
					shotsIntervalTimer = stats.ShotsInterval;
				} else {

				}
			}
		} else {
			shotsIntervalTimer -= Time.deltaTime;
		}
	}

	public void OnPickUp (PickableWeapon pickableWeapon) {
		PickableWeapon = pickableWeapon;
	}
}