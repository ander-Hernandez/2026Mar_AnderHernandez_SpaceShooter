using UnityEngine;

public class BaseEnemyBehaviour : MonoBehaviour
{
    
    
    [SerializeField] protected EnemyHealthController _healthController;
    [SerializeField] protected EnemyMovementController _movementController;
    [SerializeField] private LayerMask damageLayer;
    

   
    public void InitializeEnemyLogic(PointManager manager)
    {
        if (_movementController != null)
            _movementController.InitializeMovement();
        if(_healthController != null)
            _healthController.InitializeEnemyHealthController(manager);

    }

    private void Update()
    {
        if (_movementController != null)
        {
            _movementController.UpdateMovement();
        }
    }
    
        
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int otherLayerMask = 1 << collision.gameObject.layer;

        if ((damageLayer.value & otherLayerMask) != 0)
        {
            collision.GetComponent<SpaceShipHealthController>().TakeDamage(1);
        }
    }
}
