using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSelector : MonoBehaviour
{

    public BlackScreen blackScreen;

    public List<GameObject> rooms;

    public List<GameObject> visitedRooms = new List<GameObject>();

    GameObject selectedRoom;
    public GameObject currentRoom;

    ProgressTracker progress;

    public static List<string> startRewards = new List<string>()
    {
        "MaxHealth",
        "MaxHealth",
        "BlasterTriggerMod",
        "BlasterAmmo",
        "BlasterMag",
        "RollNum",
        "RollNum",
        "RollDistance",
        "RollDistance",
    };


    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.Find("Progress").GetComponent<ProgressTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) changeRoom(startRewards[Random.Range(0,startRewards.Count)], 0);
    }

    string reward;
    int distance;

    public void changeRoom(string _reward, int _distance)
    {
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
    }

    public void changeRoomFinished()
    {
    }
}
