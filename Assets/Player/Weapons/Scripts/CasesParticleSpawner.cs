using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasesParticleSpawner : MonoBehaviour {
    [SerializeField]
    Transform magazinePlacement;
    [SerializeField]
    GameObject particleObject;

    void OnEnable () {
        Instantiate(particleObject, magazinePlacement.position, magazinePlacement.rotation);
    }
}