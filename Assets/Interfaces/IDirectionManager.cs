using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirectionManager 
{
    bool IsRight { get; }
    void ManageDirection(Vector2 velocity);
}

