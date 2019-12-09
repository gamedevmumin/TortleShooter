using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenLevelDataContainer : MonoBehaviour {

    public static BetweenLevelDataContainer instance { get; private set; }

    //public PlayerStats playerStats = new PlayerStats();
    public MapData mapData = new MapData();
    public bool FirstScene { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            FirstScene = true;
            //playerStats.currentHP = playerStats.maxHP;
            instance = this;
        }
        else if (instance != this)
        {
            instance.FirstScene = false;
            Destroy(gameObject);
        }
    }

}
