using System;
using System.Text;
using System.Windows.Forms;

namespace Othello
{
    public delegate void TurnPlayed(); //TODO: Rename!!

    public class Othello
    {
        private Player m_PlayerWhite, m_PlayerBlack;
        private GameBoard m_Board;
        private int m_BoardSize;
        private FormGameOptions formGameOptions;
        private Player m_CurPlayer;
        private eNumOfPlayers m_NumOfPlayers;
        private int m_BlackWins = 0, m_WhiteWins = 0;
        public event TurnPlayed m_TurnPlayed;

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
                setPlayers();
                CurPlayer = m_PlayerWhite;
                FormBoard m_FormBoard = new FormBoard(this, formGameOptions, m_Board);
                m_Board.InitFirstPieces();
                m_FormBoard.ShowDialog();
                ePlayer winner = setWinner();
                addOneToWinner(winner);
                exitGame = toExitGame(winner);
            }
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

        public void DoAfterTurn()
        {
            m_TurnPlayed.Invoke();
        }

        private ePlayer setWinner()
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

        private bool toExitGame(ePlayer i_Winner)
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
                "Othello",
                MessageBoxButtons.YesNo);

            return toPlayAgain == DialogResult.No;
        }

        private void setPlayers()
        {
            m_PlayerWhite = new Player(ePlayer.White, m_Board);
            m_PlayerBlack = new Player(ePlayer.Black, m_Board);
        }

        public void SwitchCurPlayer()
        {
            CurPlayer = CurPlayer.Equals(m_PlayerWhite) ? m_PlayerBlack : m_PlayerWhite;
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
