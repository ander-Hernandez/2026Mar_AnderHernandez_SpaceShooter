using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour, IDamageable, IDie, IPointGiver
{
    
    [SerializeField] private Vector2 rotationOffset;
    [SerializeField] private float minimumRotation;
    [SerializeField] private Vector2 movementYVector;
    [SerializeField] private Vector2 movementXVector;
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private int maxHp;
    private int currentHp;
    [SerializeField] private GameObject spawnOnDestroyPrefab;
    [SerializeField] private int numberOfSpawns;
    [SerializeField] private LayerMask damageLayer;
    

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private PointManager manager;
    [SerializeField] private int pointsOnDestroy;

    [SerializeField] private GameObject pointPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeAsteroid(PointManager managerInstance) {
        this.manager = managerInstance;
        Vector2 movementVector = new Vector2(
            Random.Range(movementXVector.x, movementXVector.y),
            Random.Range(movementYVector.x, movementYVector.y)
            );
        float rotation = Random.Range(rotationOffset.x-minimumRotation, rotationOffset.y+minimumRotation);
        _rb2d.linearVelocity = movementVector;
        _rb2d.angularVelocity = rotation;

    }

    public void TakeDamage(int dmg)
    {
        currentHp -= dmg;
        if(currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        AudioManager.PlayAsteroidDestruction();
        if (numberOfSpawns > 0)
        {
            GameObject instance = null;
            for (int i = 0; i < numberOfSpawns; i++)
            {
                instance = Instantiate(spawnOnDestroyPrefab, transform.position, transform.rotation);
                instance.GetComponent<AsteroidBehaviour>().InitializeAsteroid(manager);
            }

        }
        else {
            if (destroyEffect != null) {
                Destroy(Instantiate(destroyEffect, transform.position, transform.rotation), 2);
                Destroy(Instantiate(pointPrefab, transform.position, Quaternion.identity), 1.2f);
            }
        }
        manager.AddPoints(pointsOnDestroy);
        Destroy(gameObject);  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int otherLayerMask = 1 << collision.gameObject.layer;

        if ((damageLayer.value & otherLayerMask) != 0)
        {
            collision.GetComponent<IDamageable>().TakeDamage(1);
            Die();
        }
    }

    public void UpdatePoints()
    {
        manager.AddPoints(pointsOnDestroy);
        
    }
}
