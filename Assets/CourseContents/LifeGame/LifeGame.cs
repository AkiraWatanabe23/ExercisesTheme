using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour
{
    [Range(0.01f, 1f)]
    [SerializeField]
    private float _transitionInterval = 0.1f;
    [Range(10, 100)]
    [SerializeField]
    private int _row = 0;
    [Range(10, 100)]
    [SerializeField]
    private int _column = 0;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = default;
    [SerializeField]
    private Button _startButton = default;
    [SerializeField]
    private Button _pauseButton = default;
    [SerializeField]
    private InputField _inputField = default;
    [SerializeField]
    private LifeCell _cellPrefab = default;

    private bool _isPause = false;
    private float _timer = 0f;
    private LifeCell[,] _cells = default;

    private void Start()
    {
        _gridLayoutGroup.constraint
            = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _column;

        _startButton.onClick.AddListener(() => GameStart());
        _pauseButton.onClick.AddListener(() => GamePause());
        _inputField.onEndEdit.AddListener(SkipGeneration);

        var parent = _gridLayoutGroup.transform;
        _cells = new LifeCell[_row, _column];

        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                cell.name = $"Cell ({r}, {c})";

                _cells[r, c] = cell;
                cell.SettingIndex(r, c);
            }
        }
        _isPause = true;
    }

    private void Update()
    {
        if (_isPause) return;

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

        foreach (var cell in _cells)
        {
            cell.CurrentCellState = cell.NextCellState;
        }
    }

    /// <summary> 指定したセルが次の世代でどうなるか判定する </summary>
    private LifeCellState TryCheckAlive(LifeCell lifeCell)
    {
        int count = TryGetCellCount(lifeCell.XIndex, lifeCell.YIndex);
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

        //変化がない場合は現在のステートを返す
        return lifeCell.CurrentCellState;
    }

    private int TryGetCellCount(int row, int column)
    {
        int count = 0;

        if (TryGetCell(row - 1, column - 1)) count++;
        if (TryGetCell(row - 1, column + 0)) count++;
        if (TryGetCell(row - 1, column + 1)) count++;
        if (TryGetCell(row + 0, column - 1)) count++;
        if (TryGetCell(row + 0, column + 1)) count++;
        if (TryGetCell(row + 1, column - 1)) count++;
        if (TryGetCell(row + 1, column + 0)) count++;
        if (TryGetCell(row + 1, column + 1)) count++;

        return count;
    }

    private bool TryGetCell(int row, int column)
    {
        if (0 <= row && row < _row &&
            0 <= column && column < _column)
        {
            var cell = _cells[row, column];

            if (cell.CurrentCellState == LifeCellState.Alive) return true;
        }

        return false;
    }

    #region UIに設定する関数
    private void SkipGeneration(string input)
    {
        input = _inputField.text;
        var num = int.Parse(input);

        for (int i = 0; i < num; i++)
        {
            MoveToNextGeneration();
        }
    }

    private void GameStart()
    {
        if (!_isPause) return;
        _isPause = false;

    }

    private void GamePause()
    {
        if (_isPause) return;
        _isPause = true;
    }
    #endregion
}
