using UnityEngine;
using UnityEngine.Serialization;

public class Goloole : MonoBehaviour
{
    [FormerlySerializedAs("speed")]
    [SerializeField] private float sorateHarekat = 6f;

    [FormerlySerializedAs("damage")]
    [SerializeField] private int meghdareAsib = 1;

    private Transform hadaf;
    private bool barkhordKarde;

    public void TanzimeHadaf(Transform meghdar)
    {
        hadaf = meghdar;
    }

    private void Update()
    {
        if (ModireBazi.Instance != null && ModireBazi.Instance.BaziTamamShode)
            return;

        if (hadaf == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            hadaf.position,
            sorateHarekat * Time.deltaTime);

        if (Vector2.Distance(transform.position, hadaf.position) <= 0.05f)
            Barkhord();
    }

    private void Barkhord()
    {
        if (barkhordKarde)
            return;

        barkhordKarde = true;
        GhayegheEntehari ghayegheEntehari = hadaf != null
            ? hadaf.GetComponent<GhayegheEntehari>()
            : null;

        if (ghayegheEntehari != null)
            ghayegheEntehari.DaryafteAsib(meghdareAsib);

        Destroy(gameObject);
    }
}
