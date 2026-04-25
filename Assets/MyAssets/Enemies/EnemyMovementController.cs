using UnityEngine;

public abstract class EnemyMovementController : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 1.0f;

    public virtual void UpdateMovement() {
        
    }
    public virtual void InitializeMovement()
    {

    }
}
