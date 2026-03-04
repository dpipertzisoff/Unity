using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform spawnPointsParent;
    public float spawnInterval = 1.0f;

    public Delivery deliveryScript; // Assign in Inspector

    private Transform[] spawnPoints;
    private float timer = 0f;
    public float count = 0;

    private GameObject currentSpawnedObject; // Tracks the active object

    void Start()
    {
        spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        // Only spawn if:
        // 1. Player does NOT have box
        // 2. There is NO existing spawned object
        if (!deliveryScript.hasBox && currentSpawnedObject == null && count <1)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                currentSpawnedObject = SpawnRandom();
                timer = 0f;
                count += 1;
            }
        }
    }

    GameObject SpawnRandom()
    {
        int randomPoint = Random.Range(1, spawnPoints.Length);
        int randomObject = Random.Range(0, objectsToSpawn.Length);

        return Instantiate(
            objectsToSpawn[randomObject],
            spawnPoints[randomPoint].position,
            Quaternion.identity
        );
    }
}