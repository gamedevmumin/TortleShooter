using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilEyeBullet : IRandomBulletChanger
{
    private Bullet bulletToChangeFor;

    public Bullet BulletToChangeFor => bulletToChangeFor;

    public string Name => "EvilEyeBullet";

    public bool shouldWork()
    {
        return true;
    }
}
