using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text _viewText = default;
    [SerializeField]
    private CellState _cellState = CellState.None;

    private string _cellNum = "";
    /// <summary> セルが開いているか </summary>
    private bool _isCellOpen = false;
    /// <summary> セルに旗が立っているか </summary>
    private bool _isFlag = false;

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
                //_viewText.text = "";
                _cellNum = "";
                break;

            case CellState.Mine:
                //_viewText.text = "X";
                _cellNum = "X";
                _viewText.color = Color.red;
                break;

            default:
                //_viewText.text = ((int)_cellState).ToString();
                _cellNum = ((int)_cellState).ToString();
                _viewText.color = Color.blue;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !_isCellOpen && !_isFlag)
        {
            Debug.Log("セルを開きます");

            //セルを開く
            _viewText.text = _cellNum;
            GetComponent<Image>().color = Color.white;
            _isCellOpen = true;
        }
        else if (eventData.button == PointerEventData.InputButton.Right && !_isCellOpen)
        {
            //セルに旗を外す、立てる
            if (_isFlag)
            {
                Debug.Log("セルの旗を外します");
                _viewText.text = "";
                GetComponent<Image>().color = Color.cyan;
                _isFlag = false;
            }
            else
            {
                Debug.Log("セルに旗を立てます");
                _viewText.text = "F";
                GetComponent<Image>().color = Color.gray;
                _isFlag = true;
            }
        }
    }
}
