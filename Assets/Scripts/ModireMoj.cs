using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ModireMoj : MonoBehaviour
{
    [FormerlySerializedAs("enemyPrefab")]
    [SerializeField] private GameObject ghayegheEntehariPrefab;

    [FormerlySerializedAs("spawnTime")]
    [SerializeField] private float faseleyeSpawn = 2.5f;

    [SerializeField] private float takhireShoroo = 2f;
    [SerializeField] private int tedadeGhayegheEntehari = 6;
    [SerializeField] private ModireShabake modireShabake;

    private Coroutine mojCoroutine;
    private int tedadeSpawnShode;
    private int tedadeFaal;
    private bool spawnTamamShode;
    private bool spawnMotevaghef;
    private List<int> radifHayeSpawn;

    public int TedadeSpawnShode => tedadeSpawnShode;
    public int TedadeFaal => tedadeFaal;

    private void Start()
    {
        if (modireShabake == null)
            modireShabake = FindObjectOfType<ModireShabake>();

        SakhtaneRadifHayeSpawn();
        mojCoroutine = StartCoroutine(SpawnMoj());
    }

    private void SakhtaneRadifHayeSpawn()
    {
        radifHayeSpawn = new List<int>();

        for (int i = 0; i < tedadeGhayegheEntehari; i++)
            radifHayeSpawn.Add(i % modireShabake.Rows);

        for (int i = radifHayeSpawn.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temporaryRow = radifHayeSpawn[i];
            radifHayeSpawn[i] = radifHayeSpawn[randomIndex];
            radifHayeSpawn[randomIndex] = temporaryRow;
        }
    }

    private IEnumerator SpawnMoj()
    {
        yield return new WaitForSeconds(takhireShoroo);

        while (tedadeSpawnShode < tedadeGhayegheEntehari && !spawnMotevaghef)
        {
            if (ModireBazi.Instance != null && ModireBazi.Instance.BaziTamamShode)
                yield break;

            SpawnGhayegheEntehari();

            if (tedadeSpawnShode < tedadeGhayegheEntehari)
                yield return new WaitForSeconds(faseleyeSpawn);
        }

        spawnTamamShode = tedadeSpawnShode == tedadeGhayegheEntehari;
        BarrasiPiroozi();
    }

    private void SpawnGhayegheEntehari()
    {
        if (ghayegheEntehariPrefab == null || modireShabake == null)
            return;

        int row = radifHayeSpawn[tedadeSpawnShode];
        Vector3 spawnPosition = modireShabake.GetCellPosition(
            row,
            modireShabake.Columns - 1) + Vector3.right * 2f;

        GameObject ghayegh = Instantiate(
            ghayegheEntehariPrefab,
            spawnPosition,
            Quaternion.identity);

        ghayegh.name = "GhayegheEntehari";
        GhayegheEntehari ghayegheEntehari = ghayegh.GetComponent<GhayegheEntehari>();
        if (ghayegheEntehari != null)
            ghayegheEntehari.TanzimeModireMoj(this);

        tedadeSpawnShode++;
        tedadeFaal++;
    }

    public void GhayegheEntehariNaboodShod()
    {
        if (tedadeFaal > 0)
            tedadeFaal--;

        BarrasiPiroozi();
    }

    public void TavaqofSpawn()
    {
        spawnMotevaghef = true;

        if (mojCoroutine != null)
            StopCoroutine(mojCoroutine);
    }

    private void BarrasiPiroozi()
    {
        if (!spawnTamamShode || tedadeFaal != 0)
            return;

        if (ModireBazi.Instance != null && !ModireBazi.Instance.BaziTamamShode)
            ModireBazi.Instance.NamayeshePanelePiroozi();
    }
}
