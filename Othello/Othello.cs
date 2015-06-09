namespace Othello
{
    public class Othello
    {
        private Player m_Player1, m_Player2;
        private GameBoard m_Board;
        private int m_BoardSize;
        private FormBoard m_FormBoard;
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
            bool gameOver = false ,exitGame = false, canPlayerOnePlay = true;

//            while (!gameOver)
//            {
                m_Board = new GameBoard(this, m_BoardSize);
                m_FormBoard = new FormBoard(this, formGameOptions, m_Board);
                setPlayers(m_NumOfPlayers);
                CurPlayer = m_Player2;
                m_Board.InitFirstPlayers();

                m_FormBoard.ShowDialog();

                while (!gameOver)
                {
                    if (canPlayerOnePlay)
                    {
//                        gameOver = playTurn(m_Player1);
                    }

                    bool canPlayerTwoPlay = m_Player2.GetValidateMoves(m_Board).Count > 0;
                    bool canPlayerOnePlayAfterTurn = m_Player1.GetValidateMoves(m_Board).Count > 0;
                    if ((!canPlayerOnePlayAfterTurn && !canPlayerTwoPlay) || exitGame)
                    {
                        break;
                    }

                    if (!canPlayerOnePlay)
                    {
                    }

                    if (canPlayerTwoPlay)
                    {
                        if (m_NumOfPlayers == eNumOfPlayers.TwoPlayers)
                        {
//                            exitGame = playTurn(m_Player2);
                        }
                        else if (m_NumOfPlayers == eNumOfPlayers.OnePlayer)
                        {
//                            AutoPlay.SmartPlay(m_Player2, m_Player1, m_Board, k_AutoPlayerRecDepth);
                        }
                    }

                    canPlayerOnePlay = m_Player1.GetValidateMoves(m_Board).Count > 0;
                    bool canPlayerTwoPlayAfterTurn = m_Player2.GetValidateMoves(m_Board).Count > 0;
                    if ((!canPlayerOnePlay && !canPlayerTwoPlayAfterTurn) || exitGame)
                    {
                        break;
                    }

                    if (!canPlayerTwoPlay)
                    {
                    }
//                }

                m_FormBoard.Close();
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
            CurPlayer = (CurPlayer.Equals(m_Player1)) ? m_Player2 : m_Player1;
        }
    }
}
