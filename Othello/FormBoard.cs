using System;
using System.Windows.Forms;
using System.Drawing;
namespace Othello
{
    public class FormBoard : Form
    {
        private const int k_LengthFromBoarders = 20;
        private const int k_CellSize = 70;
        private int m_NumOfCells;
        private GameBoard m_Board;
        private eNumOfPlayers m_NumOfPlayers;
        private Othello m_Othello;
        Cell[,] m_Cells;

        public FormBoard(Othello i_Othello, FormGameOptions m_FormGameOptions, GameBoard i_Board)
        {
            m_Othello = i_Othello;
            m_NumOfCells = m_FormGameOptions.BoardSize;
            m_NumOfPlayers = m_FormGameOptions.NumOfPlayers;
            m_Board = i_Board;
            setBordOptions();
            setCells();

            m_Board.m_SetColor += SetCell;
            m_Board.m_SetPossibleCell += PossibleMove;
            m_Board.m_SetCellEmpty += EmptyCell;
        }

        private void setCells()
        {
            for (int i = 0 ; i < m_NumOfCells ; i++)
            {
                for (int j = 0; j < m_NumOfCells; j++)
                {
                    m_Cells[i, j] = CreateCell(i, j);
                    Controls.Add(m_Cells[i, j]);

                    m_Cells[i, j].Click += ExecuteMove;
                }
            }
        }

        private void ExecuteMove(object i_Sender, EventArgs i_E)
        {
            Cell cell = i_Sender as Cell;
            Controller.ExecutePlayMove(m_Othello, cell.Row, cell.column, m_Othello.CurPlayer, m_Board);
        }

        public Cell[,] Cells
        {
            get { return m_Cells; }
        }

        private Cell CreateCell(int i_Row, int i_Column)
        {
            int cellWidthLocation = i_Row * k_CellSize + (k_LengthFromBoarders);
            int cellHightLocation = i_Column * k_CellSize + (k_LengthFromBoarders);
            Cell toReturn = new Cell(this, i_Row, i_Column);
            toReturn.BackColor = Color.LightGray;


            toReturn.Height = k_CellSize;
            toReturn.Width = k_CellSize;
            toReturn.Location = new Point(cellWidthLocation, cellHightLocation);
            toReturn.Enabled = false;

            return toReturn;
        }

        public eNumOfPlayers NumOfPlayers
        {
            get { return m_NumOfPlayers; }
        }

        private void setBordOptions()
        {
            m_Cells = new Cell[m_NumOfCells, m_NumOfCells];
            int boardSize = (k_LengthFromBoarders * 2) + (k_CellSize * m_NumOfCells);
            ClientSize = new Size(boardSize, boardSize);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }



        public void SetCell(ePlayer i_Player, int i_Row, int i_column) //TODO: Rename
        {
            Cell cell = Cells[i_Row, i_column];

            cell.Player = i_Player;
            cell.BackColor = (i_Player == ePlayer.Player1) ? Color.White : Color.Black;
            cell.ForeColor = (i_Player == ePlayer.Player1) ? Color.Black : Color.White;
            cell.Text = "O";
            cell.Enabled = false;
        }

        public void PossibleMove(ePlayer i_Player, int i_Row, int i_column) //TODO: Rename
        {
            if (!(NumOfPlayers == eNumOfPlayers.OnePlayer && i_Player != ePlayer.Player2))
            {
                Cell cell = Cells[i_Row, i_column];
                cell.Enabled = true;
                cell.BackColor = Color.Green;
            }
        }

        public void EmptyCell() //TODO: Rename
        {
            foreach (Cell cell in Cells)
            {
                if (cell.Player == ePlayer.NoPlayer)
                {
                    cell.BackColor = Color.LightGray;
                    cell.Enabled = false;
                }
            }
        }
    }
}
