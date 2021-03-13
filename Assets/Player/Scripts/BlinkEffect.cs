using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlinkEffect : VisualSpriteEffect
{
    private static readonly int Blink = Shader.PropertyToID("_Blink");

    public override IEnumerator PlayEffect(SpriteRenderer sR)
    {
        sR.material.SetInt(Blink, 1);
        yield return new WaitForSeconds(0.08f);
        sR.material.SetInt(Blink, 0);    
    }
}
