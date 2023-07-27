using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LifeCell : MonoBehaviour, IPointerClickHandler
{
    private LifeCellState _currentCellState = LifeCellState.None;
    private LifeCellState _nextCellState = LifeCellState.None;

    private Image _cellImage = default;

    public int XIndex { get; private set; }
    public int YIndex { get; private set; }

    public LifeCellState CurrentCellState
    {
        get => _currentCellState;
        set
        {
            _currentCellState = value;
            SwitchColor();
        }
    }
    public LifeCellState NextCellState { get => _nextCellState; set => _nextCellState = value; }

    private void OnEnable()
    {
        _cellImage = GetComponent<Image>();

        CurrentCellState = LifeCellState.Dead;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_currentCellState == LifeCellState.Alive)
        {
            CurrentCellState = LifeCellState.Dead;
        }
        else if (_currentCellState == LifeCellState.Dead)
        {
            CurrentCellState = LifeCellState.Alive;
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
        XIndex = row;
        YIndex = column;
    }
}

public enum LifeCellState
{
    None,
    Alive,
    Dead,
}
