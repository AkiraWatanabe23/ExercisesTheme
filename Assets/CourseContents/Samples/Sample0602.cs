using UnityEngine;
using UnityEngine.UI;

public class Sample0602 : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _rows = 1;
    [Min(1)]
    [SerializeField] private int _columns = 1;

    private Image[,] _images = default;

    private int _verIndex = 0;
    private int _holIndex = 0;

    private void Start()
    {
        _images = new Image[_rows, _columns];

        var layout = GetComponent<GridLayoutGroup>();
        layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layout.constraintCount = _columns;

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                if (r == 0 && c == 0) { image.color = Color.red; }
                else { image.color = Color.white; }

                _images[r, c] = image;
            }
        }
        _holIndex = 0;
        _verIndex = 0;
    }

    private void Update()
    {
        //各キー入力
        if (Input.GetKeyDown(KeyCode.LeftArrow))  TryMoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) TryMoveRight();
        if (Input.GetKeyDown(KeyCode.UpArrow))    TryMoveUp();
        if (Input.GetKeyDown(KeyCode.DownArrow))  TryMoveDown();

        if (Input.GetKeyDown(KeyCode.Space)) RemoveCell();
    }

    private bool TrySelectCell(int row, int column)
    {
        if (row < 0 || row >= _rows ||
            column < 0 || column >= _columns)
        {
            return false;
        }

        var oldCell = _images[_verIndex, _holIndex];
        var newCell = _images[row, column];

        if (!newCell.enabled) return false;

        oldCell.color = Color.white;
        newCell.color = Color.red;

        _verIndex = row;
        _holIndex = column;

        return true;
    }

    private void RemoveCell()
    {
        _images[_verIndex, _holIndex].enabled = false;
        Debug.Log("けした");

        _ = TryMoveLeft() || TryMoveRight() || TryMoveUp() || TryMoveDown();
    }

    private bool TryMoveLeft()
    {
        for (int c = _holIndex - 1; c >= 0; c--)
        {
            if (TrySelectCell(_verIndex, c)) return true;
        }
        return false;
    }

    private bool TryMoveRight()
    {
        for (int c = _holIndex + 1; c < _columns; c++)
        {
            if (TrySelectCell(_verIndex, c)) return true;
        }
        return false;
    }

    private bool TryMoveUp()
    {
        for (int r = _verIndex - 1; r >= 0; r--)
        {
            if (TrySelectCell(r, _holIndex)) return true;
        }
        return false;
    }

    private bool TryMoveDown()
    {
        for (int r = _verIndex + 1; r < _rows; r++)
        {
            if (TrySelectCell(r, _holIndex)) return true;
        }
        return false;
    }
}
