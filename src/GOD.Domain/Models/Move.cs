using System;
namespace backend.src.GOD.Domain.Models
{
    public enum Move
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public static class Rules
    {
        public static int GetWinner(Move player1Move, Move player2Move)
        {
            if (player1Move == player2Move)
                return 0;

            if (Math.Abs(player1Move - player2Move) == 1)
            {
                if (player1Move > player2Move)
                    return 1;

                return 2;

            }
            return player1Move == Move.Rock ? 1 : 2;
        }

    }
}