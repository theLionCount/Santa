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

    RoomSelector rs;

    Arrow arrow;

    public float showArrowCD;

    public static List<string> possibleRewards = new List<string>()
    {
        "Health",
        "MaxHealth",
        "RollNum",
    };

    public static List<string> possibleCommonRewards = new List<string>()
    {
        "Dmg",
        "Armour",
        "BleedChance",
        "DamageToHealthy",
        "DodgeChance",
        "MaxHealthSmall",
        "RollDistance",
        "SmallHealth",
        "Speed",
        "Coins",
        "RollDmgReduction",
        "Speed",
    };



    public static List<string> notinFirstRoom = new List<string>()
    {
        "Health",
        "MaxHealth",
        "RollDmgReduction",
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
            if (reward.StartsWith("Blaster") || reward.StartsWith("Pistol")) possibleRewards.Remove(reward);
        }
        else selectedReward = transform.Find("Rewards").Find(Random.Range(0,3) == 0 ? "SmallHealth" : "Coins").gameObject;
    }

    public static int commonStreak = 0;

    public void setDoors(bool first = false)
    {
        var common = Random.Range(0, 3) < 2;
        if (first) common = false;
        if (common) commonStreak += 1;
        else commonStreak = 0;
        if (commonStreak >= 5)
        {
            common = false;
            commonStreak = 0;
        }
        doors = GetComponentsInChildren<Door>();
        List<string> chosen = new List<string>();
        foreach (var item in doors)
        {
            string rID;
            do
            {
                rID = common ? possibleCommonRewards[Random.Range(0, possibleCommonRewards.Count)] : possibleRewards[Random.Range(0, possibleRewards.Count)];
            } while ((first && notinFirstRoom.Contains(rID)) || chosen.Contains(rID));
            chosen.Add(rID);
            item.reward = rID;
            item.rewardPrompt = transform.Find("Rewards").Find(rID).gameObject.GetComponent<InteractEffect>().getDoorPrompt();
            item.distance = first ? 1 : 1; // Random.Range(2, 5);
            item.prompt.color = common ? Color.yellow : new Color(1f, 0.6f, 0);
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
        GameObject.Find("Player").GetComponent<ActiveAbilities>().resetCharges();
        rs = GameObject.Find("GamePlay").GetComponent<RoomSelector>();
        startCD = 50;
        arrow = GameObject.Find("Player").GetComponentInChildren<Arrow>(true);
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
                arrow.gameObject.SetActive(false);
            }
        }
        else startCD--;

        if (livingEnemies.Count > 0)
        {
            showArrowCD++;
            foreach (var item in livingEnemies)
            {
                var screenPos = Camera.main.WorldToScreenPoint(item.transform.position);
                if (screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height) showArrowCD = 0;
            }
            if (showArrowCD > 120)
            {
                arrow.gameObject.SetActive(true);
                arrow.setEnemy(livingEnemies.OrderBy(t => (t.transform.position - arrow.transform.position).magnitude).FirstOrDefault());
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
        }
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
