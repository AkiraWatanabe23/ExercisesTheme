using UnityEngine;
using UnityEngine.UI;

public class MineSweeper : MonoBehaviour
{
    [Range(5, 20)]
    [SerializeField]
    private int _row = 0;
    [Range(5, 20)]
    [SerializeField]
    private int _column = 0;
    [SerializeField]
    private int _mineCount = 0;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = default;
    [SerializeField]
    private Cell _cellPrefab = default;

    private Cell[,] _cells = default;

    private void Start()
    {
        _gridLayoutGroup.constraint
            = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _column;

        _cells = new Cell[_row, _column];
        var parent = _gridLayoutGroup.transform;

        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }

        //�ݒ肪�傫�������ꍇ�A�����I�ɑS�̂� 1/10 �܂Ő�������
        if (_mineCount > _row * _column)
        {
            Debug.Log("�n���̐����������ߒ������܂�");
            _mineCount = _row * _column / 10;
        }

        for (int i  = 0; i < _mineCount; i++)
        {
            SelectMine();
        }
    }

    /// <summary> �n���̏d��������邽�߁A�}�X���d�Ȃ������蒼�� </summary>
    private void SelectMine()
    {
        var r = Random.Range(0, _row);
        var c = Random.Range(0, _column);

        var cell = _cells[r, c];
        if (cell.CellState != CellState.Mine)
        {
            cell.CellState = CellState.Mine;
            SettingNumber(r,c);
        }
        else
        {
            SelectMine();
        }
    }

    /// <summary> ���͔��ߖT�̃J�E���g�𑝂₷ </summary>
    private void SettingNumber(int row, int column)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j  = -1; j < 2; j++)
            {
                if (0 <= row + i && row + i < _row &&
                    0 <= column + j && column + j < _column &&
                    _cells[row + i, column + j].CellState != CellState.Mine)
                {
                    _cells[row + i, column + j].CellState++;
                }
            }
        }
    }
}

public enum CellState
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1,
}
