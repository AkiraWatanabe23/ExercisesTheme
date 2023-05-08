using Constants;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = Vector3.up;
    [Tooltip("初期盤面の二次元配列")]
    [SerializeField] private ChildArray[] _board = new ChildArray[Consts.BOARD_SIZE];
    [SerializeField] private GameObject[] _squares = new GameObject[2];
    [SerializeField] private GameObject[] _whitePieces = new GameObject[5];
    [SerializeField] private GameObject[] _blackPieces = new GameObject[5];

    private readonly Knight _knight = new();
    private readonly Rook _rook = new();
    private readonly Bishop _bishop = new();
    private readonly Queen _queen = new();

    private readonly Quaternion _rot = Quaternion.Euler(-90f, 0f, 0f);

    public ChildArray[] Board { get => _board; set => _board = value; }

    private void Awake()
    {
        //盤をつくる
        for (int i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (int j = 0; j < Consts.BOARD_SIZE; j++)
            {
                if ((i + j) % 2 == 1)
                {
                    var black = Instantiate(_squares[0], new Vector3(i, 0, -j), Quaternion.identity);
                    black.transform.SetParent(transform);
                }
                else
                {
                    var white = Instantiate(_squares[1], new Vector3(i, 0, -j), Quaternion.identity);
                    white.transform.SetParent(transform);
                }
            }
        }

        for (int i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (int j = 0; j < Consts.BOARD_SIZE; j++)
            {
                if (_board[i].Array[j] > 0)
                {
                    PieceSet(i, j, Consts.WHITE);
                }
                else if (_board[i].Array[j] < 0)
                {
                    PieceSet(i, j, Consts.BLACK);
                }
            }
        }

    }

    private void Start()
    {
        _knight.Start(_board);
        _rook.Start(_board);
        _bishop.Start(_board);
        _queen.Start(_board);
    }

    private void PieceSet(int x, int z, int color)
    {
        GameObject[] pieces = color == Consts.WHITE ? _whitePieces : _blackPieces;

        switch (_board[x].Array[z] * color)
        {
            case (int)PieceTypes.KNIGHT:
                Instantiate(pieces[0], new Vector3(x, 0, -z) + _offset, _rot);
                break;
            case (int)PieceTypes.BISHOP:
                Instantiate(pieces[1], new Vector3(x, 0, -z) + _offset, _rot);
                break;
            case (int)PieceTypes.ROOK:
                Instantiate(pieces[2], new Vector3(x, 0, -z) + _offset, _rot);
                break;
            case (int)PieceTypes.QUEEN:
                Instantiate(pieces[3], new Vector3(x, 0, -z) + _offset, _rot);
                break;
            case (int)PieceTypes.KING:
                Instantiate(pieces[4], new Vector3(x, 0, -z) + _offset, _rot);
                break;
        }
    }
}

[System.Serializable]
public class ChildArray
{
    public int[] Array = new int[Consts.BOARD_SIZE];
}
