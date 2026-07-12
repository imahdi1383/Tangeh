using UnityEngine;

public class KhateBakht : MonoBehaviour
{
    private bool sabtShode;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (sabtShode || other.GetComponentInParent<GhayegheEntehari>() == null)
            return;

        sabtShode = true;

        if (ModireBazi.Instance != null)
            ModireBazi.Instance.NamayeshePaneleShekast();
    }
}
