using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelStatus { WON, LOST, IN_PROGRESS };

public class LevelManager : MonoBehaviour {


    public LevelStatus levelStatus { get; protected set; }

    [SerializeField]
    protected List<Spawner> spawners;

    // Use this for initialization
    void Start () {
        levelStatus = new LevelStatus();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual protected void Win()
    {
        levelStatus = LevelStatus.WON;
        foreach (Spawner spawner in spawners)
        {
            spawner.KillThemAll();
            spawner.shouldSpawn = false;
        }
        GameManager.instance.finishLevel(levelStatus);
    }

    virtual public void StartLevel()
    {

    }
}
