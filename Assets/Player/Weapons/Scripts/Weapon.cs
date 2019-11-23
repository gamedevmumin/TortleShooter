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

    PickableWeapon pickableWeapon;
    public PickableWeapon PickableWeapon { private set { pickableWeapon = value; } get { return pickableWeapon; } }
    protected List<IItem> items = new List<IItem>();
    protected List<IRandomBulletChanger> bulletChangingItems = new List<IRandomBulletChanger>();
    protected List<int> ints;
	void Start () {
        SceneManager.sceneLoaded += OnSceneLoaded;
        shotsIntervalTimer = shotsInterval;
        firePoint = transform.Find("FirePoint").transform;
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
        anim = GetComponent<Animator>();
        items.Add(new EvilEyeBullet());
        List<IItem> temp = items.FindAll(item => item is IRandomBulletChanger);
        foreach(IItem item in temp)
        {
            bulletChangingItems.Add(item as IRandomBulletChanger);
        }
        Debug.Log(bulletChangingItems[0].shouldWork());

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
                Instantiate(bullet, firePoint.position, transform.rotation);
                AudioManager.instance.PlaySound("RifleShooting");
            }
        }
        else
        {
            shotsIntervalTimer -= Time.deltaTime;
        }
    }

    public void OnPickUp(PickableWeapon pickableWeapon)
    {
        PickableWeapon = pickableWeapon;
    }


}
