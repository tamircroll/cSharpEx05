using System;

namespace Othello
{
    using System.Collections.Generic;

    public struct Player
    {
        private readonly string r_Name;
        private readonly ePlayer r_PlayerEnum;
        private DateTime? m_LastUpdateBoard;
        private List<string> m_ValidateMoves;

        public Player(string i_Name, ePlayer i_PlayerEnum, GameBoard i_Board)
        {
            r_Name = i_Name;
            r_PlayerEnum = i_PlayerEnum;
            m_LastUpdateBoard = i_Board.LastUpdate;
            m_ValidateMoves = new List<string>();
            m_ValidateMoves = Controller.ListAllPossibleMoves(this, i_Board);
        }

        public List<string> ValidateMoves
        {
            set { m_ValidateMoves = value; }
        }

        public List<string> GetValidateMoves(GameBoard i_Board)
        {
            if (m_LastUpdateBoard != i_Board.LastUpdate)
            {
                m_LastUpdateBoard = i_Board.LastUpdate;
                m_ValidateMoves = Controller.ListAllPossibleMoves(this, i_Board);
            }

            return m_ValidateMoves;
        }

        public string Name 
        {
            get { return r_Name; }
        }

        public ePlayer PlayerEnum
        {
            get { return r_PlayerEnum; }
        }
    }
}
