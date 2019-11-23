using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenLevelDataContainer : MonoBehaviour {

    public static BetweenLevelDataContainer instance { get; private set; }

    public PlayerStats playerStats = new PlayerStats();
    public MapData mapData = new MapData();
    public bool firstScene = true;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            playerStats.currentHP = playerStats.maxHP;
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

}
