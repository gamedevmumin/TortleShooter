using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
    PlayerController player;
    
    Portal portal;

   

    void Awake()
    {
         
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            //SceneManager.sceneLoaded += OnSceneLoaded;
            instance = this;
           // DontDestroyOnLoad(this);
        }
        if (SceneManager.GetActiveScene().name == "Map")
        {
            GameObject portalGO = GameObject.Find("PORTAL");
            if (portalGO) portal = portalGO.GetComponent<Portal>(); ;
            if (portal) portal.gameObject.SetActive(false);
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        portal = GameObject.Find("PORTAL").gameObject.GetComponent<Portal>();
        portal.gameObject.SetActive(false);

    }

    // Use this for initialization
    void Start () {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        restartLevel();
    }

    void restartLevel()
    {
        if (Input.GetKey(KeyCode.R))
        {
            player.resetLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            AudioManager.instance.PlaySound("Music");
        }
    }

    public void finishLevel(LevelStatus levelStatus)
    {
        //if (levelStatus == LevelStatus.WON)
        {
            Debug.Log("YOU WON!!!");
            portal.gameObject.SetActive(true);
            portal.Open();
            AudioManager.instance.StopSound("Music");
            player.finishLevel();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        //LevelManager lM = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        // lM.StartLevel();

    }

}
