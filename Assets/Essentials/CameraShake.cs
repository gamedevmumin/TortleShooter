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

    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void BeginShake()
    {
        if (shakeAmount > 0)
        {

            Vector3 camPos = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;
            //camPos.z = -10;
            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
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
