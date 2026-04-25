using UnityEngine;
[System.Serializable]
public class PowerUpSpawnData 
{
    public GameObject prefab;

    [Range(0f, 100f)]
    public float spawnProbability = 1f;
}
