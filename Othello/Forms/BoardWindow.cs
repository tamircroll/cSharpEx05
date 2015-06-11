using System;
using System.Windows.Forms;
using System.Drawing;

namespace Othello
{
    public class BoardWindow : Form
    {
        private const int k_LengthFromBoarders = 15;
        private const int k_CellSize = 70;
        private const int k_CellSpaces = 1;
        private int m_NumOfCells;
        private GameBoard m_Board;
        private eNumOfPlayers m_NumOfPlayers;
        private OthelloGame m_Othello;
        private Cell[,] m_Cells;

        public BoardWindow(OthelloGame i_Othello, GameBoard i_Board)
        {
            m_Othello = i_Othello;
            m_NumOfCells = i_Board.Size;
            m_NumOfPlayers = i_Othello.NumOfPlayers;
            m_Board = i_Board;
            setBordOptions();
            setCells();
            setTitle_PlayerSwitched();

            m_Board.m_ColoringCell += setCellColor_ColoringCell;
            m_Board.m_SetPossibleCells += possibleMove_SetPossibleMoves;
            m_Board.m_SetCellsEmpty += emptyCells_SetCellsEmpty;
            m_Othello.m_GameOver += exitGame_GameOver;
            m_Othello.m_PlayerSwitched += setTitle_PlayerSwitched;
        }

        private void setTitle_PlayerSwitched()
        {
            Text = string.Format("OthelloGame - {0}'s Player Turn", m_Othello.CurPlayer.PlayerEnum);
        }

        private void setCells()
        {
            for (int i = 0; i < m_NumOfCells; i++)
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
            m_Othello.PlayTurn(cell.Row, cell.column);
        }

        private Cell CreateCell(int i_Row, int i_Column)
        {
            int cellWidthLocation = (i_Row * k_CellSize) + (i_Row * k_CellSpaces) + k_LengthFromBoarders;
            int cellHightLocation = (i_Column * k_CellSize) + (i_Column * k_CellSpaces) + k_LengthFromBoarders;
            Cell toReturn = new Cell(i_Row, i_Column);
            toReturn.BackColor = Color.LightGray;

            toReturn.Height = k_CellSize;
            toReturn.Width = k_CellSize;
            toReturn.Location = new Point(cellWidthLocation, cellHightLocation);
            toReturn.Enabled = false;

            return toReturn;
        }

        private void setBordOptions()
        {
            m_Cells = new Cell[m_NumOfCells, m_NumOfCells];
            int boardSize = (k_LengthFromBoarders * 2) + (k_CellSize * m_NumOfCells) + ((m_NumOfCells - 1) * k_CellSpaces);
            ClientSize = new Size(boardSize, boardSize);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void setCellColor_ColoringCell(ePlayer i_Player, int i_Row, int i_column)
        {
            Cell cell = m_Cells[i_Row, i_column];

            cell.Player = i_Player;
            cell.BackColor = (i_Player == ePlayer.White) ? Color.White : Color.Black;
            cell.ForeColor = (i_Player == ePlayer.White) ? Color.Black : Color.White;
            cell.Text = "O";
            cell.Enabled = false;
        }

        private void possibleMove_SetPossibleMoves(ePlayer i_Player, int i_Row, int i_column)
        {
            if (!(m_NumOfPlayers == eNumOfPlayers.OnePlayer && i_Player == ePlayer.Black))
            {
                Cell cell = m_Cells[i_Row, i_column];
                cell.Enabled = true;
                cell.BackColor = Color.Green;
            }
        }

        private void emptyCells_SetCellsEmpty()
        {
            foreach (Cell cell in m_Cells)
            {
                if (cell.Player == ePlayer.NoPlayer)
                {
                    cell.BackColor = Color.LightGray;
                    cell.Enabled = false;
                }
            }
        }

        public void exitGame_GameOver()
        {
            Close();
        }
    }
}
