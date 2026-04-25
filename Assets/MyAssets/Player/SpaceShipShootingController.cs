using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipShootingController : MonoBehaviour, IShootable
{
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform muzzleTransform;

    [SerializeField] public float shootingCooldown;

    private float cooldownCounter = 0f;
    private bool canShoot;
    private float originalCooldown;

    private Coroutine decreaseCooldownCoroutine;
    

    private void Start()
    {
        canShoot = true;
        cooldownCounter = 0f;
        originalCooldown = shootingCooldown;
        
    }

    private void Update()
    {
        UpdateShooting();
    }

    private void OnEnable()
    {
        shoot.action.Enable();
    }

    private void OnDisable()
    {
        shoot.action.Disable();
    }

    public void UpdateShooting()
    {
        if (canShoot && shoot.action.IsPressed())
        {
            Shoot();
            cooldownCounter = 0f;
            canShoot = false;
            return;
        }

        if (!canShoot)
        {
            cooldownCounter += Time.deltaTime;

            if (cooldownCounter >= shootingCooldown)
            {
                cooldownCounter = shootingCooldown;
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(shotPrefab, muzzleTransform);
        bullet.transform.SetParent(null);
        AudioManager.PlayPlayerShoot();
    }

    public void DecreaseCooldown(float timeToDisable, float powerUpCooldown)
    {
        if (decreaseCooldownCoroutine != null)
        {
            StopCoroutine(decreaseCooldownCoroutine);
        }

        decreaseCooldownCoroutine = StartCoroutine(
            DecreaseCooldownCoroutine(timeToDisable, powerUpCooldown)
        );
    }

    private IEnumerator DecreaseCooldownCoroutine(float timeToDisable, float powerUpCooldown)
    {
        shootingCooldown = powerUpCooldown;

        yield return new WaitForSeconds(timeToDisable);

        shootingCooldown = originalCooldown;
        decreaseCooldownCoroutine = null;
    }
}