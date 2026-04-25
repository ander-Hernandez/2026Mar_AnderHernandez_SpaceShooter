
using UnityEngine;

[System.Serializable]
public class ObstacleSpawnData
{
    public GameObject prefab;

    [Range(0f, 100f)]
    public float spawnProbability = 1f;
}