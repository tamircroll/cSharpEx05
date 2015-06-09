using System;
using System.Text;
using System.Windows.Forms;

namespace Othello
{
    public class Othello
    {
        private Player m_Player1, m_Player2;
        private GameBoard m_Board;
        private int m_BoardSize;
        private FormGameOptions formGameOptions;
        private Player m_CurPlayer;
        private eNumOfPlayers m_NumOfPlayers;
        private int m_BlackWins = 0, m_WhiteWins = 0;

        public Player CurPlayer
        {
            get { return m_CurPlayer; }
            set { m_CurPlayer = value; }
        }

        public void StartNewGame()
        {
            formGameOptions = new FormGameOptions();
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
                FormBoard m_FormBoard = new FormBoard(this, formGameOptions, m_Board);

                setPlayers(m_NumOfPlayers);
                CurPlayer = m_Player1;
                m_Board.InitFirstPlayers();
                m_FormBoard.ShowDialog();
                ePlayer winner = setWinner();
                addOneToWinner(winner);
                exitGame = toExitGame(winner);
            }
        }

        private void addOneToWinner(ePlayer i_Winner)
        {
            if (i_Winner == ePlayer.WhitePlayer)
            {
                m_WhiteWins++;
            }
            else if (i_Winner == ePlayer.BlackPlayer)
            {
                m_BlackWins++;
            }
        }

        private ePlayer setWinner()
        {
            ePlayer winner;

            if (m_Board.PlayerOneScore > m_Board.PlayerTwoScore)
            {
                winner = ePlayer.WhitePlayer;
            }
            else if (m_Board.PlayerOneScore < m_Board.PlayerTwoScore)
            {
                winner = ePlayer.BlackPlayer;
            }
            else
            {
                winner = ePlayer.NoPlayer;
            }

            return winner;
        }

        private bool toExitGame(ePlayer i_Winner)
        {
            StringBuilder msg = new StringBuilder();

            if (i_Winner == ePlayer.WhitePlayer)
            {
                msg.Append("White Won!!! ");
            }
            else if (i_Winner == ePlayer.BlackPlayer)
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

            DialogResult toPlayAgain = MessageBox.Show(msg.ToString(),
                "Othello",
                MessageBoxButtons.YesNo);

            return toPlayAgain == DialogResult.No;
        }

        private void setPlayers(eNumOfPlayers numOfPlayers)
        {
            m_Player1 = new Player(ePlayer.WhitePlayer, m_Board);

            if (numOfPlayers == eNumOfPlayers.OnePlayer)
            {
                m_Player2 = new Player(ePlayer.BlackPlayer, m_Board);
            }
            else
            {
                m_Player2 = new Player(ePlayer.BlackPlayer, m_Board);
            }
        }

        public void SwitchCurPlayer()
        {
            CurPlayer = CurPlayer.Equals(m_Player1) ? m_Player2 : m_Player1;
        }
    }
}
