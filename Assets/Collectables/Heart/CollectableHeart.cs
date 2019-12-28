using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHeart : MonoBehaviour
{
    IDestructible destructible;
    [SerializeField]
    PlayerStats playerStats;
    [SerializeField]
    GameEvent OnCollection;

    bool isCollected = false;

    private void Awake()
    {
        destructible = GetComponent<IDestructible>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected)
        {
            if (collision.CompareTag("Player"))
            {
                if (playerStats.currentHP < playerStats.maxHP)
                {
                    playerStats.currentHP++;

                    OnCollection.Raise();
                    if (destructible != null) destructible.Destroy(transform);
                    isCollected = true;
                }
            }
        }
    }
}
