using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppearence : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    int chanceToAppear;

    void Start()
    {
        int randomNumber = Random.Range(0, 100);
        if(chanceToAppear>=randomNumber)
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
