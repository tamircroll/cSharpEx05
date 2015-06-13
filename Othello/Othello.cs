using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Othello.enums;
using Othello.Logic;
using Othello.UIForms;

namespace Othello
{
    public delegate void PlayerSwitchedDelegate();

    public delegate void GameOverDelegate();

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
                while (!exitGame)
                {
                    m_GameHandler = new GameHandler(formGameOptions.BoardSize, formGameOptions.NumOfPlayers);
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