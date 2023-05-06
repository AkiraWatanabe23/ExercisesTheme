using Constants;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameObject[] _squares = new GameObject[2];

    private int[,] _board = new int[Consts.BOARD_SIZE, Consts.BOARD_SIZE];

    private Rook _rook = new();
    private Bishop _bishop = new();

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

        //盤面の初期化処理
    }

    private void Start()
    {
        _rook.Start(_board);
        _bishop.Start(_board);
    }
}
