using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    IAIManager AIManager;
    [SerializeField]
    SceneContents sceneContents;

    private void Awake()
    {
        AIManager = GetComponent<IAIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AIManager!=null) AIManager.ManageAI();
    }

    private void OnEnable()
    {
        sceneContents.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        sceneContents.UnregisterEnemy(this);
    }
}

