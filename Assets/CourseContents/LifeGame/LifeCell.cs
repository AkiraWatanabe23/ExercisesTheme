using UnityEngine;
using UnityEngine.UI;

public class LifeCell : MonoBehaviour
{
    [SerializeField]
    private LifeCellState _currentCellState = LifeCellState.None;

    private LifeCellState _nextCellState = LifeCellState.None;
    private Image _cellImage = default;

    public LifeCellState CurrentCellState { get => _currentCellState; set => _currentCellState = value; }
    public LifeCellState NextCellState { get => _nextCellState; set => _nextCellState = value; }

    private void OnEnable()
    {
        _cellImage = GetComponent<Image>();
    }

    public void SwitchColor()
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
}

public enum LifeCellState
{
    None,
    Alive,
    Dead,
}
