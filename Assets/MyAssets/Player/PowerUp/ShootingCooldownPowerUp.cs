using System.Collections;
using UnityEngine;

public class ShootingCooldownPowerUp : BasePowerUp
{
    [SerializeField] protected float powerUpCooldown;
    [SerializeField] protected float powerUpTime;
    private float originalCooldown;

    public override void ActivatePowerUp(GameObject player)
    {
        player.GetComponent<SpaceShipShootingController>().DecreaseCooldown(powerUpTime, powerUpCooldown);
    }

}
