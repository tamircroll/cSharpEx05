using System.Collections.Generic;
using Othello.enums;

namespace Othello
{
    public delegate void CellColorChangedDelegate(ePlayer i_Player, int i_Row, int i_column);

    public class GameBoard
    {
        public event CellColorChangedDelegate m_ColoringCell;
        
        private readonly int r_Size;
        private readonly OthelloGame r_Othello;
        private ePlayer[,] m_Board;
        private int m_PlayerOneScore = 0, m_PlayerTwoScore = 0;
        private List<int[]> m_PossibleMoves;

        public GameBoard(OthelloGame i_Othello, int i_Size)
        {
            r_Othello = i_Othello;
            r_Size = i_Size;
            m_Board = new ePlayer[r_Size, r_Size];
            InitFirstPieces();
            SetPossibleMoves();
        }

        public ePlayer this[int i_Row, int i_Col]
        {
            get { return m_Board[i_Row, i_Col]; }

            set
            {
                if (value != ePlayer.PossibleMove && value != ePlayer.NoPlayer)
                {
                    calcScore(i_Row, i_Col, value);
                }

                m_Board[i_Row, i_Col] = value;
                OnColoringCell(i_Row, i_Col, value);
            }
        }

        public void InitFirstPieces()
        {
            this[Size / 2, Size / 2] = ePlayer.White;
            this[(Size / 2) - 1, (Size / 2) - 1] = ePlayer.White;
            this[Size / 2, (Size / 2) - 1] = ePlayer.Black;
            this[(Size / 2) - 1, Size / 2] = ePlayer.Black;
        }

        public void SetPossibleMoves()
        {
             m_PossibleMoves = MovesHandler.ListAllPossibleMoves(r_Othello.CurPlayer.PlayerEnum, this);
             foreach (int[] possible in m_PossibleMoves)
            {
                this[possible[0], possible[1]] = ePlayer.PossibleMove;
            }
        }

        private void calcScore(int i_Row, int i_Col, ePlayer value)
        {
            if (value == ePlayer.White && m_Board[i_Row, i_Col] == ePlayer.Black)
            {
                m_PlayerOneScore++;
                m_PlayerTwoScore--;
            }
            else if (value == ePlayer.Black && m_Board[i_Row, i_Col] == ePlayer.White)
            {
                m_PlayerOneScore--;
                m_PlayerTwoScore++;
            }
            else if (value == ePlayer.White && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
            {
                m_PlayerOneScore++;
            }
            else if (value == ePlayer.Black && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
            {
                m_PlayerTwoScore++;
            }
        }

        private void OnColoringCell(int i_Row, int i_Col, ePlayer value)
        {
            if (m_ColoringCell != null)
            {
                m_ColoringCell.Invoke(value, i_Row, i_Col);
            }
        }

        public int Size
        {
            get { return r_Size; }
        }

        public int PlayerOneScore
        {
            get { return m_PlayerOneScore; }
        }

        public int PlayerTwoScore
        {
            get { return m_PlayerTwoScore; }
        }

        public ePlayer[,] Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public int GetScore(ePlayer i_Player)
        {
            return i_Player == ePlayer.White ? m_PlayerOneScore : m_PlayerTwoScore;
        }

        public void RemovePosibleMoves()
        {
            foreach (int[] possible in m_PossibleMoves)
            {
                {
                    if (this[possible[0], possible[1]] == ePlayer.PossibleMove)
                    {
                        this[possible[0], possible[1]] = ePlayer.NoPlayer;
                    }
                }
            }
        }
    }
}