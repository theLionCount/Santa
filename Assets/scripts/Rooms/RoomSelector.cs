using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSelector : MonoBehaviour
{

    public BlackScreen blackScreen;

    public List<GameObject> rooms;

    public GameObject startRoom;

    public List<GameObject> visitedRooms = new List<GameObject>();

    GameObject selectedRoom;
    public GameObject currentRoom;

    ProgressTracker progress;

    public static List<string> startRewards = new List<string>()
    {
        "BlasterTriggerMod",
        "BlasterAmmo",
        "BlasterMag",
        "RollNum",
        "RollNum",
        "PistolBullet",
        "PistolHammer",
        "PistolMag",
    };


    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        progress = GameObject.Find("Progress").GetComponent<ProgressTracker>();
        currentRoom = startRoom;
        currentRoom.GetComponent<Room>().setReward("ChooseWeapon", 0);
       // currentRoom.GetComponent<Room>().setDoors(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) changeRoom(startRewards[Random.Range(0,startRewards.Count)], 0);
    }

    string reward;
    int distance;

    public float waveMod = 1;

    public void changeRoom(string _reward, int _distance)
    {
        //float wave10thSize = 10f + progress.roomNum;
        //foreach (var item in rooms)
        //{
        //    for(int j = 0; j<item.GetComponent<Room>().waves.Count; j++)
        //    {
        //        item.GetComponent<Room>().waves[j] += (int)(item.GetComponent<Room>().waves[j] / wave10thSize);
        //    }
        //}

        waveMod += 0.1f;

        progress.roomNum++;
        int i = 0;
        do
        {
            i++;
            selectedRoom = rooms[Random.Range(0, rooms.Count)];
        } while (visitedRooms.Contains(selectedRoom) && i < 100);
        visitedRooms.Add(selectedRoom);
        if (!rooms.Any(t => !visitedRooms.Contains(t)))
        {
            visitedRooms.Clear();
            visitedRooms.Add(selectedRoom);
        }
        blackScreen.startFade();
        reward = _reward;
        distance = _distance;

    }

    public void changeRoomInBlack()
    {
        if (currentRoom != null)
        {
            currentRoom.GetComponent<Room>().purgeEnemies();
            Destroy(currentRoom);
        }
        currentRoom = Instantiate(selectedRoom);
        currentRoom.GetComponent<Room>().setReward(reward, distance);
        currentRoom.GetComponent<Room>().setDoors();
        for (int j = 0; j < currentRoom.GetComponent<Room>().waves.Count; j++)
        {
            currentRoom.GetComponent<Room>().waves[j] *= waveMod;
        }
    }

    public void changeRoomFinished()
    {
    }
}
