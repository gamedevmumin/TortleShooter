using System.Collections;
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
    float spawnInterval;
    float spawnIntervalTimer;

    [SerializeField]
    List<EnemySpawnChance> enemies;

    List<Enemy> spawnedEnemies;

    public bool shouldSpawn = true;

   

	// Use this for initialization
	void Start () {
        spawnIntervalTimer = spawnInterval;
        spawnedEnemies = new List<Enemy>();
        //Spawn();
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldSpawn)
        {
            if (spawnIntervalTimer <= 0f)
            {
                spawnIntervalTimer = spawnInterval;
                Spawn();
            }
            spawnIntervalTimer -= Time.deltaTime;
        }
	}

    void Spawn()
    {
        double randomNumber = Random.Range(0, 100);
        Debug.Log(randomNumber);
        int enemyToSpawn = 0;
        foreach (EnemySpawnChance enemy in enemies)
        {
            if (randomNumber <= enemy.chance)
            {
                StartCoroutine("spawnEnemies", enemyToSpawn);
            }
            enemyToSpawn++;
        }

    }

    public void KillThemAll()
    {
        foreach(Enemy spawn in spawnedEnemies)
        {
            if(spawn) spawn.Die();          
        }
        spawnedEnemies.Clear();
    }

    IEnumerator spawnEnemies(int enemyToSpawn)
    {
        int amount = Random.Range(enemies[enemyToSpawn].minAmount, enemies[enemyToSpawn].maxAmount);
        for (int i = 0; i < amount; i++)
        {
            if (shouldSpawn)
            {
                Enemy enemy = Instantiate(enemies[enemyToSpawn].enemy, transform.position, transform.rotation) as Enemy;
                spawnedEnemies.Add(enemy);
            }
            else KillThemAll();
            yield return new WaitForSeconds(0.45f);
        }
    }
}
