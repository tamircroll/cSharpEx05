using System;
using System.Collections.Generic;

namespace Othello
{
    public delegate void SetCellColor(ePlayer i_Player, int i_Row, int i_column);

    public delegate void SetCellPossibleMove(ePlayer i_Player, int i_Row, int i_column);
    
    public delegate void SetCellEmpty();
    
    public class GameBoard
    {
        private readonly int r_Size;
        private ePlayer[,] m_Board;
        private DateTime? m_LastUpdate;
        private int m_PlayerOneScore = 2, m_PlayerTwoScore = 2;
        private Othello m_Othello;

        public event SetCellColor m_SetColor;

        public event SetCellPossibleMove m_SetPossibleCell;

        public event SetCellEmpty m_SetCellEmpty;

        public GameBoard(Othello i_Othello, int i_Size)
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
            List<int[]> possibles = Controller.ListAllPossibleMoves(m_Othello.CurPlayer, this);

            if (possibles.Count != 0)
            {
                setPossibleMove(possibles);
            }
            else
            {
                m_Othello.DoAfterTurn();
            }
        }

        private void setPossibleMove(List<int[]> possibles)
        {
            foreach (int[] possible in possibles)
            {
                m_SetPossibleCell.Invoke(m_Othello.CurPlayer.PlayerEnum, possible[0], possible[1]);
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

                if (m_SetColor != null)
                {
                    m_SetColor.Invoke(value, i_Row, i_Col);
                    
                }

                m_Board[i_Row, i_Col] = value;
            }
        }

        public void PaintGray()
        {
            m_SetCellEmpty.Invoke();
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