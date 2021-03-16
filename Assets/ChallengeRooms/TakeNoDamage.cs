using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeNoDamage : LevelManager
{
    [SerializeField]
    int minSurvivalTime = 10;
    [SerializeField]
    int maxSurvivalTime = 20;

    float survivalTime;
    float survivalTimer;

    TimerUI timerUI;

    PlayerController playerController;

    Transform start;

    [SerializeField] CameraShake cameraShake;
    
    override public void StartLevel()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        start = transform.Find("StartingPosition");
        playerController.transform.position = start.position;
    } 

    // Use this for initialization
    void Start () {
        survivalTime = Random.Range(minSurvivalTime, maxSurvivalTime);
        survivalTimer = survivalTime;
        timerUI = GameObject.Find("Timer").GetComponent<TimerUI>();
        if(timerUI!=null) timerUI.SetTimer(survivalTime);
        if(AudioManager.instance) AudioManager.instance.PlaySound("Music");
        levelStatus = LevelStatus.IN_PROGRESS;  
        cameraShake.Shake(0.02f, 1f);
    }
	
    // Update is called once per frame
    void Update ()
    {
        if(timerUI.time<=0f && levelStatus!=LevelStatus.WON)
        {
            Debug.Log("Level Won");
            Win();
        }
    }
}
