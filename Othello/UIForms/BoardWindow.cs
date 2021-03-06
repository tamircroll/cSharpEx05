﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Othello.enums;
using Othello.Logic;

namespace Othello.UIForms
{
    public class BoardWindow : Form
    {
        private const int k_LengthFromBoarders = 15, k_CellSize = 45, k_CellSpaces = 3;
        private const string k_NotEmptyCellText = "O";
        private readonly int r_NumOfCells;
        private readonly GameBoard r_Board;
        private readonly GameHandler m_GameHandler;
        private Cell[,] m_Cells;

        public BoardWindow(GameHandler i_GameHandler, GameBoard i_Board)
        {
            m_GameHandler = i_GameHandler;
            r_NumOfCells = i_Board.Size;
            r_Board = i_Board;
            setBordOptions();
            setCells();
            setTitle_PlayerSwitched();

            r_Board.m_ColoringCell += setCellColor_CellColored;
            i_GameHandler.m_GameOver += exitGame_GameOver;
            i_GameHandler.m_PlayerSwitched += setTitle_PlayerSwitched;
        }

        private void setTitle_PlayerSwitched()
        {
            Text = string.Format("Othello - {0}'s Player Turn", m_GameHandler.CurPlayer);
        }

        private void setCells()
        {
            for (int i = 0; i < r_NumOfCells; i++)
            {
                for (int j = 0; j < r_NumOfCells; j++)
                {
                    m_Cells[i, j] = initCell(i, j);
                    m_Cells[i, j].Click += executeMove_Click;
                    if (r_Board[i, j] != ePlayer.NoPlayer)
                    {
                        setCellColor_CellColored(r_Board[i, j], i, j);
                    }
                    
                    Controls.Add(m_Cells[i, j]);
                }
            }
        }

        private void executeMove_Click(object i_Sender, EventArgs i_E)
        {
            Cell cell = i_Sender as Cell;
            if (cell != null)
            {
                m_GameHandler.PlayTurn(cell.Row, cell.Column);
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
                    cell.Enabled = true;
                    cell.Text = k_NotEmptyCellText;
                    cell.Click -= executeMove_Click;
                    break;
                case ePlayer.Black:
                    cell.ForeColor = Color.White;
                    cell.BackColor = Color.Black;
                    cell.Enabled = true;
                    cell.Text = k_NotEmptyCellText;
                    cell.Click -= executeMove_Click;
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
