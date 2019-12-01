using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour, IDashing
{
    [SerializeField]
    PlayerStats stats;
    bool dashed = false;
    CameraShake cameraShake;
    void Start()
    {
        cameraShake = GameObject.Find("CameraShake").GetComponent<CameraShake>();
    }
    public void Dash()
    {
        if (!dashed)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        cameraShake.Shake(0.01f, 0.01f);
        stats.speed.Value += 685;
        dashed = true;
        yield return new WaitForSeconds(0.15f);
        stats.speed.Value -= 685;
        yield return new WaitForSeconds(0.03f);
        dashed = false;        
    }
}
