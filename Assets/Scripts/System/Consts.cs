using System.Collections.Generic;

namespace Constants
{
    public static class Consts
    {
        public const int BOARD_SIZE = 8;

        public const int WHITE = 1;
        public const int BLACK = -1;

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