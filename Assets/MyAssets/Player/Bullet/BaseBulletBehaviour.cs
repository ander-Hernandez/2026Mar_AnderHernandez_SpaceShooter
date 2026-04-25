using UnityEngine;

public class BaseBulletBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private int bulletDamage;
    [SerializeField] private int hittableEnemyCount = 1;
    [SerializeField] private Vector2 velocityVector;
    private int counter = 0;

    private void Start()
    {
        counter = 0;
        this.GetComponent<Rigidbody2D>().linearVelocity = velocityVector;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        int otherLayerMask = 1 << collision.gameObject.layer;

        if ((damageLayer.value & otherLayerMask) != 0)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(bulletDamage);
            counter++;
            if (counter >= hittableEnemyCount)
            {
                Destroy(this.gameObject);
                Destroy(Instantiate(destroyEffect, transform.position, transform.rotation), 0.5f);

            }
                
        }
    }

    
}
