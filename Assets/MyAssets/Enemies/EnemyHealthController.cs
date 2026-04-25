using UnityEngine;

public class EnemyHealthController : MonoBehaviour, IDamageable, IDie, IPointGiver
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private GameObject deadEffect;
    [SerializeField] private PointManager pointManager;
    [SerializeField] private int pointsOnDead;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private string damageTag = "Damaged";


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void InitializeEnemyHealthController(PointManager manager)
    {
        currentHealth = maxHealth;
        pointManager = manager;
    }
    public void TakeDamage(int dmg)
    {
        AudioManager.PlayEnemyDamage();
        currentHealth -= dmg;
        if (animator != null)
            animator.SetTrigger(damageTag);
        if (currentHealth <= 0)
            Die();
    }
    public void Die()
    {
        AudioManager.PlayEnemyDeath();
        if (deadEffect != null)
            Destroy(Instantiate(deadEffect, transform.position, transform.rotation), 2);
        UpdatePoints();
        Destroy(gameObject);
    }

    public void UpdatePoints()
    {
        pointManager.AddPoints(pointsOnDead);
        Destroy(Instantiate(pointPrefab, transform.position, transform.rotation),1.2f);
    }
}
