using System.Windows.Forms;
using Othello.enums;

namespace Othello.UIForms
{
    public class Cell : Button
    {
        private readonly int r_Row, r_Column;
        private ePlayer m_Player;

        public Cell(int i_Row, int i_Column)
        {
            r_Row = i_Row;
            r_Column = i_Column;
        }

        public ePlayer Player
        {
            get { return m_Player; }
            set { m_Player = value; }
        }

        public int Row
        {
            get { return r_Row; }
        }

        public int Column
        {
            get { return r_Column; }
        }
    }
}
