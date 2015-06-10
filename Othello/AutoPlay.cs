using System;
using System.Collections.Generic;

namespace Othello
{
    public static class AutoPlay
    {
        public static void ComputerPlay(OthelloGame i_Othello, GameBoard i_Board)
        {
            if (i_Othello.CurPlayer.Equals(i_Othello.PlayerBlack))
            {
                List<int[]> allMoves = Controller.ListAllPossibleMoves(i_Othello.PlayerBlack, i_Board);
                if (allMoves.Count > 0)
                {
                    int moveIndex = new Random().Next(allMoves.Count - 1);
                    int[] chosenMove = allMoves[moveIndex];
                    Controller.ExecutePlayMove(i_Othello, chosenMove[0], chosenMove[1], i_Othello.PlayerBlack, i_Board);
                }
                else
                {
                    i_Othello.AfterTurn();
                }
            }
        }
    }
}