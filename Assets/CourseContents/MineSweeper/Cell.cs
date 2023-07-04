using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Text _viewText = default;
    [SerializeField]
    private CellState _cellState = CellState.None;

    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    /// <summary> Scriptがロードされたとき、または InspectorViewの値が更新されたときに呼び出される </summary>
    private void OnValidate()
    {
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if (_viewText == null) return;

        switch (_cellState)
        {
            case CellState.None:
                _viewText.text = "";
                break;

            case CellState.Mine:
                _viewText.text = "X";
                _viewText.color = Color.red;
                break;

            default:
                _viewText.text = ((int)_cellState).ToString();
                _viewText.color = Color.blue;
                break;
        }
    }
}
