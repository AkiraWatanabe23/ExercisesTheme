using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightsOut : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _row = 5;
    [SerializeField] private int _column = 5;
    [SerializeField]private ClearState _state = ClearState.White;

    private Image[,] _cells = default;
    private int _moveCount = 0;
    private bool _isClear = false;
    private float _timer = 0f;

    private void Start()
    {
        _cells = new Image[_row, _column];
        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;

                _cells[r, c] = cell.AddComponent<Image>();
            }
        }
    }

    private void Update()
    {
        if (!_isClear)
        {
            _timer += Time.deltaTime;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var image = cell.GetComponent<Image>();

        var name = cell.name;

        image.color =
            image.color == Color.black ?
            Color.white : Color.black;

        SwitchCell(int.Parse(name[5].ToString()), int.Parse(name[8].ToString()));
        _isClear = IsClear();
        if (_isClear)
        {
            Debug.Log($"ゲームクリア!!!\n手数：{_moveCount}\nTime：{_timer.ToString("F2")}");
        }
        else
        {
            Debug.Log("ゲーム続行...");
            _moveCount++;
        }
    }

    /// <summary> クリック地点の周囲を見て、色を変換する </summary>
    private void SwitchCell(int r, int c)
    {
        //上
        if (r - 1 >= 0) ChangeColor(r - 1, c);
        //下
        if (r + 1 < _row) ChangeColor(r + 1, c);
        //左
        if (c - 1 >= 0) ChangeColor(r, c - 1);
        //右
        if (c + 1 < _column) ChangeColor(r, c + 1);
    }

    private void ChangeColor(int r, int c)
    {
        _cells[r, c].color =
            _cells[r, c].color == Color.black ?
            Color.white : Color.black;
    }

    private bool IsClear()
    {
        int counter = 0;

        foreach (var cell in _cells)
        {
            if (_state == ClearState.White)
            {
                if (cell.color == Color.white) counter++;
            }
            else
            {
                if (cell.color == Color.black) counter++;
            }
        }
        return counter == _row * _column;
    }
}

public enum ClearState
{
    White,
    Black
}
