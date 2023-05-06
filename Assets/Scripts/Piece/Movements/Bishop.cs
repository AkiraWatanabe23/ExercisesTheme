using Constants;
using System;

/// <summary> 斜め探索 </summary>
[Serializable]
public class Bishop : PieceMoveBase
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

    public override int[] DiagonalSearch(int x, int z)
    {
        _checkHol = x;
        _checkVer = z;

        int moveDir = 0;

        int countUpRight
            = SearchLoop(() => _checkHol < Consts.BOARD_SIZE || _checkVer >= 0,
                         () => { _checkHol++; _checkVer--; },
                         () => { _checkHol = x; _checkVer = z; });
        int countDownRight
            = SearchLoop(() => _checkHol < Consts.BOARD_SIZE || _checkVer < Consts.BOARD_SIZE,
                         () => { _checkHol++; _checkVer++; },
                         () => { _checkHol = x; _checkVer = z; });
        int countUpLeft
            = SearchLoop(() => _checkHol >= 0 || _checkVer >= 0,
                         () => { _checkHol--; _checkVer--; },
                         () => { _checkHol = x; _checkVer = z; });
        int countDownLeft
            = SearchLoop(() => _checkHol >= 0 || _checkVer < Consts.BOARD_SIZE,
                         () => { _checkHol--; _checkVer++; },
                         () => { _checkHol = x; _checkVer = z; });
        
        return new int[] { moveDir, countUpRight, countDownRight, countUpLeft, countDownLeft };
    }

    public override int SearchLoop(Func<bool> func, Action action, Action finishedAction)
    {
        int count = 0;

        for (; func(); action())
        {
            if (_board[_checkHol].Array[_checkVer] == 0)
            {
                count++;
            }
        }
        finishedAction();

        return count;
    }
}
