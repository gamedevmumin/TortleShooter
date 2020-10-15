using UnityEngine;

public class Weapon : MonoBehaviour {
	[SerializeField]
	Sprite icon;
	public Sprite Icon { get { return icon; } private set { icon = value; } }
	protected float shotsIntervalTimer;
	protected IRotatable rotator;
	protected IShooting shooting;
	protected IWeaponMagazineManager magazineManager;
	public PickableWeapon PickableWeapon { private set; get; }

	[SerializeField]
	protected WeaponStats stats;

	[SerializeField]
	PlayerStats playerStats;
	bool canPlayOutOfAmmoSound = true;

	private void Awake () {
		rotator = GetComponent<IRotatable> ();
		shooting = GetComponent<IShooting> ();
		magazineManager = GetComponent<IWeaponMagazineManager> ();
		magazineManager.Initialize (stats);
		shooting.Initialize(stats);
	}

	void Start () {
		shotsIntervalTimer = 0f;
	}

	void Update () {
		rotator.Rotate ();
		ManageShooting ();
	}

	virtual protected void ManageShooting () {
		if (shotsIntervalTimer <= 0f) {
			if (Input.GetButton ("Fire1")) {
				if (magazineManager.IsMagazineEmpty () == false) {
					if (magazineManager.IsReloading == false) {
						shooting.Shoot ();
						shotsIntervalTimer = stats.ShotsInterval;
						canPlayOutOfAmmoSound = true;
					}
				} else if (canPlayOutOfAmmoSound) {
					AudioManager.instance.PlaySound ("OutOfAmmo");
					canPlayOutOfAmmoSound = false;
				}
			}
			if (Input.GetButtonDown ("Fire1") && magazineManager.IsMagazineEmpty ()) {
				AudioManager.instance.PlaySound ("OutOfAmmo");
			}
		} else {
			shotsIntervalTimer -= Time.deltaTime;
		}
	}

	public void OnPickUp (PickableWeapon pickableWeapon) {
		PickableWeapon = pickableWeapon;
	}
}