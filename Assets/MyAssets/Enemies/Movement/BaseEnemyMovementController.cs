using UnityEngine;

public class BaseEnemyMovementController : EnemyMovementController
{

    [SerializeField] private Vector2 movementVelocityVector;



    public override void UpdateMovement()
    {
        transform.Translate(movementVelocityVector * movementSpeed * Time.deltaTime);
    }
}
