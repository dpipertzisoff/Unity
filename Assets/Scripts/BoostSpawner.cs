using UnityEngine;

public class BoostSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn1;
    public Transform spawnPointsParent1;
    public float spawnInterval1 = 1.0f;
    public float countBoostItem = 0f;

    public Driver driverScript;


    private Transform[] spawnPoints1;
    private float timer = 0f;

    private GameObject currentSpawnedObject;

    void Start()
    {
        spawnPoints1 = spawnPointsParent1.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!driverScript.hasBoost && currentSpawnedObject == null && countBoostItem == 0 )
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval1)
            {
                currentSpawnedObject = SpawnRandom();
                timer = 0f;
                countBoostItem += 1;
                
            }
        }
    }

    GameObject SpawnRandom()
    {
        int randomPoint = Random.Range(1, spawnPoints1.Length);
        int randomObject = Random.Range(0, objectsToSpawn1.Length);

        return Instantiate(
            objectsToSpawn1[randomObject],
            spawnPoints1[randomPoint].position,
            Quaternion.identity
        );
    }
}