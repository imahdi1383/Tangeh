using UnityEngine;
using UnityEngine.Serialization;

public class ModireShabake : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 12;
    [SerializeField] private float cellWidth = 1.1f;
    [SerializeField] private float cellHeight = 1.1f;

    [Header("Visual")]
    [FormerlySerializedAs("cellPrefab")]
    [SerializeField] private GameObject khooneyeShabakePrefab;
    [SerializeField] private bool showGrid = true;

    public int Rows => rows;
    public int Columns => columns;
    public float CellHeight => cellHeight;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (!showGrid || khooneyeShabakePrefab == null)
            return;

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject khoone = Instantiate(
                    khooneyeShabakePrefab,
                    GetCellPosition(row, column),
                    Quaternion.identity,
                    transform);

                khoone.name = $"KhooneyeShabake_{row}_{column}";
            }
        }
    }

    private Vector3 GetBottomLeft()
    {
        return transform.position - new Vector3(
            columns * cellWidth * 0.5f,
            rows * cellHeight * 0.5f,
            0f);
    }

    public Vector3 GetCellPosition(int row, int column)
    {
        Vector3 bottomLeft = GetBottomLeft();
        return bottomLeft + new Vector3(
            (column + 0.5f) * cellWidth,
            (row + 0.5f) * cellHeight,
            0f);
    }

    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        Vector3 bottomLeft = GetBottomLeft();
        int column = Mathf.FloorToInt((worldPosition.x - bottomLeft.x) / cellWidth);
        int row = Mathf.FloorToInt((worldPosition.y - bottomLeft.y) / cellHeight);

        if (row < 0 || row >= rows || column < 0 || column >= columns)
            return new Vector2Int(-1, -1);

        return new Vector2Int(column, row);
    }

    public int GetRowFromWorldY(float worldY)
    {
        int row = Mathf.FloorToInt((worldY - GetBottomLeft().y) / cellHeight);
        return row >= 0 && row < rows ? row : -1;
    }

    private void OnDrawGizmos()
    {
        if (!showGrid)
            return;

        Gizmos.color = Color.green;
        Vector3 bottomLeft = GetBottomLeft();

        for (int row = 0; row <= rows; row++)
        {
            float y = bottomLeft.y + row * cellHeight;
            Gizmos.DrawLine(
                new Vector3(bottomLeft.x, y, 0f),
                new Vector3(bottomLeft.x + columns * cellWidth, y, 0f));
        }

        for (int column = 0; column <= columns; column++)
        {
            float x = bottomLeft.x + column * cellWidth;
            Gizmos.DrawLine(
                new Vector3(x, bottomLeft.y, 0f),
                new Vector3(x, bottomLeft.y + rows * cellHeight, 0f));
        }
    }
}
