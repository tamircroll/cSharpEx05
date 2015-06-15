using System.Windows.Forms;
using Othello.Logic;
using Othello.UIForms;

namespace Othello
{

    public class Othello
    {
        private GameHandler m_GameHandler;

        public void Play()
        {
            FormGameOptions formGameOptions = new FormGameOptions();
            DialogResult gameOptions = formGameOptions.ShowDialog();
            if (gameOptions == DialogResult.OK)
            {
                bool exitGame = false;
                m_GameHandler = new GameHandler(formGameOptions.BoardSize, formGameOptions.NumOfPlayers);
                while (!exitGame)
                {
                    m_GameHandler.StartANewGame();
                    exitGame = new BoardWindow(m_GameHandler, m_GameHandler.Board).ShowDialog() != DialogResult.OK;
                    if (!exitGame)
                    {
                        exitGame = m_GameHandler.ToExitGame();
                    }
                }
            }
        }
    }
}