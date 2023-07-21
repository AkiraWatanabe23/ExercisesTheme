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
    private string _startData = default;
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

                var random = Random.Range(1, 3);
                cell.CurrentCellState = random == 1 ? LifeCellState.Alive : LifeCellState.Dead;
                cell.SwitchColor();
                _cells[r, c] = cell;
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

    private void DataLoad()
    {
        //ここで初期設定
    }

    private void MoveToNextGeneration()
    {
        for (int r = 0; r < _row; r++)
        {
            for (int c = 0; c < _column; c++)
            {
                _cells[r, c].NextCellState = TryCheckAlive(r, c);
            }
        }

        for (int r = 0; r < _row; r++)
        {
            for (int c = 0; c < _column; c++)
            {
                _cells[r, c].CurrentCellState = _cells[r, c].NextCellState;
                _cells[r, c].SwitchColor();
            }
        }
    }

    /// <summary> 指定したセルが次の世代で生きているか判定する </summary>
    /// <returns> 生きていたらtrue </returns>
    private LifeCellState TryCheckAlive(int row, int column)
    {
        int count = TryGetCell(row, column);
        if (_cells[row, column].CurrentCellState == LifeCellState.Alive)
        {
            if (count == 2 || count == 3) //生存
            {
                return LifeCellState.Alive;
            }
            else //過疎、過密
            {
                return LifeCellState.Dead;
            }
        }
        else if (_cells[row, column].CurrentCellState == LifeCellState.Dead)
        {
            if (count == 3) return LifeCellState.Alive; //誕生
        }

        return LifeCellState.None;
    }

    private int TryGetCell(int row, int column)
    {
        int count = 0;

        for (int r = -1; r <= 1; r++)
        {
            for (int c = -1; c <= 1; c++)
            {
                if (r == row && c == column) continue;

                //範囲内で、セルが生きてるか
                if (0 <= row + r && row + r < _row &&
                    0 <= column + c && column + c < _column &&
                    _cells[row + r, column + c].CurrentCellState == LifeCellState.Alive)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
