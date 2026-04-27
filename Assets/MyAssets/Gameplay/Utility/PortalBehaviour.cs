
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    [SerializeField] private PortalBehaviour otherPortal;
    [SerializeField] private LayerMask teleportLayer;
    [SerializeField] private float teleportCooldown = 0.2f;

    [SerializeField] private Vector2 teleportMovingXLimits;
    [SerializeField] private Vector2 teleportMovingYLimits;
    [SerializeField] private float minDistanceBetweenPortals = 4;
    [SerializeField] private float timeToSwitchPlaces = 4f;
    private float positionCounter = 0f;

    private bool canTeleport = true;

    private Animator _animator;


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        positionCounter = 0;
    }
    public void TeleportPlayerToOtherPortal(Transform player)
    {
        if (!canTeleport || otherPortal == null)
            return;


        Vector2 offset = player.position - transform.position;

        player.position = new Vector2(
            otherPortal.transform.position.x,
            otherPortal.transform.position.y - offset.y
        );

        canTeleport = false;
        otherPortal.canTeleport = false;

        Invoke(nameof(ResetTeleport), teleportCooldown);
        otherPortal.Invoke(nameof(ResetTeleport), teleportCooldown);
    } 
    private void MoveTeleport()
    {
        Vector2 position = new Vector2(
                Random.Range(teleportMovingXLimits.x, teleportMovingXLimits.y),
                Random.Range(teleportMovingYLimits.x, teleportMovingYLimits.y)
            );

        if (Vector2.Distance(position, otherPortal.transform.position) < minDistanceBetweenPortals)
        {
            position = new Vector2(
                Random.Range(teleportMovingXLimits.x, teleportMovingXLimits.y),
                Random.Range(teleportMovingYLimits.x, teleportMovingYLimits.y)
            );
        }
            
        _animator.SetTrigger("RestartPortal");
        transform.position = position;

    }
    private void Update()
    {
        if(positionCounter > timeToSwitchPlaces)
        {
            positionCounter = 0;
            MoveTeleport();

        }
        positionCounter += Time.deltaTime;
    }


    private void ResetTeleport()
    {
        canTeleport = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int otherLayerMask = 1 << collision.gameObject.layer;

        if ((teleportLayer.value & otherLayerMask) != 0)
        {
            TeleportPlayerToOtherPortal(collision.transform);
        }
    }

    public void SetMovingCooldown(float newCooldown) { 
        timeToSwitchPlaces = newCooldown;
    }
   
}