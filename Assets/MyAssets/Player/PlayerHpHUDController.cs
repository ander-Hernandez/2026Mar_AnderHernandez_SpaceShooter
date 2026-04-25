using UnityEngine;

public class PlayerHpHUDController : MonoBehaviour
{
    [SerializeField] private GameObject[] hpGameObjects;
    [SerializeField] private SpaceShipHealthController playerHpController;

    private void Start()
    {
        
    }

    public void UpdateHpHUD()
    {
        
        int hp = playerHpController.GetCurrentHealth();

        for (int i = 0; i < hpGameObjects.Length; i++)
        {
            if(i < hp)
                hpGameObjects[i].SetActive(true);
            if(i >= hp)
                hpGameObjects[i].SetActive(false);
        }
    }

    public void SetPlayer(SpaceShipHealthController playerHUD)
    {
        playerHpController = playerHUD;
    }
}