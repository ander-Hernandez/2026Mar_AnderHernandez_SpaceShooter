using UnityEngine;

[System.Serializable]
public class EnemySpawnData 
{
    public GameObject prefab;

    [Range(0f, 100f)]
    public float spawnProbability = 1f;

    public float minDifficulty = 0f;
}
