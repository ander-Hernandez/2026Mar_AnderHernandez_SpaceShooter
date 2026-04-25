using UnityEngine;

public class ShieldPowerUp : BasePowerUp
{
    [SerializeField] private float powerUpTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public override void ActivatePowerUp(GameObject player)
    {
        player.GetComponent<SpaceShipShieldController>().EnableShield(powerUpTime);
    }
}
