using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Othello.enums;

namespace Othello.Logic
{
    public class GameHandler
    {
        private readonly GameBoard r_Board;
        private readonly eNumOfPlayers r_NumOfPlayers;
        private ePlayer m_PlayerWhite;
        private ePlayer m_PlayerBlack;
        private int m_BlackWins = 0;
        private int m_WhiteWins = 0;
        private ePlayer m_CurPlayer;

        public GameHandler(int i_BoardSize, eNumOfPlayers i_NumOfPlayers)
        {
            r_NumOfPlayers = i_NumOfPlayers;
            setPlayers();
            r_Board = new GameBoard(this, i_BoardSize);
        }

        public ePlayer CurPlayer
        {
            get { return m_CurPlayer; }
            set { m_CurPlayer = value; }
        }

        public GameBoard Board
        {
            get { return r_Board; }
        }

        public virtual void OnPlayerSwitched()
        {
            if (m_PlayerSwitched != null)
            {
                m_PlayerSwitched.Invoke();
            }
        }

        public virtual void OnGameOver()
        {
            if (m_GameOver != null)
            {
                m_GameOver.Invoke();
            }
        }

        public event PlayerSwitchedDelegate m_PlayerSwitched;

        public event GameOverDelegate m_GameOver;

        public void PlayTurn(int i_Row, int i_Column)
        {
            r_Board.RemovePosibleMoves();
            MovesHandler.ExecutePlayMove(i_Row, i_Column, CurPlayer, r_Board);
            AfterTurn();
        }

        public bool ToExitGame()
        {
            ePlayer winner = getWinner();
            addOneToWinner(winner);
            return !toPlayAgain(winner);
        }

        private void addOneToWinner(ePlayer i_Winner)
        {
            if (i_Winner == ePlayer.White)
            {
                m_WhiteWins++;
            }
            else if (i_Winner == ePlayer.Black)
            {
                m_BlackWins++;
            }
        }

        private ePlayer getWinner()
        {
            ePlayer winner;

            if (r_Board.PlayerWhiteScore > r_Board.PlayerBlackScore)
            {
                winner = ePlayer.White;
            }
            else if (r_Board.PlayerWhiteScore < r_Board.PlayerBlackScore)
            {
                winner = ePlayer.Black;
            }
            else
            {
                winner = ePlayer.NoPlayer;
            }

            return winner;
        }

        private bool toPlayAgain(ePlayer i_Winner)
        {
            StringBuilder msg = new StringBuilder();

            if (i_Winner == ePlayer.White)
            {
                msg.Append("White Won!!! ");
            }
            else if (i_Winner == ePlayer.Black)
            {
                msg.Append("Black Won!!! ");
            }
            else
            {
                msg.Append("Tie!!! ");
            }

            msg.AppendFormat("({0}/{1})", r_Board.PlayerWhiteScore, r_Board.PlayerBlackScore);
            msg.AppendFormat("({0}/{1}){2}", m_WhiteWins, m_BlackWins, Environment.NewLine);
            msg.AppendFormat("Would you like to play another round?");

            DialogResult toPlayAgain = MessageBox.Show(
                msg.ToString(),
                "Othello",
                MessageBoxButtons.YesNo);

            return toPlayAgain == DialogResult.Yes;
        }

        private void setPlayers()
        {
            m_PlayerWhite = ePlayer.White;
            m_PlayerBlack = ePlayer.Black;
            CurPlayer = m_PlayerWhite;
        }

        private void switchPlayer()
        {
            CurPlayer = CurPlayer.Equals(m_PlayerWhite) ? m_PlayerBlack : m_PlayerWhite;
            OnPlayerSwitched();
        }

        public void AfterTurn()
        {
            if (isGameOver())
            {
                OnGameOver();
            }
            else
            {
                switchPlayer();
                if (r_NumOfPlayers == eNumOfPlayers.OnePlayer && CurPlayer == ePlayer.Black)
                {
                    AutoPlay.ComputerPlay(this, r_Board);
                }
                else if (!r_Board.SetPossibleMoves())
                {
                    AfterTurn();
                }
            }
        }

        private bool isGameOver()
        {
            List<int[]> whitePlayerPossibles = MovesHandler.ListAllPossibleMoves(ePlayer.White, r_Board);
            List<int[]> blackPlayerPossibles = MovesHandler.ListAllPossibleMoves(ePlayer.Black, r_Board);
            
            return whitePlayerPossibles.Count == 0 && blackPlayerPossibles.Count == 0;
        }
    }
}