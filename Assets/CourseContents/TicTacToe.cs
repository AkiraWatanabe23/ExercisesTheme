using UnityEngine;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour
{
    private const int Size = 3;

    private Image[,] _cells;

    [SerializeField] private Color _normalCell = Color.white;
    [SerializeField] private Color _selectedCell = Color.cyan;

    [SerializeField] private Sprite _circle = null;
    [SerializeField] private Sprite _cross = null;

    private int _selectedRow = 0;
    private int _selectedColumn = 0;

    private int _currentTurn = 0;

    private const int Circle = 0;
    private const int Cross = 1;

    private bool _isWinning = false;

    private void Start()
    {
        _cells = new Image[Size, Size];

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r},{c})");
                cell.transform.parent = transform;
                var image = cell.AddComponent<Image>();

                _cells[r, c] = image;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { _selectedColumn--; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { _selectedColumn++; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { _selectedRow--; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { _selectedRow++; }

        if (_selectedColumn < 0) { _selectedColumn = 0; }
        if (_selectedColumn >= Size) { _selectedColumn = Size - 1; }
        if (_selectedRow < 0) { _selectedRow = 0; }
        if (_selectedRow >= Size) { _selectedRow = Size - 1; }

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = _cells[r, c];
                var image = cell.GetComponent<Image>();
                image.color =
                    (r == _selectedRow && c == _selectedColumn)
                    ? _selectedCell : _normalCell;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) TrySelectCell();
        if (Input.GetKeyDown(KeyCode.Escape)) ResetGame();
    }

    private void TrySelectCell()
    {
        if (_isWinning) return;

        var cell = _cells[_selectedRow, _selectedColumn];
        var image = cell.GetComponent<Image>();

        if (image.sprite != null)
        {
            Debug.Log("既に選択済みです");
            return;
        }

        image.sprite = _currentTurn % 2 == Circle ? _circle : _cross;

        if (WinningCheck())
        {
            Debug.Log("ゲーム終了");
            _isWinning = true;
        }

        _currentTurn++;
    }

    private bool WinningCheck()
    {
        int checkCount = 0;
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                if (_cells[r, c].sprite != null) checkCount++;
            }
        }
        if (checkCount == Size * Size) return false;

        int diagonalLeft = 0;
        int diagonalRight = 0;

        for (int r = 0; r < _cells.GetLength(0); r++)
        {
            if (_currentTurn % 2 == Circle)
            {
                if (_cells[r, r].sprite == _circle) diagonalLeft++;
                if (_cells[Size - 1 - r, r].sprite == _circle) diagonalRight++;
            }
            else
            {
                if (_cells[r, r].sprite == _cross) diagonalLeft++;
                if (_cells[Size - 1 - r, r].sprite == _cross) diagonalRight++;
            }

            if (diagonalLeft == Size || diagonalRight == Size) return true;


            int rowCount = 0;
            int columnCount = 0;

            for (int c = 0; c < _cells.GetLength(1); c++)
            {
                if (_currentTurn % 2 == Circle)
                {
                    //縦
                    if (_cells[c, r].sprite == _circle) rowCount++;
                    //横
                    if (_cells[r, c].sprite == _circle) columnCount++;
                }
                else
                {
                    //縦
                    if (_cells[c, r].sprite == _cross) rowCount++;
                    //横
                    if (_cells[r, c].sprite == _cross) columnCount++;
                }

                if (rowCount == Size || columnCount == Size) return true;
            }
        }
        return false;
    }

    private void ResetGame()
    {
        foreach (var c in _cells)
        {
            c.sprite = null;
        }
        Debug.Log("ゲームをやり直します");
        _isWinning = false;
    }
}