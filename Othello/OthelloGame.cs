using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Othello.enums;
using Othello.UIForms;

namespace Othello
{
    public delegate void PlayerSwitchedDelegate();

    public delegate void GameOverDelegate();

    public class OthelloGame
    {
        public event PlayerSwitchedDelegate m_PlayerSwitched;

        public event GameOverDelegate m_GameOver;

        private ePlayer m_PlayerWhite;
        private ePlayer m_PlayerBlack;
        private GameBoard m_Board;
        private ePlayer m_CurPlayer;
        private eNumOfPlayers m_NumOfPlayers;
        private int m_BlackWins = 0, m_WhiteWins = 0;

        public void Play()
        {
            FormGameOptions formGameOptions = new FormGameOptions();
            DialogResult toPlay = formGameOptions.ShowDialog();
            if (toPlay == DialogResult.OK)
            {
                m_NumOfPlayers = formGameOptions.NumOfPlayers;
                bool exitGame = false;
                while (!exitGame)
                {
                    setPlayers();
                    m_Board = new GameBoard(this, formGameOptions.BoardSize);
                    exitGame = new BoardWindow(this, m_Board).ShowDialog() != DialogResult.OK;
                    if (!exitGame)
                    {
                        exitGame = toExitGame();
                    }
                }
            }
        }

        public void PlayTurn(int i_Row, int i_Column)
        {
            m_Board.RemovePosibleMoves();
            MovesHandler.ExecutePlayMove(i_Row, i_Column, CurPlayer, m_Board);
            AfterTurn();
        }

        private bool toExitGame()
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

            if (m_Board.PlayerWhiteScore > m_Board.PlayerBlackScore)
            {
                winner = ePlayer.White;
            }
            else if (m_Board.PlayerWhiteScore < m_Board.PlayerBlackScore)
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

            msg.AppendFormat("({0}/{1})", m_Board.PlayerWhiteScore, m_Board.PlayerBlackScore);
            msg.AppendFormat("({0}/{1}){2}", m_WhiteWins, m_BlackWins, Environment.NewLine);
            msg.AppendFormat("Would you like to play another round?");

            DialogResult toPlayAgain = MessageBox.Show(
                msg.ToString(),
                "OthelloGame",
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
                if (m_NumOfPlayers == eNumOfPlayers.OnePlayer && CurPlayer == ePlayer.Black)
                {
                    AutoPlay.ComputerPlay(this, m_Board);
                }
                else if (!m_Board.SetPossibleMoves())
                {
                    AfterTurn();
                }
            }
        }

        protected virtual void OnPlayerSwitched()
        {
            if (m_PlayerSwitched != null)
            {
                m_PlayerSwitched.Invoke();
            }
        }

        protected virtual void OnGameOver()
        {
            if (m_GameOver != null)
            {
                m_GameOver.Invoke();
            }
        }

        private bool isGameOver()
        {
            List<int[]> whitePlayerPossibles = MovesHandler.ListAllPossibleMoves(ePlayer.White, m_Board);
            List<int[]> blackPlayerPossibles = MovesHandler.ListAllPossibleMoves(ePlayer.Black, m_Board);
            
            return whitePlayerPossibles.Count == 0 && blackPlayerPossibles.Count == 0;
        }

        public ePlayer CurPlayer
        {
            get { return m_CurPlayer; }
            set { m_CurPlayer = value; }
        }
    }
}