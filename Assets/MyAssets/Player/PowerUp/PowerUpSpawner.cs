using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private PowerUpSpawnData[] powerUpPrefabs;
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
            SpawnPowerUpInRandomTransform();
            spawnCounter = 0f;
        }
    }

    private void SpawnPowerUp(GameObject prefab, Transform spawnTransform)
    {
        GameObject instance = Instantiate(
            prefab,
            spawnTransform.position,
            spawnTransform.rotation
        );

    }

    private void SpawnPowerUpInRandomTransform()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            return;
        }

        GameObject selectedPrefab = GetRandomPowerUpPrefab();

        if (selectedPrefab == null)
            return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        SpawnPowerUp(selectedPrefab, spawnPoints[spawnIndex]);
    }

    private GameObject GetRandomPowerUpPrefab()
    {
        if (powerUpPrefabs == null || powerUpPrefabs.Length == 0)
        {
            return null;
        }

        float totalWeight = 0f;

        foreach (PowerUpSpawnData obstacleData in powerUpPrefabs)
        {
            if (obstacleData.prefab == null)
                continue;

            totalWeight += obstacleData.spawnProbability;
        }

        if (totalWeight <= 0f)
            return null;

        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (PowerUpSpawnData obstacleData in powerUpPrefabs)
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
