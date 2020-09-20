using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionManager : MonoBehaviour, IDirectionManager
{

    public bool IsRight { get; private set; }
    [SerializeField]
    private SpriteRenderer sR;
    [SerializeField]
    ParticleSystem dashEffect;

    void Start()
    {
        IsRight = true;
    }

    void Flip()
    {
        IsRight = !IsRight;
        sR.flipX = !sR.flipX;
        dashEffect.transform.localScale = new Vector2(dashEffect.transform.localScale.x * -1, dashEffect.transform.localScale.y );
        dashEffect.transform.rotation = Quaternion.Euler(dashEffect.transform.rotation.x + 180f, dashEffect.transform.rotation.y, dashEffect.transform.rotation.z);
    }

    public void ManageDirection(Vector2 velocity)
    {
        if ((velocity.x < 0 && IsRight) || (velocity.x > 0 && !IsRight))
            Flip();
    }
}
