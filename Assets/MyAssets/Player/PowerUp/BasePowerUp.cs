using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour
{

    [SerializeField] protected Vector2 moveVector;
    [SerializeField] protected string tagName = "PlayerShip";
    void Update()
    {
        transform.Translate(moveVector* Time.deltaTime);
    }

    public abstract void ActivatePowerUp(GameObject player);
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(tagName))
        {
            AudioManager.PlayPowerUpPickup();
            ActivatePowerUp(collision.gameObject);
            Destroy(gameObject);
        }
        
    }
}
