using System.Collections;
using UnityEngine;

public class SpaceShipHealthController : MonoBehaviour, IDamageable, IDie
{
    [SerializeField] private int hpPoints = 3;
    [SerializeField] private GameObject deadEffect;
    [SerializeField] private PlayerSpawner spawner;
    [SerializeField] private BoxCollider2D hitCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float invulnerabilityTime = 4f;
    [SerializeField] private float blinkInterval = 0.12f;

    [SerializeField] private SpaceShipShieldController shieldManager;
    [SerializeField] public PlayerHpHUDController hpHUDController;
 

    private int currentHealth;
    private bool isInvulnerable;
    private bool isDead;

    private Coroutine blinkCoroutine;
    private Coroutine invulnerabilityCoroutine;

    private void Awake()
    {
        shieldManager = GetComponent<SpaceShipShieldController>();
        currentHealth = hpPoints;
    }

    public int GetCurrentHealth() { 
        return currentHealth;
    }
    public void TakeDamage(int dmg)
    {
        if (isDead || isInvulnerable)
            return;
        if(shieldManager.isShielded)
        {
            shieldManager.DisableShield();
            return;
        }
        currentHealth -= dmg;
        hpHUDController.UpdateHpHUD();

        
        Die();
        if(currentHealth <= 0)
            EndGame();
        
        
    }

    public void SetSpawner(PlayerSpawner spawnerInstance)
    {
        spawner = spawnerInstance;
    }

    public void Die()
    {
        AudioManager.PlayPlayerDeath();
        if (deadEffect != null)
            Destroy(Instantiate(deadEffect, transform.position, transform.rotation), 2f);

        if (spawner != null)
        {
            spawner.RespawnPlayer();
        }

        
        StartTemporaryInvulnerability();
        isDead = false;
    }

    public void EndGame()
    {
        GameManager.EnableRestartMenu();
        gameObject.SetActive(false);
    }

    public void StartTemporaryInvulnerability()
    {
        if (invulnerabilityCoroutine != null)
            StopCoroutine(invulnerabilityCoroutine);

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        invulnerabilityCoroutine = StartCoroutine(InvulnerabilityRoutine(invulnerabilityTime));
    }

    private IEnumerator InvulnerabilityRoutine(float seconds)
    {
        isInvulnerable = true;

        if (hitCollider != null)
            hitCollider.enabled = false;

        if (spriteRenderer != null)
            blinkCoroutine = StartCoroutine(BlinkRoutine());

        yield return new WaitForSeconds(seconds);

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (hitCollider != null)
            hitCollider.enabled = true;

        isInvulnerable = false;
        invulnerabilityCoroutine = null;
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void TriggerHpPowerUp(float backUpShieldTime)
    {
        if(currentHealth < hpPoints)
        {
            currentHealth++;
            hpHUDController.UpdateHpHUD();
        }
        else
        {
            shieldManager.EnableShield(backUpShieldTime);
        }
    }
}