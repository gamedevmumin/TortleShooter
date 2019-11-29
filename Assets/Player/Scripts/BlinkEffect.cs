using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlinkEffect : VisualSpriteEffect
{

    public override IEnumerator PlayEffect(SpriteRenderer sR)
    {
        sR.material.SetFloat("_FlashAmount", 1f);
        yield return new WaitForSeconds(0.03f);
        sR.material.SetFloat("_FlashAmount", 0f);
    } 
}
