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
            }
        }

        private void setPlayers(eNumOfPlayers numOfPlayers)
        {
            m_Player1 = new Player(ePlayer.Player1, m_Board);

            if (numOfPlayers == eNumOfPlayers.OnePlayer)
            {
                m_Player2 = new Player(ePlayer.Player2, m_Board);
            }
            else
            {
                m_Player2 = new Player(ePlayer.Player2, m_Board);
            }
        }

        public void SwitchCurPlayer()
        {
            CurPlayer = CurPlayer.Equals(m_Player1) ? m_Player2 : m_Player1;
        }
    }
}
