using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    IDestructible destructible;
    [SerializeField]
    PlayerCollectables playerCollectables;
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
                playerCollectables.IncreaseKeysAmount(1);
                OnCollection.Raise();
                if (destructible != null) destructible.Destroy(transform);
                isCollected = true;
            }
        }
    }
}
