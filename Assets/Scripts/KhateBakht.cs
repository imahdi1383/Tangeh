using UnityEngine;

public class KhateBakht : MonoBehaviour
{
    [SerializeField] private ModireShabake modireShabake;
    [SerializeField, Min(0f)] private float faseleAzShabake = 0.2f;
    [SerializeField, Min(0.01f)] private float arzKhate = 0.1f;

    private BoxCollider2D boxCollider;
    private bool sabtShode;

    private void Awake()
    {
        if (modireShabake == null)
            modireShabake = FindObjectOfType<ModireShabake>();

        boxCollider = GetComponent<BoxCollider2D>();
        TanzimeKhate();
    }

    private void TanzimeKhate()
    {
        if (modireShabake == null || boxCollider == null)
            return;

        transform.position = new Vector3(
            modireShabake.GridLeftX - faseleAzShabake,
            modireShabake.GridCenterY,
            transform.position.z);

        boxCollider.isTrigger = true;
        boxCollider.offset = Vector2.zero;
        boxCollider.size = new Vector2(arzKhate, modireShabake.GridHeight);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GhayegheEntehari doshman = other.GetComponentInParent<GhayegheEntehari>();
        ModireBazi modireBazi = ModireBazi.Instance;

        if (sabtShode || doshman == null || modireBazi == null || modireBazi.BaziTamamShode)
            return;

        float khateBakhtX = transform.position.x;
        float markazeDoshmanX = doshman.transform.position.x;

        if (markazeDoshmanX > khateBakhtX)
            return;

        sabtShode = true;
        modireBazi.NamayeshePaneleShekast();
    }

    private void OnDrawGizmos()
    {
        if (modireShabake == null)
            return;

        Vector3 markaz = new Vector3(
            modireShabake.GridLeftX - faseleAzShabake,
            modireShabake.GridCenterY,
            transform.position.z);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(markaz, new Vector3(arzKhate, modireShabake.GridHeight, 0f));
    }
}
