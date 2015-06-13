using System;
using System.Drawing;
using System.Windows.Forms;

namespace Othello
{
    public class FormGameOptions : Form
    {
        private const int k_LengthFromSideBoarder = 10;
        private Button m_ButtonEncreaseSize = new Button();
        private Button m_ButtonOnePlayer = new Button();
        private Button m_ButtonTwoPlayer = new Button();
        private int m_ButtonHight;
        private int m_BoardSize = 6;
        private eNumOfPlayers m_NumOfPlayers;

        public FormGameOptions()
        {
            Size = new Size(450, 150);
            m_ButtonHight = ClientSize.Height / 3;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Othello - Game Settings";
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public eNumOfPlayers NumOfPlayers
        {
            get { return m_NumOfPlayers; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            Controls.Add(m_ButtonEncreaseSize);
            Controls.Add(m_ButtonOnePlayer);
            Controls.Add(m_ButtonTwoPlayer);
        }

        private void InitControls()
        {
            initEncreaseSizeButton();
            initOnePlayerButton();
            initTwoPlayerButton();
        }

        private void initEncreaseSizeButton()
        {
            m_ButtonEncreaseSize.Text = string.Format("Board SIze {0}X{0} (click to increase)", BoardSize);
            m_ButtonEncreaseSize.Location = new Point(k_LengthFromSideBoarder, ClientSize.Height / 10);
            m_ButtonEncreaseSize.Width = ClientSize.Width - (2 * k_LengthFromSideBoarder);
            m_ButtonEncreaseSize.Height = m_ButtonHight;
            m_ButtonEncreaseSize.Click += buttonIncreaseBoardSizeByTwo_Click;
        }

        private void initOnePlayerButton()
        {
            m_ButtonOnePlayer.Text = "Play against the computer";
            m_ButtonOnePlayer.Location = new Point(k_LengthFromSideBoarder, ClientSize.Height / 2);
            m_ButtonOnePlayer.Width = (m_ButtonEncreaseSize.Width - k_LengthFromSideBoarder) / 2;
            m_ButtonOnePlayer.Height = m_ButtonHight;
            m_ButtonOnePlayer.Click += buttonOnePlayer_Click;
        }

        private void initTwoPlayerButton()
        {
            m_ButtonTwoPlayer.Text = "Play against your friend";
            m_ButtonTwoPlayer.Location = new Point((2 * k_LengthFromSideBoarder) + m_ButtonOnePlayer.Width, ClientSize.Height / 2);
            m_ButtonTwoPlayer.Width = (m_ButtonEncreaseSize.Width - k_LengthFromSideBoarder) / 2;
            m_ButtonTwoPlayer.Height = m_ButtonHight;
            m_ButtonTwoPlayer.Click += buttonTwoPlayers_Click;
        }

        private void buttonIncreaseBoardSizeByTwo_Click(object i_Sender, EventArgs i_E)
        {
            if (m_BoardSize < 12)
            {
                m_BoardSize += 2;
            }
            else
            {
                m_BoardSize = 6;
            }

            ((Button)i_Sender).Text = string.Format("Board SIze {0}X{0} (click to increase)", BoardSize);
        }

        private void buttonOnePlayer_Click(object i_Sender, EventArgs i_E)
        {
            m_NumOfPlayers = eNumOfPlayers.OnePlayer;
            Close();
        }

        private void buttonTwoPlayers_Click(object i_Sender, EventArgs i_E)
        {
            m_NumOfPlayers = eNumOfPlayers.TwoPlayers;
            Close();
        }
    }
}