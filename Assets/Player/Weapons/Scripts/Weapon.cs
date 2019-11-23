using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Weapon : MonoBehaviour {

    protected bool isOnRight = true;
    [SerializeField]
    protected Bullet bullet;
    protected Transform firePoint;
    protected CameraShake cameraShake;
    protected Animator anim;
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get { return icon; }  private set { icon = value; } }
    protected float shotsIntervalTimer;
    [SerializeField]
    protected float shotsInterval;
    [SerializeField]
    protected float shakeAmount;

    public PickableWeapon PickableWeapon { private set; get; }
    protected PlayerItems playerItems;
    protected List<IRandomBulletChanger> bulletChangingItems = new List<IRandomBulletChanger>();
    protected List<int> ints;
	void Start () {
        SceneManager.sceneLoaded += OnSceneLoaded;
        shotsIntervalTimer = shotsInterval;
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        firePoint = transform.Find("FirePoint").transform;
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        anim = GetComponent<Animator>();
        List<Item> temp = playerItems.EquippedItems.FindAll(item => item is IRandomBulletChanger);
        foreach (Item item in temp)
        {
            bulletChangingItems.Add(item as IRandomBulletChanger);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
    }

    void Update () {
        ManageRotation();
        ManageShooting();
    }

    protected void ManageRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle));

        if (mousePos.x > 0 && !isOnRight)
        {
            isOnRight = !isOnRight;
            Vector2 newScale = transform.localScale;
            newScale.y *= -1;
            transform.localScale = newScale;
        }
        else if (mousePos.x < 0 && isOnRight)
        {
            isOnRight = !isOnRight;
            Vector2 newScale = transform.localScale;
            newScale.y *= -1;
            transform.localScale = newScale;
        }
    }

    virtual protected void ManageShooting()
    {
        if (shotsIntervalTimer <= 0f)
        {
            if (Input.GetButton("Fire1"))
            {
                shotsIntervalTimer = shotsInterval;
                anim.SetTrigger("Shot");
                cameraShake.Shake(shakeAmount, 0.1f);            
                Instantiate(ChooseBulletToShoot(), firePoint.position, transform.rotation);
                AudioManager.instance.PlaySound("RifleShooting");
            }
        }
        else
        {
            shotsIntervalTimer -= Time.deltaTime;
        }
    }

    virtual protected Bullet ChooseBulletToShoot()
    {
        Bullet bulletToShoot;
        foreach(IRandomBulletChanger irbc in bulletChangingItems)
        {
            if(irbc.shouldWork())
            {
                bulletToShoot = irbc.BulletToChangeFor;
            }
        }
        bulletToShoot = bullet;
        return bulletToShoot;
    }

    public void OnPickUp(PickableWeapon pickableWeapon)
    {
        PickableWeapon = pickableWeapon;
    }


}
