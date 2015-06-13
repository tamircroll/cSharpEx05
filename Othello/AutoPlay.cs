﻿using System;
using System.Collections.Generic;
using Othello.enums;

namespace Othello
{
    public static class AutoPlay
    {
        public static void ComputerPlay(OthelloGame i_Othello, GameBoard i_Board)
        {
            List<int[]> allMoves = MovesHandler.ListAllPossibleMoves(ePlayer.Black, i_Board);
            if (allMoves.Count > 0)
            {
                int moveIndex = new Random().Next(allMoves.Count - 1);
                int[] chosenMove = allMoves[moveIndex];
                MovesHandler.ExecutePlayMove(chosenMove[0], chosenMove[1], ePlayer.Black, i_Board);
            }

            i_Othello.AfterTurn();
        }
    }
}