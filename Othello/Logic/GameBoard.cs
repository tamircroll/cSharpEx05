using System.Collections.Generic;
using Othello.enums;

namespace Othello.Logic
{
    public delegate void CellColorChangedDelegate(ePlayer i_Player, int i_Row, int i_Column);

    public class GameBoard
    {
        public event CellColorChangedDelegate m_ColoringCell;
        
        private readonly int r_Size;
        private readonly GameHandler r_GameHandler;
        private ePlayer[,] m_Board;
        private int m_PlayerWhiteScore = 0, m_PlayerBlackScore = 0;
        private List<int[]> m_PossibleMoves;

        public GameBoard(GameHandler i_GameHandler, int i_Size)
        {
            r_GameHandler = i_GameHandler;
            r_Size = i_Size;
            InitBoard();
        }

        public void InitBoard()
        {
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

        public bool SetPossibleMoves()
        {
            m_PossibleMoves = MovesHandler.ListAllPossibleMoves(r_GameHandler.CurPlayer, this);
            foreach (int[] possible in m_PossibleMoves)
            {
                this[possible[0], possible[1]] = ePlayer.PossibleMove;
            }

            return m_PossibleMoves.Count > 0;
        }

        private void calcScore(int i_Row, int i_Col, ePlayer i_Player)
        {
            if (i_Player == ePlayer.White && m_Board[i_Row, i_Col] == ePlayer.Black)
            {
                m_PlayerWhiteScore++;
                m_PlayerBlackScore--;
            }
            else if (i_Player == ePlayer.Black && m_Board[i_Row, i_Col] == ePlayer.White)
            {
                m_PlayerWhiteScore--;
                m_PlayerBlackScore++;
            }
            else if (i_Player == ePlayer.White && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
            {
                m_PlayerWhiteScore++;
            }
            else if (i_Player == ePlayer.Black && m_Board[i_Row, i_Col] == ePlayer.NoPlayer)
            {
                m_PlayerBlackScore++;
            }
        }

        private void OnColoringCell(int i_Row, int i_Col, ePlayer i_Player)
        {
            if (m_ColoringCell != null)
            {
                m_ColoringCell.Invoke(i_Player, i_Row, i_Col);
            }
        }

        public int Size
        {
            get { return r_Size; }
        }

        public int PlayerWhiteScore
        {
            get { return m_PlayerWhiteScore; }
        }

        public int PlayerBlackScore
        {
            get { return m_PlayerBlackScore; }
        }

        public ePlayer[,] Board
        {
            set { m_Board = value; }
        }

        public int GetScore(ePlayer i_Player)
        {
            return i_Player == ePlayer.White ? m_PlayerWhiteScore : m_PlayerBlackScore;
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