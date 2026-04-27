using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipController : MonoBehaviour
{

    [SerializeField] private InputActionReference move;

    private Rigidbody2D _rb2d;
    [SerializeField] private float linearSpeed;
    [SerializeField] private Animator animator;
    private Vector2 moveVector;
    



    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        move.action.started += OnMove;
        move.action.canceled += OnMove;
        move.action.performed += OnMove;
        animator = GetComponentInChildren<Animator>();
        moveVector = Vector2.zero;

    }

    private void OnEnable()
    {
        move.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        moveVector = move.action.ReadValue<Vector2>();
        if(_rb2d != null)
        _rb2d.linearVelocity = moveVector * linearSpeed;
        animator.SetFloat("MoveX", moveVector.x);
        animator.SetFloat("MoveY", moveVector.y);
        
    }
}