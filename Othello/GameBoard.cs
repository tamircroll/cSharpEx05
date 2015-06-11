using System;
using System.Collections.Generic;

namespace Othello
{
    public delegate void CellColorChangedDelegate(ePlayer i_Player, int i_Row, int i_column);

    public delegate void SetCellPossibleMovesDelegate(ePlayer i_Player, int i_Row, int i_column);
    
    public delegate void ClearEmptyCellsDelegate();
    
    public class GameBoard
    {
        private readonly int r_Size;
        private ePlayer[,] m_Board;
        private DateTime? m_LastUpdate;
        private int m_PlayerOneScore = 0, m_PlayerTwoScore = 0;
        private OthelloGame m_Othello;

        public event CellColorChangedDelegate m_ColoringCell;

        public event SetCellPossibleMovesDelegate m_SetPossibleCells;

        public event ClearEmptyCellsDelegate m_SetCellsEmpty;

        public GameBoard(OthelloGame i_Othello, int i_Size)
        {
            m_Othello = i_Othello;
            r_Size = i_Size;
            m_Board = new ePlayer[r_Size, r_Size];
            m_LastUpdate = DateTime.Now;
        }

        public int PlayerOneScore
        {
            get { return m_PlayerOneScore; }
        }

        public int PlayerTwoScore
        {
            get { return m_PlayerTwoScore; }
        }

        public void InitFirstPieces()
        {
            this[Size / 2, Size / 2] = ePlayer.White;
            this[(Size / 2) - 1, (Size / 2) - 1] = ePlayer.White;
            this[Size / 2, (Size / 2) - 1] = ePlayer.Black;
            this[(Size / 2) - 1, Size / 2] = ePlayer.Black;

            SetPossibleMoves();
        }

        public void SetPossibleMoves()
        {
            List<int[]> possibles = MovesHandler.ListAllPossibleMoves(m_Othello.CurPlayer, this);

            if (possibles.Count != 0)
            {
                OnSetPossibleCells(possibles);
            }
            else
            {
                m_Othello.AfterTurn();
            }
        }

        private void OnSetPossibleCells(List<int[]> possibles)
        {
            foreach (int[] possible in possibles)
            {
                m_SetPossibleCells.Invoke(m_Othello.CurPlayer.PlayerEnum, possible[0], possible[1]);
            }
        }

        public ePlayer this[int i_Row, int i_Col]
        {
            get
            {
                return m_Board[i_Row, i_Col];
            }

            set
            {
                m_LastUpdate = DateTime.Now;
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

                OnColoringCell(i_Row, i_Col, value);

                m_Board[i_Row, i_Col] = value;
            }
        }

        private void OnColoringCell(int i_Row, int i_Col, ePlayer value)
        {
            if (m_ColoringCell != null)
            {
                m_ColoringCell.Invoke(value, i_Row, i_Col);
            }
        }


        public void OnSetCellsEmpty()
        {
            m_SetCellsEmpty.Invoke();
        }

        public DateTime? LastUpdate
        {
            get { return m_LastUpdate; }
        }

        public int Size
        {
            get { return r_Size; }
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
    }
}