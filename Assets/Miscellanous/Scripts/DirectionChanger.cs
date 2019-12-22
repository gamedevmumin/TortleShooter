using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.GetComponent<EyeRunnerMovementManager>()?.ChangeDirection();
    }

}
