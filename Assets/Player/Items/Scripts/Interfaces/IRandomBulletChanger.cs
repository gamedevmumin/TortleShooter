using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRandomBulletChanger : IRandomChanceItem
{
    Bullet BulletToChangeFor { get; }
}
