using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider damageTakenBar;

    [SerializeField] private EnemyStartingStats enemyStartingStats;

    [SerializeField] private EnemyStats enemyStats;

    [SerializeField] private float timeToShrink = 0.5f;
    [SerializeField] private float shrinkingTimer;
    
    // Start is called before the first frame update
    private void Start()
    {
        healthBar.maxValue = enemyStartingStats.MaxHP;
        healthBar.value = enemyStartingStats.MaxHP;
        damageTakenBar.maxValue = healthBar.maxValue;
        damageTakenBar.value = healthBar.value;
    }

    private void Update()
    {
        shrinkingTimer -= Time.deltaTime;
        if (shrinkingTimer < 0f)
        {
            if (healthBar.value < damageTakenBar.value)
            {
                Debug.Log("But it is!");
                damageTakenBar.value -= 800 * Time.deltaTime;
            }
        }
    }
    
    public void UpdateHealthBar()
    {
        healthBar.value = enemyStats.CurrentHP;
        shrinkingTimer = timeToShrink;
    }
}
