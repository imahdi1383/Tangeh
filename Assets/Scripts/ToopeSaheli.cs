using UnityEngine;
using UnityEngine.Serialization;

public class ToopeSaheli : MonoBehaviour
{
    [FormerlySerializedAs("bulletPrefab")]
    [SerializeField] private GameObject goloolePrefab;

    [SerializeField] private Transform firePoint;

    [FormerlySerializedAs("fireRate")]
    [SerializeField] private float faseleyeShelik = 2f;

    [FormerlySerializedAs("range")]
    [SerializeField] private float masafateShelik = 15f;

    private float zamaneShelikeBadi;
    private ModireShabake modireShabake;

    private void Start()
    {
        modireShabake = FindObjectOfType<ModireShabake>();
    }

    private void Update()
    {
        if (ModireBazi.Instance != null && ModireBazi.Instance.BaziTamamShode)
            return;

        if (Time.time < zamaneShelikeBadi)
            return;

        GhayegheEntehari hadaf = PeydaKardaneHadaf();
        if (hadaf == null)
            return;

        Shelik(hadaf);
        zamaneShelikeBadi = Time.time + faseleyeShelik;
    }

    private GhayegheEntehari PeydaKardaneHadaf()
    {
        GhayegheEntehari[] ghayeghHa = FindObjectsOfType<GhayegheEntehari>();
        GhayegheEntehari nazdiktarin = null;
        float faseleyeNazdiktarin = masafateShelik;
        int row = modireShabake != null
            ? modireShabake.GetRowFromWorldY(transform.position.y)
            : -1;

        foreach (GhayegheEntehari ghayegh in ghayeghHa)
        {
            if (ghayegh.transform.position.x <= transform.position.x)
                continue;

            if (modireShabake != null &&
                modireShabake.GetRowFromWorldY(ghayegh.transform.position.y) != row)
                continue;

            float fasele = Vector2.Distance(transform.position, ghayegh.transform.position);
            if (fasele < faseleyeNazdiktarin)
            {
                faseleyeNazdiktarin = fasele;
                nazdiktarin = ghayegh;
            }
        }

        return nazdiktarin;
    }

    private void Shelik(GhayegheEntehari hadaf)
    {
        if (goloolePrefab == null)
            return;

        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        GameObject golooleObject = Instantiate(goloolePrefab, spawnPosition, Quaternion.identity);
        golooleObject.name = "Goloole";

        Goloole goloole = golooleObject.GetComponent<Goloole>();
        if (goloole != null)
            goloole.TanzimeHadaf(hadaf.transform);
        else
            Destroy(golooleObject);
    }
}
