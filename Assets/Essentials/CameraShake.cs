using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{

    [SerializeField]
    Camera mainCam;

    float shakeAmount = 0;

    Image blinding;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
        if (blinding == null)
            blinding = GameObject.Find("Blind").GetComponent<Image>();
        blinding.color = new Color(blinding.color.r, blinding.color.g, blinding.color.b, 0f);
    }

    public void Shake(float amount, float length, bool prioritize = false)
    {
        if (!prioritize && shakeAmount > amount) return;
        if(prioritize) Debug.Log("Should be shaking");
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void BeginShake()
    {
        if (!(shakeAmount > 0)) return;
        var mainCamTransform = mainCam.transform;
        var camPos = mainCamTransform.position;

        var offsetX = Random.value * shakeAmount * 2 - shakeAmount;
        var offsetY = Random.value * shakeAmount * 2 - shakeAmount;
        camPos.x += offsetX;
        camPos.y += offsetY;
        //camPos.z = -10;
        mainCamTransform.position = camPos;
    }

    void StopShake()
    {
        shakeAmount = 0;
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = Vector3.zero;
    }

    public void blind()
    {
        StartCoroutine("BeginBlind");
    }

    IEnumerator BeginBlind()
    {
        blinding.color = new Color(blinding.color.r, blinding.color.g, blinding.color.b, 1f);
        yield return new WaitForSeconds(0.015f);
        blinding.color = new Color(blinding.color.r, blinding.color.g, blinding.color.b, 0f);
    }


}
