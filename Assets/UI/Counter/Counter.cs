using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

  //  CollectorManager collectorManager;
    Text text;

    int amount;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        //collectorManager = GameObject.Find("LevelManager").GetComponent<CollectorManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateCounter(int amount)
    {
        this.amount = amount;
        text.text = amount.ToString();
    }
}
