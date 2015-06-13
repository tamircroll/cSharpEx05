using System;
using Othello.enums;

namespace Othello
{
    using System.Collections.Generic;

    public struct Player
    {
        private readonly ePlayer r_PlayerEnum;

        public Player(ePlayer i_PlayerEnum)
        {
            r_PlayerEnum = i_PlayerEnum;
            new List<int[]>();
//            MovesHandler.ListAllPossibleMoves(r_PlayerEnum, i_Board);
        }

        public ePlayer PlayerEnum
        {
            get { return r_PlayerEnum; }
        }
    }
}
