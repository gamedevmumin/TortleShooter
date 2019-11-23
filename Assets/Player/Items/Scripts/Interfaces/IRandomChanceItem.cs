using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRandomChanceItem : IItem
{
    bool shouldWork(); 
}
