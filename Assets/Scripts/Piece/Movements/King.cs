using System;

public class King : PieceMoveBase
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
