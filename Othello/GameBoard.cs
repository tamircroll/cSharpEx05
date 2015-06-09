using System;
using System.Collections.Generic;

namespace Othello
{
    public delegate void SetCellColor(ePlayer i_Player, int i_Row, int i_column);

    public delegate void SetCellPossibleMove(ePlayer i_Player, int i_Row, int i_column);
    
    public delegate void SetCellEmpty();
    
    public delegate void GameOver();

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

        public event GameOver m_GameOver;

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

        public void InitFirstPlayers()
        {
            m_Board[Size / 2, Size / 2] = ePlayer.WhitePlayer;
            m_Board[(Size / 2) - 1, (Size / 2) - 1] = ePlayer.WhitePlayer;
            m_Board[Size / 2, (Size / 2) - 1] = ePlayer.BlackPlayer;
            m_Board[(Size / 2) - 1, Size / 2] = ePlayer.BlackPlayer;
            m_SetColor.Invoke(ePlayer.WhitePlayer, Size / 2, Size / 2);
            m_SetColor.Invoke(ePlayer.WhitePlayer, (Size / 2) - 1, (Size / 2) - 1);
            m_SetColor.Invoke(ePlayer.BlackPlayer, Size / 2, (Size / 2) - 1);
            m_SetColor.Invoke(ePlayer.BlackPlayer, (Size / 2) - 1, Size / 2);
            SetPossibleMoves();
        }

        public void SetPossibleMoves()
        {
            List<int[]> possibles = Controller.ListAllPossibleMoves(m_Othello.CurPlayer, this);

            if (possibles.Count == 0)
            {
                m_Othello.SwitchCurPlayer();
                possibles = Controller.ListAllPossibleMoves(m_Othello.CurPlayer, this);
            }

            if (possibles.Count != 0)
            {
                setPossibleMove(possibles);
            }
            else
            {
                m_GameOver.Invoke();
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
                if (value == ePlayer.WhitePlayer && m_Board[i_Row, i_Col] == ePlayer.BlackPlayer)
                {
                    m_PlayerOneScore++;
                    m_PlayerTwoScore--;
                }
                else if (value == ePlayer.BlackPlayer && m_Board[i_Row, i_Col] == ePlayer.WhitePlayer)
                {
                    m_PlayerOneScore--;
                    m_PlayerTwoScore++;
                }
                else if (value == ePlayer.WhitePlayer && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
                {
                    m_PlayerOneScore++;
                }
                else if (value == ePlayer.BlackPlayer && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
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
            return i_Player == ePlayer.WhitePlayer ? m_PlayerOneScore : m_PlayerTwoScore;
        }
    }
}
