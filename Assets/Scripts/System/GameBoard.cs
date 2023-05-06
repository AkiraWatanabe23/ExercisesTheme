using Constants;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameObject[] _squares = new GameObject[2];

    private int[,] _board = new int[Consts.BOARD_SIZE, Consts.BOARD_SIZE];

    private readonly Knight _knight = new();
    private readonly Rook _rook = new();
    private readonly Bishop _bishop = new();
    private readonly Queen _queen = new();

    public int[,] Board { get => _board; set => _board = value; }

    private void Awake()
    {
        for (int i = 0; i < Consts.BOARD_SIZE; i++)
        {
            for (int j = 0; j < Consts.BOARD_SIZE; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    var black = Instantiate(_squares[0], new Vector3(i, 0, j), Quaternion.identity);
                    black.transform.SetParent(transform);
                }
                else
                {
                    var white = Instantiate(_squares[1], new Vector3(i, 0, j), Quaternion.identity);
                    white.transform.SetParent(transform);
                }
            }
        }

        //TODO：盤面の初期化処理
    }

    private void Start()
    {
        _knight.Start(_board);
        _rook.Start(_board);
        _bishop.Start(_board);
        _queen.Start(_board);
    }
}
