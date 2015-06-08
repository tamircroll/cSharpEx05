using System;

namespace Othello
{
    using System.Collections.Generic;

    public struct Player
    {
        private readonly ePlayer r_PlayerEnum;
        private DateTime? m_LastUpdateBoard;
        private List<int[]> m_ValidateMoves;

        public Player(ePlayer i_PlayerEnum, GameBoard i_Board)
        {
            r_PlayerEnum = i_PlayerEnum;
            m_LastUpdateBoard = i_Board.LastUpdate;
            m_ValidateMoves = new List<int[]>();
            m_ValidateMoves = Controller.ListAllPossibleMoves(this, i_Board);
        }

//        public List<string> ValidateMoves
//        {
//            set { m_ValidateMoves = value; }
//        }

        public List<int[]> GetValidateMoves(GameBoard i_Board)
        {
            if (m_LastUpdateBoard != i_Board.LastUpdate)
            {
                m_LastUpdateBoard = i_Board.LastUpdate;
                m_ValidateMoves = Controller.ListAllPossibleMoves(this, i_Board);
            }

            return m_ValidateMoves;
        }

        public ePlayer PlayerEnum
        {
            get { return r_PlayerEnum; }
        }
    }
}
