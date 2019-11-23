using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Level
{
   public string roomName;
  public  string roomType;
    // RoomType roomType;
}

public class Map : MonoBehaviour {

   // enum RoomType { COLLECT_THEM_ALL, SURVIVE };


    [SerializeField]
    int minAmountOfLevels = 4;
    [SerializeField]
    int maxAmountOfLevels = 6;
    [SerializeField]
    List<Level> collectThemAllRooms;
    [SerializeField]
    List<Level> surviveRooms;

    [SerializeField]
    MapRoom mapRoomPrefab;

    List<MapRoom> rooms;

    Vector3 firstRoomPosition;

    GameObject playerIcon;

    MapData mapData;

    float startTime;
    Vector3 startPosition;
    Vector3 endPosition;
    float journeyLength;
    bool shouldLerp = false;
    bool ready = false;
    // Use this for initialization
    void Start () {
        firstRoomPosition = transform.Find("FirstRoomPosition").transform.position;
        rooms = new List<MapRoom>();
        mapData = BetweenLevelDataContainer.instance.mapData;
        playerIcon = transform.Find("PlayerIcon").gameObject;

        GenerateLevel();         
        
       if(mapData.firstTime == false) StartCoroutine(GoToNextLevel());
       else
        {
            BetweenLevelDataContainer.instance.mapData = mapData;
            ready = true;
        }
        mapData.firstTime = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && ready)
        {
            SceneManager.LoadScene(mapData.roomsData[mapData.currentRoom].roomName);
        }
        
    }

    void FixedUpdate()
    {
        if (shouldLerp)
        {
            float distCovered = (Time.time - startTime) * 15f;


            float fracJourney = distCovered / journeyLength;


            playerIcon.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
            if (shouldLerp && fracJourney >= 1f)
            {
                shouldLerp = false;
                //AudioManager.instance.PlaySound("LevelUp");
                rooms[mapData.currentRoom].activate();
                ready = true;
            }
        }
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        mapData.currentRoom++;
        rooms[mapData.currentRoom - 1].complete();
        yield return new WaitForSeconds(0.5f);
        rooms[mapData.currentRoom - 1].deactivate();
        if (mapData.currentRoom >= rooms.Count)
        {
            Debug.Log("You won level!");
        }
        else
        {
            rooms[mapData.currentRoom - 1].deactivate();

            startPosition = playerIcon.transform.position;
            startTime = Time.time;
            endPosition = rooms[mapData.currentRoom].playerIconPosition.position;
            journeyLength = Vector3.Distance(startPosition, endPosition);
            shouldLerp = true;
            BetweenLevelDataContainer.instance.mapData = mapData;
        }
        //playerIcon.transform.position = rooms[currentRoom].playerIconPosition.position;
       


    }

    void GenerateLevel()
    {     
        SpawnRooms();
        rooms[mapData.currentRoom].activate();
        playerIcon.transform.position = rooms[mapData.currentRoom].playerIconPosition.position;
    }
    void SpawnRooms()
    {
        if (mapData.firstTime)
        {
            mapData.amountOfLevels = Random.Range(minAmountOfLevels, maxAmountOfLevels);
        }
        Vector3 pos = firstRoomPosition;
        if (mapData.firstTime)
        {          
            for (int i = 0; i < mapData.amountOfLevels; i++)
            {
                int typeNumber = Random.Range(0, 2); 
                    
                pos = new Vector2(firstRoomPosition.x, firstRoomPosition.y + 1.4f * ((i)));
                rooms.Add(Instantiate(mapRoomPrefab, pos, Quaternion.Euler(Vector3.zero)));
                if (typeNumber == 0)
                {
                    int levelNumber = Random.Range(0, surviveRooms.Count);
                    Level level = new Level();
                    level.roomType = "SURVIVE";
                    level.roomName = surviveRooms[levelNumber].roomName;
                    mapData.roomsData.Add(level);
                    rooms[i].initialize("SURVIVE");
                }
                else if (typeNumber == 1)
                {
                    int levelNumber = Random.Range(0, collectThemAllRooms.Count);
                    Level level = new Level();
                    level.roomType = "Collect Them All";
                    level.roomName = collectThemAllRooms[levelNumber].roomName;
                    mapData.roomsData.Add(level);
                    rooms[i].initialize("Collect Them All");
                }
            }
        }
        else
        {
            for (int i = 0; i < mapData.amountOfLevels; i++)
            {
                int typeNumber = Random.Range(0, 2);

                pos = new Vector2(firstRoomPosition.x, firstRoomPosition.y + 1.4f * ((i)));
                rooms.Add(Instantiate(mapRoomPrefab, pos, Quaternion.Euler(Vector3.zero)));
                rooms[i].initialize(mapData.roomsData[i].roomType);
            }
        }
        

    }

}
