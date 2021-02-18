using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerState : ScriptableObject
{
    public bool IsAbleToMove { get; set; }

    private void Awake()
    {
        IsAbleToMove = true;
    }
}
