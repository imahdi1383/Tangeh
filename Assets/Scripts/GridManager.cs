using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 5;           // تعداد مسیرها (lanes)
    public int columns = 9;        // تعداد خانه‌ها در هر مسیر
    public float cellWidth = 1f;   // عرض هر خانه
    public float cellHeight = 1f;  // ارتفاع هر خانه

    [Header("Visual (Optional)")]
    public GameObject cellPrefab;  // اختیاری: برای نمایش خطوط شبکه
    public bool showGrid = true;

    private Vector3[,] gridPositions; // ذخیره موقعیت هر خانه

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridPositions = new Vector3[rows, columns];

        // محاسبه موقعیت مرکزی شبکه
        float startX = -(columns - 1) * cellWidth / 2f;
        float startY = -(rows - 1) * cellHeight / 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x = startX + col * cellWidth;
                float y = startY + row * cellHeight;

                gridPositions[row, col] = new Vector3(x, y, 0);

                // اختیاری: نمایش بصری شبکه
                if (showGrid && cellPrefab != null)
                {
                    GameObject cell = Instantiate(cellPrefab, gridPositions[row, col], Quaternion.identity);
                    cell.transform.parent = transform;
                    cell.name = $"Cell_{row}_{col}";
                }
            }
        }
    }

    // تبدیل موقعیت ماوس به خانه شبکه
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        float startX = -(columns - 1) * cellWidth / 2f;
        float startY = -(rows - 1) * cellHeight / 2f;

        int col = Mathf.RoundToInt((worldPosition.x - startX) / cellWidth);
        int row = Mathf.RoundToInt((worldPosition.y - startY) / cellHeight);

        // بررسی محدوده
        if (row >= 0 && row < rows && col >= 0 && col < columns)
            return new Vector2Int(col, row);

        return new Vector2Int(-1, -1); // خارج از شبکه
    }

    // گرفتن موقعیت دنیای واقعی از خانه شبکه
    public Vector3 GetWorldPosition(int row, int col)
    {
        if (row >= 0 && row < rows && col >= 0 && col < columns)
            return gridPositions[row, col];

        return Vector3.zero;
    }

    // رسم خطوط شبکه در Editor (اختیاری)
    void OnDrawGizmos()
    {
        if (!showGrid) return;

        Gizmos.color = Color.green;

        float startX = -columns * cellWidth / 2f;
        float startY = -rows * cellHeight / 2f;
        float endX = startX + columns * cellWidth;
        float endY = startY + rows * cellHeight;

        // خطوط افقی
        for (int i = 0; i <= rows; i++)
        {
            float y = startY + i * cellHeight;
            Gizmos.DrawLine(new Vector3(startX, y, 0), new Vector3(endX, y, 0));
        }

        // خطوط عمودی
        for (int i = 0; i <= columns; i++)
        {
            float x = startX + i * cellWidth;
            Gizmos.DrawLine(new Vector3(x, startY, 0), new Vector3(x, endY, 0));
        }
    }
    public Vector3 GetCellPosition(int row, int col)
    {
        return GetWorldPosition(row, col);
    }
    public Vector2Int WorldToGrid(Vector2 worldPos)
    {
        float startX = transform.position.x - (columns * cellWidth) / 2f;
        float startY = transform.position.y - (rows * cellHeight) / 2f;

        int x = Mathf.FloorToInt((worldPos.x - startX) / cellWidth);
        int y = Mathf.FloorToInt((worldPos.y - startY) / cellHeight);
        return new Vector2Int(x, y);
    }

    public Vector2 GridToWorld(Vector2Int gridPos)
    {
        float startX = transform.position.x - (columns * cellWidth) / 2f;
        float startY = transform.position.y - (rows * cellHeight) / 2f;

        float x = startX + (gridPos.x * cellWidth) + (cellWidth / 2f);
        float y = startY + (gridPos.y * cellHeight) + (cellHeight / 2f);
        return new Vector2(x, y);
    }



}
