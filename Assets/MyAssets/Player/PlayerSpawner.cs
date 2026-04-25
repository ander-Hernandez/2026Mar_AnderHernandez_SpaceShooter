using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;
    [SerializeField] private Transform playerSpawnTransform;
    [SerializeField] private PlayerHpHUDController playerHpHUDController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        RespawnPlayer();
        playerHpHUDController.SetPlayer(playerInstance.GetComponent<SpaceShipHealthController>());
        playerHpHUDController.UpdateHpHUD();
    }
    public void RespawnPlayer() {

        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);
            playerInstance.GetComponent<SpaceShipHealthController>().SetSpawner(this);
            playerInstance.GetComponent<SpaceShipHealthController>().hpHUDController = playerHpHUDController;
        }
        else { 
            playerInstance.transform.position = playerSpawnTransform.position;
        }
        
    }
    
}
