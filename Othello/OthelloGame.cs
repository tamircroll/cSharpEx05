using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Othello
{
    public delegate void PlayerSwitchedDelegate();

    public delegate void GameOverDelegate();

    public class OthelloGame
    {
        public event PlayerSwitchedDelegate m_PlayerSwitched;

        public event GameOverDelegate m_GameOver;

        private Player m_PlayerWhite, m_PlayerBlack;
        private GameBoard m_Board;
        private int m_BoardSize;
        private Player m_CurPlayer;
        private eNumOfPlayers m_NumOfPlayers;
        private int m_BlackWins = 0, m_WhiteWins = 0;

        public void StartNewGame()
        {
            FormGameOptions formGameOptions = new FormGameOptions();
            formGameOptions.ShowDialog();
            m_BoardSize = formGameOptions.BoardSize;
            m_NumOfPlayers = formGameOptions.NumOfPlayers;
            startPlaying();
        }

        private void startPlaying()
        {
            bool exitGame = false;

            while (!exitGame)
            {
                m_Board = new GameBoard(this, m_BoardSize);
                setPlayers();
                new BoardWindow(this, m_Board).ShowDialog();
                exitGame = toExitGame();
            }
        }

        public void PlayTurn(int i_Row, int i_Column)
        {
            MovesHandler.ExecutePlayMove(i_Row, i_Column, CurPlayer, m_Board);
            AfterTurn();
        }

        public bool toExitGame()
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

            if (m_Board.PlayerOneScore > m_Board.PlayerTwoScore)
            {
                winner = ePlayer.White;
            }
            else if (m_Board.PlayerOneScore < m_Board.PlayerTwoScore)
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

            msg.AppendFormat("({0}/{1})", m_Board.PlayerOneScore, m_Board.PlayerTwoScore);
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
            m_PlayerWhite = new Player(ePlayer.White, m_Board);
            m_PlayerBlack = new Player(ePlayer.Black, m_Board);
            CurPlayer = m_PlayerWhite;
        }

        public void AfterTurn()
        {
            if (isGameOver())
            {
                OnGameOver();
            }
            else
            {
                m_Board.OnSetCellsEmpty();
                switchedPlayer();

                if (NumOfPlayers == eNumOfPlayers.OnePlayer && CurPlayer.PlayerEnum == ePlayer.Black)
                {
                    AutoPlay.ComputerPlay(this, m_Board);
                }
                else
                {
                    m_Board.SetPossibleMoves();
                }
            }
        }

        private void switchedPlayer()
        {
            CurPlayer = CurPlayer.Equals(m_PlayerWhite) ? m_PlayerBlack : m_PlayerWhite;
            OnPlayerSwitched();
        }

        protected virtual void OnPlayerSwitched()
        {
            m_PlayerSwitched.Invoke();
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
            bool isGameOver = false;

            List<int[]> whitePlayerPossibles = MovesHandler.ListAllPossibleMoves(m_PlayerWhite, m_Board);
            List<int[]> blackPlayerPossibles = MovesHandler.ListAllPossibleMoves(m_PlayerBlack, m_Board);

            if (whitePlayerPossibles.Count == 0 && blackPlayerPossibles.Count == 0)
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        public Player PlayerBlack
        {
            get { return m_PlayerBlack; }
        }

        public Player PlayerWhite
        {
            get { return m_PlayerWhite; }
        }

        public Player CurPlayer
        {
            get { return m_CurPlayer; }
            set { m_CurPlayer = value; }
        }

        public eNumOfPlayers NumOfPlayers
        {
            get { return m_NumOfPlayers; }
        }
    }
}