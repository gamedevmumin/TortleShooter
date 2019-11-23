using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour {
    public List<Item> EquippedItems { get; private set; }

    private void Awake()
    {
        EquippedItems = new List<Item>();
    }

}
