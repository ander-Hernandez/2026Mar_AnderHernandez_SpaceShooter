using UnityEngine;

public class WaveEnemyMovementController : EnemyMovementController
{
    
    [SerializeField] float minYOffset;
    [SerializeField] float maxYOffset;
    [SerializeField] float directionX;
    [SerializeField] float ySpeed;
    private float initialYPos;
    bool isGoingUp = true;



    private void Start()
    {
        initialYPos = transform.position.y;
    }
    public override void UpdateMovement()
    {


        float directionY = isGoingUp ? 1 : -1;
        Vector2 direction = new Vector2(directionX*movementSpeed, directionY*ySpeed);
        
        transform.Translate(direction * Time.deltaTime);


        if (isGoingUp && (transform.position.y > initialYPos+ maxYOffset))
        {
            isGoingUp = false;
        }
        else if(!isGoingUp && (transform.position.y < initialYPos - minYOffset))
        {
            isGoingUp = true;
        }

        
    }
}
