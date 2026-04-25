using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;

    [Header("Reset Positions")]
    [SerializeField] private float limitX = -20f;
    [SerializeField] private float resetX = 20f;

    private void Update()
    {
        MoveBackground();
        CheckResetPosition();
    }

    private void MoveBackground()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void CheckResetPosition()
    {
        if (transform.position.x <= limitX)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = resetX;
            transform.position = newPosition;
        }
    }
}