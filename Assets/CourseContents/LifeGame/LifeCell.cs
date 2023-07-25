using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LifeCell : MonoBehaviour, IPointerClickHandler
{
    private LifeCellState _currentCellState = LifeCellState.None;
    private LifeCellState _nextCellState = LifeCellState.None;
    private Image _cellImage = default;
    private int _xIndex = 0;
    private int _yIndex = 0;

    public int XIndex => _xIndex;
    public int YIndex => _yIndex;

    public LifeCellState CurrentCellState
    {
        get => _currentCellState;
        set
        {
            _currentCellState = value;
            SwitchColor();
        }
    }
    public LifeCellState NextCellState
    {
        get => _nextCellState;
        set
        {
            _nextCellState = value;
            _currentCellState = _nextCellState;
            SwitchColor();
        }
    }

    private void OnEnable()
    {
        _cellImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_currentCellState == LifeCellState.Alive)
        {
            _currentCellState = LifeCellState.Dead;
        }
        else if (_currentCellState == LifeCellState.Dead)
        {
            _currentCellState = LifeCellState.Alive;
        }
    }

    private void SwitchColor()
    {
        if (_currentCellState == LifeCellState.Alive)
        {
            _cellImage.color = Color.black;
        }
        else if (_currentCellState == LifeCellState.Dead)
        {
            _cellImage.color = Color.white;
        }
    }

    public void SettingIndex(int row, int column)
    {
        _xIndex = row;
        _yIndex = column;
    }
}

public enum LifeCellState
{
    None,
    Alive,
    Dead,
}
