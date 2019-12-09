using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorManager : LevelManager
{

    [System.Serializable]
    struct SpawnRange
    {
        [SerializeField] public Transform leftBorder;
        [SerializeField] public Transform rightBorder;
    }


    [SerializeField]
    int minObjectsToCollect = 5;
    [SerializeField]
    int maxObjectsToCollect = 5;

    int amountOfObjectsToCollect;

    int objectsRemaining;

    int lastRange = -37;

    [SerializeField]
    CollectableObjective objective;

    [SerializeField]
    List<SpawnRange> spawnRanges;

    Counter counter;
    PlayerController playerController;
    Transform start;

    void Start()
    {
        amountOfObjectsToCollect = Random.Range(minObjectsToCollect, maxObjectsToCollect);
        objectsRemaining = amountOfObjectsToCollect;
        counter = GameObject.Find("Counter").GetComponent<Counter>();
        if (counter != null)
        {
            counter.UpdateCounter(amountOfObjectsToCollect);
        }
        SpawnObjective();
        if(AudioManager.instance) AudioManager.instance.PlaySound("Music");
    }

    override public void StartLevel()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        start = transform.Find("StartingPosition");
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfObjectsToCollect <= 0)
        {
            Debug.Log("Yeah");
            Win();
        }

    }

    void SpawnObjective()
    {
        int whichRange = Random.Range(0, spawnRanges.Count);
        while (whichRange == lastRange)
        {
            whichRange = Random.Range(0, spawnRanges.Count);
        }
        if (spawnRanges.Count > 1) lastRange = whichRange;
        float x = Random.Range(spawnRanges[whichRange].leftBorder.position.x, spawnRanges[whichRange].rightBorder.position.x);
        Vector2 spawnPos = new Vector2(x, spawnRanges[whichRange].leftBorder.position.y);
        CollectableObjective obj = Instantiate(objective, spawnPos, Quaternion.Euler(Vector3.zero)) as CollectableObjective;
        obj.initialize(this);
    }

    public void OnCollection()
    {
        amountOfObjectsToCollect -= 1;
        if (counter != null)
        {
            counter.UpdateCounter(amountOfObjectsToCollect);
        }
        if (amountOfObjectsToCollect > 0) SpawnObjective();            
    }
}
