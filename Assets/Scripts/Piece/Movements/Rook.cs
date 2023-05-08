using Constants;
using System;

/// <summary> 縦横探索 </summary>
[Serializable]
public class Rook : PieceMoveBase
{
    private ChildArray[] _board = default;
    /// <summary> 横方向の探索用 </summary>
    private int _checkHol = 0;
    /// <summary> 縦方向の探索用 </summary>
    private int _checkVer = 0;

    public void Start(ChildArray[] board)
    {
        _board = board;
    }

    public override int[] VerticalAndHorizontalSearch(int x, int z)
    {
        _checkHol = x;
        _checkVer = z;

        int moveDir = 0;

        int countUp
            = SearchLoop(() => _checkVer >= 0, () => _checkVer--, () => _checkVer = z);
        int countDown
            = SearchLoop(() => _checkVer < Consts.BOARD_SIZE, () => _checkVer++, () => _checkVer = z);
        int countRight
            = SearchLoop(() => _checkHol < Consts.BOARD_SIZE, () => _checkHol++, () => _checkHol = x);
        int countLeft
            = SearchLoop(() => _checkHol >= 0, () => _checkHol--, () => _checkHol = x);

        return new int[] { moveDir, countUp, countDown, countRight, countLeft };
    }

    public override int SearchLoop(Func<bool> func, Action action, Action finishedAction)
    {
        int count = 0;

        for (; func(); action())
        {
            //選択した駒の位置から探索を始める
            if (_board[_checkHol].Array[_checkVer] == 0)
            {
                count++;
            }
        }
        finishedAction();

        var a = Test(_checkVer = 0);

        return count;
    }

    private int Test<T>(T aaa)
    {
        return 0;
    }
}
