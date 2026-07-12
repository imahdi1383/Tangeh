using UnityEngine;
using UnityEngine.Serialization;

public class ModireSakhtSaazeh : MonoBehaviour
{
    [FormerlySerializedAs("towerPrefab")]
    [SerializeField] private GameObject toopeSaheliPrefab;

    [FormerlySerializedAs("towerCost")]
    [SerializeField] private int hazineyeSakht = 50;

    [FormerlySerializedAs("gridManager")]
    [SerializeField] private ModireShabake modireShabake;

    [FormerlySerializedAs("gameManager")]
    [SerializeField] private ModireBazi modireBazi;

    private bool[,] occupiedCells;

    public int HazineyeSakht => hazineyeSakht;

    private void Start()
    {
        if (modireShabake == null)
            modireShabake = FindObjectOfType<ModireShabake>();

        if (modireBazi == null)
            modireBazi = FindObjectOfType<ModireBazi>();

        if (modireShabake != null)
            occupiedCells = new bool[modireShabake.Rows, modireShabake.Columns];
    }

    private void Update()
    {
        if (ModireBazi.Instance != null && ModireBazi.Instance.BaziTamamShode)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPosition = modireShabake.WorldToGrid(worldPosition);
            SakhtDarKhoone(gridPosition.y, gridPosition.x);
        }
    }

    public bool SakhtDarKhoone(int row, int column)
    {
        if (modireShabake == null || modireBazi == null || toopeSaheliPrefab == null)
            return false;

        if (row < 0 || row >= modireShabake.Rows ||
            column < 0 || column >= modireShabake.Columns)
            return false;

        if (occupiedCells == null || occupiedCells[row, column])
            return false;

        if (!modireBazi.SpendRial(hazineyeSakht))
            return false;

        GameObject toopeSaheli = Instantiate(
            toopeSaheliPrefab,
            modireShabake.GetCellPosition(row, column),
            Quaternion.identity);

        toopeSaheli.name = "ToopeSaheli";
        occupiedCells[row, column] = true;
        return true;
    }
}
