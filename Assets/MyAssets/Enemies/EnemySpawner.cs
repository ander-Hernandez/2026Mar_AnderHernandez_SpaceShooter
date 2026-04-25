using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemySpawnData[] enemyPrefabs;
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
            SpawnEnemyInRandomTransform();
            spawnCounter = 0f;
        }
    }

    private void SpawnEnemy(GameObject prefab, Transform spawnTransform)
    {
        GameObject instance = Instantiate(
            prefab,
            spawnTransform.position,
            spawnTransform.rotation
        );

        BaseEnemyBehaviour enemy = instance.GetComponent<BaseEnemyBehaviour>();

        if (enemy != null)
        {
            enemy.InitializeEnemyLogic(manager);
        }
        else
        {
            Debug.LogWarning($"{prefab.name} does not have BaseEnemyBehaviour.");
        }
    }

    private void SpawnEnemyInRandomTransform()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned in EnemySpawner.");
            return;
        }

        GameObject selectedPrefab = GetRandomEnemyPrefab();

        if (selectedPrefab == null)
            return;

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        SpawnEnemy(selectedPrefab, spawnPoints[spawnIndex]);
    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned in EnemySpawner.");
            return null;
        }

        float totalWeight = 0f;

        foreach (EnemySpawnData enemyData in enemyPrefabs)
        {
            if (enemyData.prefab == null)
                continue;

            totalWeight += enemyData.spawnProbability;
        }

        if (totalWeight <= 0f)
            return null;

        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (EnemySpawnData enemyData in enemyPrefabs)
        {
            if (enemyData.prefab == null)
                continue;

            currentWeight += enemyData.spawnProbability;

            if (randomValue <= currentWeight)
            {
                return enemyData.prefab;
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