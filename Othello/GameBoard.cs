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
            m_Board = new ePlayer[i_Size, i_Size];
            m_LastUpdate = DateTime.Now;
        }

        public void InitFirstPlayers()
        {
            m_Board[Size/2, Size/2] = ePlayer.Player1;
            m_Board[(Size/2) - 1, (Size/2) - 1] = ePlayer.Player1;
            m_Board[Size/2, (Size/2) - 1] = ePlayer.Player2;
            m_Board[(Size/2) - 1, Size/2] = ePlayer.Player2;
            m_SetColor.Invoke(ePlayer.Player1,Size/2, Size/2);
            m_SetColor.Invoke(ePlayer.Player1, (Size/2) - 1, (Size/2) - 1);
            m_SetColor.Invoke(ePlayer.Player2, Size/2, (Size/2) - 1);
            m_SetColor.Invoke(ePlayer.Player2, (Size/2) - 1, Size/2);
            SetPossibleMoves();
        }

        public void SetPossibleMoves()
        {
            List<int[]> possibles = Controller.ListAllPossibleMoves(m_Othello.CurPlayer, this);

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
                if (value == ePlayer.Player1 && m_Board[i_Row, i_Col] == ePlayer.Player2)
                {
                    m_PlayerOneScore++;
                    m_PlayerTwoScore--;
                }
                else if (value == ePlayer.Player2 && m_Board[i_Row, i_Col] == ePlayer.Player1)
                {
                    m_PlayerOneScore--;
                    m_PlayerTwoScore++;
                }
                else if (value == ePlayer.Player1 && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
                {
                    m_PlayerOneScore++;
                }
                else if (value == ePlayer.Player2 && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
                {
                    m_PlayerTwoScore++;
                }

                m_Board[i_Row, i_Col] = value;
                m_SetColor.Invoke(m_Othello.CurPlayer.PlayerEnum, i_Row, i_Col);
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
            if (i_Player == ePlayer.Player1)
            {
                return m_PlayerOneScore;
            }
            else
            {
                return m_PlayerTwoScore;
            }
        }

        public GameBoard CloneBoard()
        {
            GameBoard cloned = new GameBoard(m_Othello, Size);
            cloned.m_PlayerOneScore = m_PlayerOneScore;
            cloned.m_PlayerTwoScore = m_PlayerTwoScore;
            ePlayer[,] board = new ePlayer[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    board[row, col] = this[row, col];
                }
            }

            cloned.Board = board;

            return cloned;
        }
    }
}
