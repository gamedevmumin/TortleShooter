﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour {

    [System.Serializable]
    struct EnemySpawnChance
    {
        [SerializeField]
        public Enemy enemy;
        [SerializeField] [Range(0, 100)]
        public int chance;
        [SerializeField]
        public int minAmount;
        [SerializeField]
        public int maxAmount;
    }

    [SerializeField]
    float firstSpawnTime = 3f;

    [SerializeField]
    float spawnInterval;
    float spawnIntervalTimer;

    [SerializeField]
    List<EnemySpawnChance> enemies;

    [SerializeField]
    SceneContents sceneContents;
    public bool shouldSpawn = true;

   

	// Use this for initialization
	void Start () {
        spawnIntervalTimer = firstSpawnTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldSpawn)
        {
            if (spawnIntervalTimer <= 0f)
            {
                
                spawnIntervalTimer = spawnInterval;
                StartCoroutine(Spawn());
            }
            spawnIntervalTimer -= Time.deltaTime;
        }
	}

    IEnumerator Spawn()
    {
        double randomNumber = Random.Range(0, 100);
        int enemyToSpawn = 0;
        foreach (EnemySpawnChance enemy in enemies)
        {
            if (randomNumber <= enemy.chance)
            {
                StartCoroutine("spawnEnemies", enemyToSpawn);
            }
            yield return new WaitForSeconds(0.45f);
            enemyToSpawn++;
        }

    }

    public void KillThemAll()
    {
        foreach(Enemy spawn in sceneContents.Enemies.ToArray())
        {
            IKillable killable = null;
            if (spawn) killable = spawn.GetComponent<IKillable>();
            if(killable != null) killable.Die();          
        }
    }

    IEnumerator spawnEnemies(int enemyToSpawn)
    {
        
        int amount = Random.Range(enemies[enemyToSpawn].minAmount, enemies[enemyToSpawn].maxAmount);
        for (int i = 0; i < amount; i++)
        {
            if (shouldSpawn)
            {
                Enemy enemy = Instantiate(enemies[enemyToSpawn].enemy, transform.position, transform.rotation) as Enemy;
            }
            else KillThemAll();
            yield return new WaitForSeconds(0.45f);
        }
    }
}
