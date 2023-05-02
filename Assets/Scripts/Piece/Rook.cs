using System;
using Constants;

[Serializable]
public class Rook : PieceMoveBase
{
    private int[,] _board = default;
    /// <summary> 縦方向の探索用 </summary>
    private int _checkVer = 0;
    /// <summary> 横方向の探索用 </summary>
    private int _checkHol = 0;

    public void Start(int[,] board)
    {
        _board = board;
    }

    public override int[] VerticalAndHorizontalSearch()
    {
        int moveDir = 0;
        int countUp = SearchLoop(() => _checkVer >= 0, () => _checkVer--);
        int countDown = SearchLoop(() => _checkVer < Consts.BOARD_SIZE, () => _checkVer++);
        int countRight = SearchLoop(() => _checkHol < Consts.BOARD_SIZE, () => _checkHol++);
        int countLeft = SearchLoop(() => _checkHol >= 0, () => _checkHol--);

        return new int[] { moveDir, countUp, countDown, countRight, countLeft };
    }

    public override int SearchLoop(Func<bool> func, Action action)
    {
        for (; func(); action())
        {

        }
        _checkVer = 0;
        _checkHol = 0;

        return 0;
    }
}
