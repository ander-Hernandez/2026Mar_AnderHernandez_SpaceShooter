using UnityEngine;

public class SprayEnemyBehaviour : EnemyShootingBehaviour
{
    [SerializeField] private Transform[] extraMuzzles;

    public override void Shoot()
    {
        

        GameObject bullet;
        foreach (Transform t in extraMuzzles)
        {
            bullet = GameObject.Instantiate(bulletPrefab, t);
            bullet.transform.SetParent(null);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = t.right;
        }
        
    }
}
