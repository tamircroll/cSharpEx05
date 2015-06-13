using System;
using System.Drawing;
using System.Windows.Forms;
using Othello.enums;

namespace Othello.Forms
{
    public class BoardWindow : Form
    {
        private const int k_LengthFromBoarders = 15;
        private const int k_CellSize = 45;
        private const int k_CellSpaces = 3;
        private readonly int r_NumOfCells;
        private readonly GameBoard r_Board;
        private readonly OthelloGame r_Othello;
        private Cell[,] m_Cells;

        public BoardWindow(OthelloGame i_Othello, GameBoard i_Board)
        {
            r_Othello = i_Othello;
            r_NumOfCells = i_Board.Size;
            r_Board = i_Board;
            setBordOptions();
            setCells();
            setTitle_PlayerSwitched();

            r_Board.m_ColoringCell += setCellColor_CellColored;
            r_Othello.m_GameOver += exitGame_GameOver;
            r_Othello.m_PlayerSwitched += setTitle_PlayerSwitched;
        }

        private void setTitle_PlayerSwitched()
        {
            Text = string.Format("OthelloGame - {0}'s Player Turn", r_Othello.CurPlayer);
        }

        private void setCells()
        {
            for (int i = 0; i < r_NumOfCells; i++)
            {
                for (int j = 0; j < r_NumOfCells; j++)
                {
                    m_Cells[i, j] = initCell(i, j);
                    if (r_Board[i, j] != ePlayer.NoPlayer)
                    {
                        setCellColor_CellColored(r_Board[i, j], i, j);
                    }
                    
                    Controls.Add(m_Cells[i, j]);
                    m_Cells[i, j].Click += executeMove;
                }
            }
        }

        private void executeMove(object i_Sender, EventArgs i_E)
        {
            Cell cell = i_Sender as Cell;
            if (cell != null)
            {
                r_Othello.PlayTurn(cell.Row, cell.Column);
            }
        }

        private Cell initCell(int i_Row, int i_Column)
        {
            int cellWidthLocation = (i_Row * k_CellSize) + (i_Row * k_CellSpaces) + k_LengthFromBoarders;
            int cellHightLocation = (i_Column * k_CellSize) + (i_Column * k_CellSpaces) + k_LengthFromBoarders;
            Cell toReturn = new Cell(i_Row, i_Column);

            toReturn.BackColor = Color.LightGray;
            toReturn.ForeColor = Color.Black;
            toReturn.Height = k_CellSize;
            toReturn.Width = k_CellSize;
            toReturn.Location = new Point(cellWidthLocation, cellHightLocation);
            toReturn.Enabled = false;

            return toReturn;
        }

        private void setBordOptions()
        {
            m_Cells = new Cell[r_NumOfCells, r_NumOfCells];
            int boardSize = (k_LengthFromBoarders * 2) + (k_CellSize * r_NumOfCells) + ((r_NumOfCells - 1) * k_CellSpaces);
            ClientSize = new Size(boardSize, boardSize);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void setCellColor_CellColored(ePlayer i_Player, int i_Row, int i_Column)
        {
            Cell cell = m_Cells[i_Row, i_Column];

            cell.Player = i_Player;

            switch (i_Player)
            {
                case ePlayer.NoPlayer:
                    cell.BackColor = Color.LightGray;
                    cell.Enabled = false;
                    break;
                case ePlayer.White:
                    cell.ForeColor = Color.Black;
                    cell.BackColor = Color.White;
                    cell.Enabled = false;
                    cell.Text = "O";
                    break;
                case ePlayer.Black:
                    cell.ForeColor = Color.White;
                    cell.BackColor = Color.Black;
                    cell.Enabled = false;
                    cell.Text = "O";
                    break;
                case ePlayer.PossibleMove:
                    cell.Enabled = true;
                    cell.BackColor = Color.LawnGreen;
                    break;
            }
        }

        private void exitGame_GameOver()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
