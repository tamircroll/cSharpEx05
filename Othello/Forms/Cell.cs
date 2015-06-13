using System.Windows.Forms;
using Othello.enums;

namespace Othello
{
    public class Cell : Button
    {
        private ePlayer m_Player;
        private int m_Row, m_column;

        public Cell(int i_Row, int i_column)
        {
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
    }
}
