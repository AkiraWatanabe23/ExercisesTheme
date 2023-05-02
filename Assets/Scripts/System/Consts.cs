using System.Collections.Generic;

namespace Constants
{
    public static class Consts
    {
        public const int BOARD_SIZE = 8;

        public const int WHITE = 1;
        public const int BLACK = -1;

        //探索の各方向
        public const int LEFT = 1;
        public const int UPPER_LEFT = 2;
        public const int UPPER = 4;
        public const int UPPER_RIGHT = 8;
        public const int RIGHT = 16;
        public const int LOWER_RIGHT = 32;
        public const int LOWER = 64;
        public const int LOWER_LEFT = 128;

        public static readonly Dictionary<SceneNames, string> Scenes = new()
        {
            [SceneNames.TITLE_SCENE] = "Title",
            [SceneNames.GAME_SCENE] = "InGame",
            [SceneNames.RESULT_SCENE] = "Result",
        };
    }

    public enum SceneNames
    {
        TITLE_SCENE,
        GAME_SCENE,
        RESULT_SCENE,
    }

    public enum PieceTypes
    {
        KNIGHT = 2,
        BISHOP,
        ROOK,
        QUEEN,
        KING
    }
}