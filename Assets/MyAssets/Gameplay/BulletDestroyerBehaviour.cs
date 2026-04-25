using UnityEngine;

public class BulletDestroyerBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask destroyLayers;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        int otherLayerMask = 1 << collision.gameObject.layer;

        if ((destroyLayers.value & otherLayerMask) != 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
