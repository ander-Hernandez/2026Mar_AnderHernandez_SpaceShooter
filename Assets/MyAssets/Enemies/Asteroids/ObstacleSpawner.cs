using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private ObstacleSpawnData[] obstaclePrefabs;
    [SerializeField] private float spawnCooldown = 2f;
    [SerializeField] private bool shouldSpawn = false;

    [Header("References")]
    [SerializeField] private PointManager manager;

    private float spawnCounter = 0f;

    private void Awake()
    {
        shouldSpawn = false;
        spawnCounter = 0f;
    }

    private void Update()
    {
        if (!shouldSpawn)
            return;

        spawnCounter += Time.deltaTime;

        if (spawnCounter >= spawnCooldown)
        {
            SpawnObstacleInRandomTransform();
            spawnCounter = 0f;
        }
    }

    private void SpawnObstacle(GameObject prefab, Transform spawnTransform)
    {
        GameObject instance = Instantiate(
            prefab,
            spawnTransform.position,
            spawnTransform.rotation
        );

        AsteroidBehaviour asteroid = instance.GetComponent<AsteroidBehaviour>();

        if (asteroid != null)
        {
            asteroid.InitializeAsteroid(manager);
        }
        else
        {
            Debug.LogWarning($"{prefab.name} does not have AsteroidBehaviour.");
        }
    }

    private void SpawnObstacleInRandomTransform()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned in ObstacleSpawner.");
            return;
        }

        GameObject selectedPrefab = GetRandomObstaclePrefab();

        if (selectedPrefab == null)
            return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        SpawnObstacle(selectedPrefab, spawnPoints[spawnIndex]);
    }

    private GameObject GetRandomObstaclePrefab()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("No obstacle prefabs assigned in ObstacleSpawner.");
            return null;
        }

        float totalWeight = 0f;

        foreach (ObstacleSpawnData obstacleData in obstaclePrefabs)
        {
            if (obstacleData.prefab == null)
                continue;

            totalWeight += obstacleData.spawnProbability;
        }

        if (totalWeight <= 0f)
            return null;

        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (ObstacleSpawnData obstacleData in obstaclePrefabs)
        {
            if (obstacleData.prefab == null)
                continue;

            currentWeight += obstacleData.spawnProbability;

            if (randomValue <= currentWeight)
            {
                return obstacleData.prefab;
            }
        }

        return null;
    }

    public void InitializeSpawnerWithCustomSettings(float cooldown, bool spawnActive)
    {
        spawnCooldown = cooldown;
        shouldSpawn = spawnActive;
        spawnCounter = 0f;
    }

    public void SetSpawnActive(bool active)
    {
        shouldSpawn = active;
    }
}