﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Congratz : MonoBehaviour {
    Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        gameObject.SetActive(false);
	}

    public void Display(string time)
    {
        text.text = "Congratz! \n You survived " + time + " minutes";
    }
}
