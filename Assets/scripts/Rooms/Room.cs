using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{

    List<IThreatLevel> enemies;
    public List<float> waves;
    EnemySpawner[] spawners;
    Door[] doors;
    int currentWave;

    public float waveCooldown;
    public float partialWaveCooldown;
    float cd;

    List<GameObject> livingEnemies = new List<GameObject>();
    bool doorsOpen;

    List<IThreatLevel> selectedEnemies;
    float minThreat;

    bool startPartialWave;

    Vector3 playerStartPos;
    float startCD;

    GameObject selectedReward;
    int distance;
    string cRevID;

    public static List<string> possibleRewards = new List<string>()
    {
        "Health",
        "MaxHealth",
        "MaxHealth",
        "BlasterTriggerMod",
        "BlasterAmmo",
        "BlasterMag",
        "RollNum",
        "RollDmgReduction",
        "RollDistance",
        "Dmg",
        "Armour",
    };

    public void setReward(string reward, int _distance)
    {
        cRevID = reward;
        distance = _distance;
        if (distance == 0)
        {
            selectedReward = transform.Find("Rewards").Find(reward).gameObject;
            if (reward.StartsWith("Blaster")) possibleRewards.Remove(reward);
        }
        else selectedReward = transform.Find("Rewards").Find(Random.Range(0,3) == 0 ? "SmallHealth" : "Coins").gameObject;
    }
    public void setDoors()
    {
        doors = GetComponentsInChildren<Door>();
        foreach (var item in doors)
        {

            var rID = possibleRewards[Random.Range(0, possibleRewards.Count)];
            item.reward = rID;
            item.rewardPrompt = transform.Find("Rewards").Find(rID).gameObject.GetComponent<InteractEffect>().getDoorPrompt();
            item.distance = Random.Range(2, 5);
        }
        if (distance > 0)
        {
            var item = doors[Random.Range(0, doors.Length)];
            var rID = cRevID;
            item.reward = rID;
            item.rewardPrompt = transform.Find("Rewards").Find(rID).gameObject.GetComponent<InteractEffect>().getDoorPrompt();
            item.distance = distance;
        }
    }

    public void purgeEnemies()
    {
        foreach (var item in livingEnemies)
        {
            if (item != null) Destroy(item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponentInChildren<EnemyHolder>().enemies;

        spawners = GetComponentsInChildren<EnemySpawner>();
       
        selectedEnemies = enemies;
        minThreat = selectedEnemies.Min(t => t.threatLevel);
        playerStartPos = transform.Find("PlayerStartPos").transform.position;
        GameObject.Find("Player").transform.position = playerStartPos;
        startCD = 50;
    }

    private void FixedUpdate()
    {
        if (startCD <= 0)
        {
            cd--;
            livingEnemies.RemoveAll(t => t == null || !t.activeSelf);
            if (startPartialWave)
            {
                if (cd <= 0) startNextWave();
            }
            else if (livingEnemies.Count <= 0 && cd <= 0)
            {
                cd = waveCooldown;
                if (currentWave < waves.Count) startNextWave();
                else if (!doorsOpen) createReward();
            }
        }
        else startCD--;
    }

    void startNextWave()
    {
        startPartialWave = false;
        float currentThreat = 0;
        List<EnemySpawner> usableSpawners = new List<EnemySpawner>(spawners);
        while (currentThreat + minThreat <= waves[currentWave] && usableSpawners.Count>0)
        {
            IThreatLevel selectedThreat;
            do
            {
                selectedThreat = selectedEnemies[Random.Range(0, selectedEnemies.Count)];
            } while (!(currentThreat + selectedThreat.threatLevel <= waves[currentWave]));
            currentThreat += selectedThreat.threatLevel;
            var spawn = usableSpawners[Random.Range(0, usableSpawners.Count)];
            usableSpawners.Remove(spawn);
            spawn.instantiate(selectedThreat.gameObject);
        }
        if (currentThreat + minThreat > waves[currentWave])
        {
            currentWave++;
        }
        else
        {
            startPartialWave = true;
            waves[currentWave] -= currentThreat;
            cd = partialWaveCooldown;
        }
    }

    void createReward()
    {
        doorsOpen = true;
        selectedReward.SetActive(true);
    }

    public void rewardTaken()
    {
        openDoors();
    }

    void openDoors()
    {
        foreach (var item in doors)
        {
            item.open();
        }
    }

    public void addLivingEnemy(GameObject enemy)
    {
        livingEnemies.Add(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
