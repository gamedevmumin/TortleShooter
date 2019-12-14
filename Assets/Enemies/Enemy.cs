using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    IAIManager AIManager;
    private void Awake()
    {
        AIManager = GetComponent<IAIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AIManager.ManageAI();
    }
}

