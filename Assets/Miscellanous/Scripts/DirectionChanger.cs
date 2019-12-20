using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Runner")
        {
            Debug.Log("XD2");
            coll.GetComponent<EyeRunnerMovementManager>().ChangeDirection();
        }
    }

}
