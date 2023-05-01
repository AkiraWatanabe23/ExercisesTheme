/// <summary> マス探索の基底クラス
///           「どの方向に」「何マス進めるか」を返す </summary>
public abstract class PieceMoveBase
{
    /// <summary> 縦横方向</summary>
    public virtual int[] VerticalAndHorizontalSearch()
    {
        int moveDir = 0;
        int countUp = 0;
        int countDown = 0;
        int countRight = 0;
        int countLeft = 0;

        return new int[] { moveDir, countUp, countDown, countRight, countLeft };
    }

    /// <summary> 斜め方向 </summary>
    public virtual int[] DiagonalSearch()
    {
        int moveDir = 0;
        int countUpRight = 0;
        int countDownRight = 0;
        int countUpLeft = 0;
        int countDownLeft = 0;

        return new int[] { moveDir, countUpRight, countDownRight, countUpLeft, countDownLeft };
    }

    /// <summary> Knight専用 </summary>
    public virtual int[] PeculiarSearch()
    {
        int moveDir = 0;
        int count = 0;

        return new int[] { moveDir, count };
    }
}
