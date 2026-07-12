using UnityEngine;
using UnityEngine.Serialization;

public class GhayegheEntehari : MonoBehaviour
{
    [FormerlySerializedAs("speed")]
    [SerializeField] private float sorateHarekat = 0.4f;

    [FormerlySerializedAs("health")]
    [SerializeField] private int jooneHadaksar = 5;

    private int jooneFeli;
    private ModireMoj modireMoj;
    private bool naboodShode;

    private void Start()
    {
        jooneFeli = jooneHadaksar;
    }

    private void Update()
    {
        if (ModireBazi.Instance != null && ModireBazi.Instance.BaziTamamShode)
            return;

        transform.Translate(Vector2.left * sorateHarekat * Time.deltaTime);
    }

    public void TanzimeModireMoj(ModireMoj meghdar)
    {
        modireMoj = meghdar;
    }

    public void DaryafteAsib(int meghdareAsib)
    {
        if (naboodShode)
            return;

        jooneFeli -= meghdareAsib;
        if (jooneFeli > 0)
            return;

        naboodShode = true;

        if (modireMoj != null)
            modireMoj.GhayegheEntehariNaboodShod();

        Destroy(gameObject);
    }
}
