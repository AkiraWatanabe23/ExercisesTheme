using UnityEngine;
using UnityEngine.UI;

public class Sample0602 : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _row = 1;
    [Min(1)]
    [SerializeField] private int _column = 1;

    private Image[,] _images = default;

    private int _holIndex = 0;
    private int _verIndex = 0;

    public int HolIndex
    {
        get => _holIndex;
        set
        {
            var oldCell = _images[_verIndex, _holIndex];

            if (value == _column) _holIndex = 0;
            else if (value < 0) _holIndex = _column - 1;
            else _holIndex = value;

            var newCell = _images[_verIndex, _holIndex];

            oldCell.color = Color.white;
            newCell.color = Color.red;
        }
    }

    public int VerIndex
    {
        get => _verIndex;
        set
        {
            var oldCell = _images[_verIndex, _holIndex];

            if (value == _row) _verIndex = 0;
            else if (value < 0) _verIndex = _row - 1;
            else _verIndex = value;

            var newCell = _images[_verIndex, _holIndex];

            oldCell.color = Color.white;
            newCell.color = Color.red;
        }
    }

    private void Start()
    {
        _images = new Image[_row, _column];

        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))  MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
        if (Input.GetKeyDown(KeyCode.UpArrow))    MoveUp();
        if (Input.GetKeyDown(KeyCode.DownArrow))  MoveDown();

        if (Input.GetKeyDown(KeyCode.Space)) RemoveCell();
    }

    private void RemoveCell()
    {
        _images[_verIndex, _holIndex].enabled = false;
        Debug.Log("けした");
    }

    private void MoveUp()
    {
        VerIndex--;
    }

    private void MoveDown()
    {
        VerIndex++;
    }

    private void MoveLeft()
    {
        HolIndex--;
    }

    private void MoveRight()
    {
        HolIndex++;
    }
}
