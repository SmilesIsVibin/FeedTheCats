using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("Spawner Info")]
    [SerializeField] public GameManager gameManager;
    [SerializeField] public float timer;
    [SerializeField] public float minTimer;
    [SerializeField] public float spawnTimer;
    [SerializeField] public float currentTimer;
    [SerializeField] public float secondsToEvent;
    [SerializeField] public bool difficultyProgress;

    [Header("Food Spawner Info")]
    [SerializeField] public List<Spawners> foodSpawnerList;
    [SerializeField] public Spawners spawnerHolder;
    [SerializeField] public GameObject foodSpawnedObjects;

    [Header("Powerup Spawner Info")]
    [SerializeField] public List<Spawners> powerUpSpawnerList;
    [SerializeField] public Spawners powerUpHolder;
    [SerializeField] public GameObject powerupSpawnedObjects;


    private void Start()
    {
        SpawnFood();
    }

    private void Update()
    {
        if (gameManager.active)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTimer)
            {
                SpawnFood();
                SpawnPowerUps();
                timer = 0;
            }
            if (difficultyProgress)
            {
                currentTimer += Time.deltaTime;
                if (currentTimer >= secondsToEvent)
                {
                    spawnTimer--;
                    if (spawnTimer <= minTimer)
                    {
                        difficultyProgress = false;
                    }
                    currentTimer = 0f;
                }
            }
        }
    }

    public void SpawnFood()
    {
        for(int i = 0; i < foodSpawnerList.Count; i++)
        {
            spawnerHolder = foodSpawnerList[i];
            spawnerHolder.itemToSpawn = Random.Range(1, spawnerHolder.maxItemSpawn + 1);
            for (int j = 0; j < spawnerHolder.itemToSpawn; j++)
            {
                int randNo = Random.Range(0, spawnerHolder.foodItemList.Count);
                Vector3 randomPos = Random.insideUnitCircle * spawnerHolder.spawnRadius;
                GameObject spawnedObject = Instantiate(spawnerHolder.foodItemList[randNo], spawnerHolder.transform.position + randomPos, Quaternion.identity);
                spawnedObject.transform.SetParent(foodSpawnedObjects.transform);
            }
        }
    }

    public void SpawnPowerUps()
    {
        for (int k = 0; k < powerUpSpawnerList.Count; k++)
        {
            powerUpHolder = powerUpSpawnerList[k];
            powerUpHolder.itemToSpawn = Random.Range(1, powerUpHolder.maxItemSpawn + 1);
            for (int l = 0; l < powerUpHolder.itemToSpawn; l++)
            {
                int randNo = Random.Range(0, powerUpHolder.foodItemList.Count);
                Vector3 randomPos = Random.insideUnitCircle * powerUpHolder.spawnRadius;
                GameObject spawnedObject = Instantiate(powerUpHolder.foodItemList[randNo], powerUpHolder.transform.position + randomPos, Quaternion.identity);
                spawnedObject.transform.SetParent(powerUpHolder.transform);
            }
        }
    }
}
