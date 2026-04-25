using System.Diagnostics;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private PowerUpSpawner powerUpSpawner;

    [Header("Wave Settings")]
    [SerializeField] private float difficultyIncreaseTime = 10f;

    [SerializeField] private float enemyCooldown = 2f;
    [SerializeField] private float obstacleCooldown = 3f;
    [SerializeField] private float powerUpCooldown = 3f;

    [SerializeField] private float enemyCooldownDecrease = 0.2f;
    [SerializeField] private float obstacleCooldownDecrease = 0.15f;
    [SerializeField] private float powerUpCooldownDecrease = 0.15f;

    [SerializeField] private float minEnemyCooldown = 0.5f;
    [SerializeField] private float minObstacleCooldown = 1f;
    [SerializeField] private float minPowerUpCooldown = 0.5f;

    [Header("Display")]
    [SerializeField] private TextMeshProUGUI displayText;
    [Header("Debug")]
    [SerializeField] private float difficultyCounter = 0f;
    [SerializeField] private float currentDifficulty = 0f;


    private void Start()
    {
        AudioManager.StopMusic();
        AudioManager.PlayBackgroundMusic();
        enemySpawner.InitializeSpawnerWithCustomSettings(enemyCooldown, true);
        obstacleSpawner.InitializeSpawnerWithCustomSettings(obstacleCooldown, true);
        powerUpSpawner.InitializeSpawnerWithCustomSettings(powerUpCooldown, true);
        currentDifficulty = 0;
        UpdateDisplayText();
        
    }

    private void Update()
    {
        difficultyCounter += Time.deltaTime;

        if (difficultyCounter >= difficultyIncreaseTime)
        {
            IncreaseDifficulty();
            difficultyCounter = 0f;
        }
    }

    private void IncreaseDifficulty()
    {
        currentDifficulty += 1;
        UpdateDisplayText();
        enemyCooldown = Mathf.Max(minEnemyCooldown, enemyCooldown - enemyCooldownDecrease);
        obstacleCooldown = Mathf.Max(minObstacleCooldown, obstacleCooldown - obstacleCooldownDecrease);
        powerUpCooldown = Mathf.Max(minPowerUpCooldown, powerUpCooldown - powerUpCooldownDecrease);
        enemySpawner.InitializeSpawnerWithCustomSettings(enemyCooldown, true);
        obstacleSpawner.InitializeSpawnerWithCustomSettings(obstacleCooldown, true);
        powerUpSpawner.InitializeSpawnerWithCustomSettings(powerUpCooldown, true);

    }
    public void UpdateDisplayText()
    {
        displayText.text = "Lvl: " + currentDifficulty.ToString();

    }
}