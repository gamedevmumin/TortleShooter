﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenLevelDataContainer : MonoBehaviour {

    public static BetweenLevelDataContainer instance { get; private set; }

    public MapData mapData = new MapData();
    public bool FirstScene { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            FirstScene = true;
            instance = this;
        }
        else if (instance != this)
        {
            instance.FirstScene = false;
            Destroy(gameObject);
        }
    }

}
