using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 2f;
    private GridManager gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        InvokeRepeating("SpawnEnemy", 1f, spawnTime);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || gridManager == null) return;

        int randomRow = Random.Range(0, gridManager.rows);

        Vector3 gridSpawnPos = gridManager.GetCellPosition(randomRow, gridManager.columns - 1);

        float offsetX = 2f;
        Vector3 spawnPos = new Vector3(gridSpawnPos.x + offsetX, gridSpawnPos.y, 0);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

}
