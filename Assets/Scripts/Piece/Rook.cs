using System;
using Constants;

[Serializable]
public class Rook : PieceMoveBase
{
    private int[,] _board = default;
    /// <summary> 横方向の探索用 </summary>
    private int _checkHol = 0;
    /// <summary> 縦方向の探索用 </summary>
    private int _checkVer = 0;

    public void Start(int[,] board)
    {
        _board = board;
    }

    public override int[] VerticalAndHorizontalSearch(int x, int z)
    {
        _checkHol = x;
        _checkVer = z;

        int moveDir = 0;
        int countUp = SearchLoop(() => _checkVer >= 0, () => _checkVer--);
        int countDown = SearchLoop(() => _checkVer < Consts.BOARD_SIZE, () => _checkVer++);
        int countRight = SearchLoop(() => _checkHol < Consts.BOARD_SIZE, () => _checkHol++);
        int countLeft = SearchLoop(() => _checkHol >= 0, () => _checkHol--);

        return new int[] { moveDir, countUp, countDown, countRight, countLeft };
    }

    public override int SearchLoop(Func<bool> func, Action action)
    {
        int count = 0;

        for (; func(); action())
        {
            //選択した駒の位置から探索を始める
            if (_board[_checkHol, _checkVer] == 0)
            {
                count++;
            }

        }
        _checkVer = 0;
        _checkHol = 0;

        return count;
    }
}
