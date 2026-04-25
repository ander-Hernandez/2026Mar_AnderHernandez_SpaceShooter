using UnityEngine;

public class HpPowerUp : BasePowerUp
{

    [SerializeField] private float backUpShielTime;

    public override void ActivatePowerUp(GameObject player)
    {
        
        player.GetComponent<SpaceShipHealthController>().TriggerHpPowerUp(backUpShielTime);
    }
}
