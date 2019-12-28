using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppearence : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    int chanceToAppear;
    [SerializeField]
    List<SpawnRange> spawnRanges;

    void Start()
    {
        int randomNumber = Random.Range(0, 100);
        if (spawnRanges.Count > 0)
        {
            int whichRange = Random.Range(0, spawnRanges.Count);
            float x = Random.Range(spawnRanges[whichRange].leftBorder.position.x, spawnRanges[whichRange].rightBorder.position.x);
            transform.position = new Vector2(x, spawnRanges[whichRange].leftBorder.position.y);
        }
        if (chanceToAppear>=randomNumber)
        {
            Debug.Log(chanceToAppear + " >= " + randomNumber);
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
