using System;

namespace Othello
{
    public struct Board
    {
        private readonly int r_Size;
        private readonly ePlayer[,] r_Board;
        private DateTime? m_LastUpdate;
  
        public Board(int i_Size)
        {
            r_Size = i_Size;
            r_Board = new ePlayer[i_Size, i_Size];
            m_LastUpdate = DateTime.Now;

            r_Board[i_Size / 2, i_Size / 2] = ePlayer.Player1;
            r_Board[(i_Size / 2) - 1, (i_Size / 2) - 1] = ePlayer.Player1;
            r_Board[i_Size / 2, (i_Size / 2) - 1] = ePlayer.Player2;
            r_Board[(i_Size / 2) - 1, i_Size / 2] = ePlayer.Player2;
        }

        public ePlayer this[int i_X, int i_Y]
        {
            get
            {
                return r_Board[i_X, i_Y];
            }

            set
            {
                m_LastUpdate = DateTime.Now;
                r_Board[i_X, i_Y] = value;
            }
        }

        public DateTime? LastUpdate
        {
            get { return m_LastUpdate; }
        }

        public int Size
        {
            get { return r_Size; }
        }

        public ePlayer[,] GetBoard
        {
            get { return r_Board; }
        }
    }
}
