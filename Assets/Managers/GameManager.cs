using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
    PlayerController player;

    Portal portal;

    void Awake () {
        if (instance != null) {
            if (instance != this) {
                Destroy (this.gameObject);
            }
        } else {
            instance = this;
        }
        if (SceneManager.GetActiveScene ().name == "Map") {
        GameObject portalGO = GameObject.Find ("PORTAL");
            if (portalGO) portal = portalGO.GetComponent<Portal> ();;
            if (portal) portal.gameObject.SetActive (false);
            return;
        }
        portal = GameObject.Find ("PORTAL").gameObject.GetComponent<Portal> ();
        portal.gameObject.SetActive (false);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey (KeyCode.R)) {
            restartLevel ();
        }
    }

    void restartLevel () {
        player.resetLevel ();
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        AudioManager.instance.PlaySound ("Music");
    }

    public void finishLevel (LevelStatus levelStatus) {
        portal.gameObject.SetActive (true);
        portal.Open ();
        AudioManager.instance.StopSound ("Music");
        player.finishLevel ();
    }

}