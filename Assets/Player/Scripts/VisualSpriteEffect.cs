using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualSpriteEffect : ScriptableObject
{
    public abstract IEnumerator PlayEffect(SpriteRenderer sR);
}
