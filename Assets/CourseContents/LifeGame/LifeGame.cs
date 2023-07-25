using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour
{
    [SerializeField]
    private float _transitionInterval = 1f;
    [Range(10, 100)]
    [SerializeField]
    private int _row = 0;
    [Range(10, 100)]
    [SerializeField]
    private int _column = 0;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = default;
    [SerializeField]
    private LifeCell _cellPrefab = default;

    private float _timer = 0f;
    private LifeCell[,] _cells = default;

    private void Start()
    {
        _gridLayoutGroup.constraint
            = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _column;

        _cells = new LifeCell[_row, _column];
        var parent = _gridLayoutGroup.transform;

        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                cell.name = $"Cell ({r}, {c})";

                var random = Random.Range(0, 5);
                cell.CurrentCellState = random == 0 ? LifeCellState.Alive : LifeCellState.Dead;
                _cells[r, c] = cell;
                cell.SettingIndex(r, c);
            }
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _transitionInterval)
        {
            MoveToNextGeneration();
            _timer = 0f;
        }
    }

    private void MoveToNextGeneration()
    {
        foreach (var cell in _cells)
        {
            cell.NextCellState = TryCheckAlive(cell);
        }
    }

    /// <summary> 指定したセルが次の世代で生きているか判定する </summary>
    /// <returns> 生きていたらtrue </returns>
    private LifeCellState TryCheckAlive(LifeCell lifeCell)
    {
        int count = TryGetCellCount(lifeCell);
        //生存
        if (lifeCell.CurrentCellState == LifeCellState.Alive && (count == 2 || count == 3))
        {
            return LifeCellState.Alive;
        }
        //過疎、過密
        else if (lifeCell.CurrentCellState == LifeCellState.Alive && (count <= 1 || count >= 4))
        {
            return LifeCellState.Dead;
        }
        //誕生
        else if (lifeCell.CurrentCellState == LifeCellState.Dead && count == 3)
        {
            return LifeCellState.Alive;
        }

        return LifeCellState.None;
    }

    private int TryGetCellCount(LifeCell lifeCell)
    {
        int count = 0;
        var row = lifeCell.XIndex;
        var column = lifeCell.YIndex;

        { if (TryGetCell(row - 1, column - 1, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }
        { if (TryGetCell(row - 1, column + 0, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }
        { if (TryGetCell(row - 1, column + 1, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }
        { if (TryGetCell(row + 0, column - 1, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }
        { if (TryGetCell(row + 0, column + 1, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }
        { if (TryGetCell(row + 1, column - 1, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }
        { if (TryGetCell(row + 1, column + 0, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }
        { if (TryGetCell(row + 1, column + 1, out LifeCell cell) && cell.CurrentCellState == LifeCellState.Alive) count++; }

        return count;
    }

    private bool TryGetCell(int row, int column, out LifeCell cell)
    {
        if (0 <= row && row < _row &&
            0 <= column && column < _column)
        {
            cell = _cells[row, column];
            return true;
        }

        cell = null;
        return false;
    }
}
