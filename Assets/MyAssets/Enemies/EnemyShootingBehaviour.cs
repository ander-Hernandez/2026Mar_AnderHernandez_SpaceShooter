using UnityEngine;


public class EnemyShootingBehaviour : BaseEnemyBehaviour, IShootable
{

    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected float shootingCooldown;
    protected float shootingCounter;
    [SerializeField] protected Transform muzzleTransform;
   
    private void Update()
    {
        _movementController.UpdateMovement();
        UpdateShooting();
    }


    public virtual void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, muzzleTransform);
        bullet.transform.SetParent(null);
        
    }

    public void UpdateShooting()
    {
        if (shootingCounter >= shootingCooldown)
        {
            Shoot();
            shootingCounter = 0;
        }
        else {
            shootingCounter += Time.deltaTime;
        
        }
    }

    

   
}
