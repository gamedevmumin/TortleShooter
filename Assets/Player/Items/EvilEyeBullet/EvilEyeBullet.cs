using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilEyeBullet : Item, IRandomBulletChanger
{
    [SerializeField]
    new string name;
    [SerializeField]
    private Bullet bulletToChangeFor;
    [SerializeField]
    private Sprite icon;
    private PickableItem pickableItem;
    public override Sprite Icon { get { return icon;  } protected set { icon = value; } }
    public override string Name { get => name; protected set { name = value; } }
    public Bullet BulletToChangeFor => bulletToChangeFor;

    public override PickableItem PickableItem { get; protected set; }

    public override void OnPickUp(PickableItem pickableItem)
    {
        PickableItem = pickableItem;
    }

    public bool shouldWork()
    {
        return true;
    }
}
