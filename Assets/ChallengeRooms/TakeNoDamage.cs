using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeNoDamage : LevelManager
{
    [SerializeField] int minSurvivalTime = 10;
    [SerializeField] int maxSurvivalTime = 20;

    float survivalTime;
    float survivalTimer;

    TimerUI timerUI;

    PlayerController playerController;

    Transform start;

    [SerializeField] CameraShake cameraShake;
    [SerializeField] private GameObject rewardObject;
    [SerializeField] private GameObject winText;

    override public void StartLevel()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        start = transform.Find("StartingPosition");
        playerController.transform.position = start.position;
    }

    // Use this for initialization
    void Start()
    {
        survivalTime = Random.Range(minSurvivalTime, maxSurvivalTime);
        survivalTimer = survivalTime;
        timerUI = GameObject.Find("Timer").GetComponent<TimerUI>();
        if (timerUI != null) timerUI.SetTimer(survivalTime);
        if (AudioManager.instance) AudioManager.instance.PlaySound("Music");
        levelStatus = LevelStatus.IN_PROGRESS;
        cameraShake.Shake(0.02f, 1f);
    }

    protected void FinishLevel()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.KillThemAll();
            spawner.shouldSpawn = false;
        }
    }

    protected override void Win()
    {
        levelStatus = LevelStatus.WON;
        FinishLevel();
       // rewardObject.SetActive(true);
        AudioManager.instance.PlaySound("Win");
        winText.SetActive(true);
        cameraShake.Shake(0.02f, 1f);
        StartCoroutine(SpawnReward());
        //GameManager.instance.finishLevel(levelStatus);
    }

    public void Lose()
    {
        levelStatus = LevelStatus.LOST;
        FinishLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerUI.time <= 0f && levelStatus != LevelStatus.WON)
        {
            Debug.Log("Level Won");
            Win();
        }
    }

    private IEnumerator SpawnReward()
    {
        yield return new WaitForSeconds(1.2f);
        rewardObject.SetActive(true);
    }
}