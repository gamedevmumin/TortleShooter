using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract string Name { get; protected set; }
    public abstract Sprite Icon { get; protected set; }
    public abstract PickableItem PickableItem { get; protected set; }
    public abstract void OnPickUp(PickableItem pickableItem);

}
