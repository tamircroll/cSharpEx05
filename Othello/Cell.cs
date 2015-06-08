using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Othello
{
    public class Cell : Button
    {
        private ePlayer m_Player;
        private FormBoard m_FormBoard;
        private int m_Row, m_column;
        public Cell(FormBoard i_FormBoard, int i_Row, int i_column)
        {
            m_FormBoard = i_FormBoard;
            m_Row = i_Row;
            m_column = i_column;
        }

        public ePlayer Player
        {
            get { return m_Player; }
            set { m_Player = value; }
        }

        public int Row
        {
            get { return m_Row; }
        }

        public int column
        {
            get { return m_column; }
        }

        public void ChangePlayer(object i_Sender, Object i_Params)
        {
            if (m_Player != ePlayer.NoPlayer)
            {
                m_Player = m_Player == ePlayer.Player1 ? ePlayer.Player2 : ePlayer.Player1;
            }
            else
            {
                throw new Exception("Cannot change an empty cell");
            }
        }

        
    }
}
