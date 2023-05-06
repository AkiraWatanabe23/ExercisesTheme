using Constants;
using System;

/// <summary> Knightの探索 </summary>
[Serializable]
public class Knight : PieceMoveBase
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

    public override int[] PeculiarSearch(int x, int z)
    {
        _checkHol = x + 1;
        _checkVer = z - 2;

        int i = 0;

        int moveDir = 0;

        int countUp
            = SearchLoop(() => i < 2 && (0 <= _checkHol || _checkHol < Consts.BOARD_SIZE) && 0 <= _checkVer,
                         () => { _checkHol -= 2; i++; },
                         () => { _checkHol = x + 2; _checkVer = z + 4; i = 0; });
        int countDown
            = SearchLoop(() => i < 2 && (0 <= _checkHol || _checkHol < Consts.BOARD_SIZE) && _checkVer < Consts.BOARD_SIZE,
                         () => { _checkHol -= 2; i++; },
                         () => { _checkHol = x + 3; _checkVer = z - 3; i = 0; });
        int countRight
            = SearchLoop(() => i < 2 && _checkHol < Consts.BOARD_SIZE && (0 <= _checkVer || _checkVer < Consts.BOARD_SIZE),
                         () => { _checkVer -= 2; i++; },
                         () => { _checkHol = x - 4; _checkVer = z + 2; i = 0; });
        int countLeft
            = SearchLoop(() => i < 2 && 0 <= _checkHol && (0 <= _checkVer || _checkVer < Consts.BOARD_SIZE),
                         () => { _checkVer -= 2; i++; },
                         () => { _checkHol = x; _checkVer = z; i = 0; });

        return new int[] { moveDir, countUp, countDown, countRight, countLeft };
    }

    public override int SearchLoop(Func<bool> func, Action action, Action finishedAction)
    {
        int count = 0;

        for (; func(); action())
        {
            if (_board[_checkHol, _checkVer] == 0)
            {
                count++;
            }
        }
        finishedAction();

        return count;
    }
}
