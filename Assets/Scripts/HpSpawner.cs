using UnityEngine;
using System.Collections.Generic;


public class HpSpawner : MonoBehaviour
{
     public GameObject[] objectsToSpawn2;
    public Transform spawnPointsParent2;
    public float spawnInterval2 = 1.0f;
    public Driver driverScript1;


    private Transform[] spawnPoints2;
    private float timer = 0f;

    private GameObject currentSpawnedObject2;

void Start()
    {
        spawnPoints2 = spawnPointsParent2.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        MaintainThreePotions();
    }

    void MaintainThreePotions()
    {
        int currentPotionCount = CountActivePotions();

        if (currentPotionCount < 3)
        {
            TrySpawn();
        }
    }

    int CountActivePotions()
    {
        int count = 0;

        for (int i = 1; i < spawnPoints2.Length; i++)
        {
            if (spawnPoints2[i].childCount > 0)
            {
                count++;
            }
        }

        return count;
    }

    void TrySpawn()
    {
        List<Transform> freeSpawnPoints = new List<Transform>();

        for (int i = 1; i < spawnPoints2.Length; i++)
        {
            if (spawnPoints2[i].childCount == 0)
            {
                freeSpawnPoints.Add(spawnPoints2[i]);
            }
        }

        if (freeSpawnPoints.Count == 0)
            return;

        int randomPointIndex = Random.Range(0, freeSpawnPoints.Count);
        Transform chosenPoint = freeSpawnPoints[randomPointIndex];

        int randomObject = Random.Range(0, objectsToSpawn2.Length);

        GameObject spawnedObject = Instantiate(
            objectsToSpawn2[randomObject],
            chosenPoint.position,
            Quaternion.identity
        );

        spawnedObject.transform.parent = chosenPoint;
    }
}