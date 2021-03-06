﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

    Text timerLabel;
    [SerializeField]
    Congratz congratz;
    string congratzTime;
    public float time;

    public bool isStopped = false;

    //double minutes = 0;
    void Start () {
        timerLabel = GetComponent<Text> ();
    }

    public void SetTimer (float maxTime) {
        time = maxTime;
    }

    public void StopTimer()
    {
        isStopped = true;
    }
    
    void Update () {
        if (!isStopped) {
            time -= Time.deltaTime;

            var minutes = Mathf.Floor (time / 60); //Divide the guiTime by sixty to get the minutes.

            var seconds = time % 60; //Use the euclidean division for the seconds.
            var fraction = (time * 100) % 100;
            if (seconds <= 0) {
                seconds = 59;
                minutes--;
            }

            //update the label value  + ":"+
            
            if (time<= 0) isStopped = true;
            timerLabel.text = string.Format ("{0:00}", minutes) + ":" + string.Format ("{0:00}", seconds) + ":" + string.Format ("{0:00}", fraction);
            congratzTime = string.Format ("{0:00}", minutes) + ":" + string.Format ("{0:00}", seconds);

        } else
        {
            timerLabel.enabled = false;
            //congratz.Display (congratzTime);
        }
    }
}