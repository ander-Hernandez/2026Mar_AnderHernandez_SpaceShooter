using System.Collections;
using UnityEngine;

public class SpaceShipShieldController : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] public bool isShielded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isShielded = false;
    }

    public void EnableShield(float timeToDisable)
    {
        StartCoroutine(EnableShieldCoroutine(timeToDisable));

    }

    private IEnumerator EnableShieldCoroutine(float timeToDisable)
    {
        shield.SetActive(true);
        isShielded=true;

        yield return new WaitForSeconds(timeToDisable);
        if (isShielded)
        {
            shield.SetActive(false);
            isShielded = false;
        }
    }


    public void DisableShield()
    {
        shield.SetActive(false);
        isShielded = false;
    }
}
