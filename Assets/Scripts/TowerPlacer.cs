using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public int towerCost = 100;
    public GridManager gridManager;
    public GameManager gameManager;

    private bool[,] occupiedCells; // خانه‌های اشغال‌شده

    void Start()
    {
        if (gridManager != null)
        {
            occupiedCells = new bool[gridManager.rows, gridManager.columns];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    void TryPlaceTower()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPos = gridManager.WorldToGrid(mousePos);

        // چک کن که موقعیت معتبر باشه
        if (!IsValidPosition(gridPos))
            return;

        // چک کن که خانه خالی باشه
        if (occupiedCells[gridPos.y, gridPos.x])
        {
            Debug.Log("این خانه قبلاً اشغال شده!");
            return;
        }

        // چک کن که پول کافی داشته باشیم
        if (gameManager.currentMoney < towerCost)
        {
            Debug.Log("پول کافی نیست!");
            return;
        }

        // توپ رو بساز
        Vector2 worldPos = gridManager.GridToWorld(gridPos);
        GameObject tower = Instantiate(towerPrefab, worldPos, Quaternion.identity);

        // خانه رو اشغال‌شده علامت بزن
        occupiedCells[gridPos.y, gridPos.x] = true;

        // پول رو کم کن
        gameManager.SpendMoney(towerCost);
    }

    bool IsValidPosition(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < gridManager.columns &&
               gridPos.y >= 0 && gridPos.y < gridManager.rows;
    }

}
