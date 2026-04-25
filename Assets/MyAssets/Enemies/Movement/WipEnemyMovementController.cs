using UnityEngine;

public class WipEnemyMovementController : EnemyMovementController
{
    
    
    [SerializeField] float leftLimitCoordinate = -3f;
    [SerializeField] float rightScreenLimit = 3f;
    bool isGoingLeft = true;
    [SerializeField] SpriteRenderer spriteRenderer;

    public override void UpdateMovement()
    {
        Vector2 direction = isGoingLeft ? Vector2.left : Vector2.right;
        transform.Translate(direction * movementSpeed * Time.deltaTime);
        if(isGoingLeft)
            spriteRenderer.flipX = false;
        if(!isGoingLeft)
            spriteRenderer.flipX = true;

        if (isGoingLeft && (transform.position.x < leftLimitCoordinate))
        {
            isGoingLeft = false;
        }

        if (!isGoingLeft && (transform.position.x > rightScreenLimit))
        {
            Destroy(gameObject);
        }
    }

    
    
}
