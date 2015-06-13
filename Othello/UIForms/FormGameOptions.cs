using System;
using System.Drawing;
using System.Windows.Forms;
using Othello.enums;

namespace Othello.UIForms
{
    public class FormGameOptions : Form
    {
        private const int k_LengthFromSideBoarder = 10;
        private readonly Button r_ButtonEncreaseSize = new Button();
        private readonly Button r_ButtonOnePlayer = new Button();
        private readonly Button r_ButtonTwoPlayer = new Button();
        private readonly int m_ButtonHight;
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

        protected override void OnLoad(EventArgs i_E)
        {
            base.OnLoad(i_E);
            InitControls();
            Controls.Add(r_ButtonEncreaseSize);
            Controls.Add(r_ButtonOnePlayer);
            Controls.Add(r_ButtonTwoPlayer);
        }

        private void InitControls()
        {
            initEncreaseSizeButton();
            initOnePlayerButton();
            initTwoPlayerButton();
        }

        private void initEncreaseSizeButton()
        {
            r_ButtonEncreaseSize.Text = string.Format("Board SIze {0}X{0} (click to increase)", BoardSize);
            r_ButtonEncreaseSize.Location = new Point(k_LengthFromSideBoarder, ClientSize.Height / 10);
            r_ButtonEncreaseSize.Width = ClientSize.Width - (2 * k_LengthFromSideBoarder);
            r_ButtonEncreaseSize.Height = m_ButtonHight;
            r_ButtonEncreaseSize.Click += buttonIncreaseBoardSizeByTwo_Click;
        }

        private void initOnePlayerButton()
        {
            r_ButtonOnePlayer.Text = "Play against the computer";
            r_ButtonOnePlayer.Location = new Point(k_LengthFromSideBoarder, ClientSize.Height / 2);
            r_ButtonOnePlayer.Width = (r_ButtonEncreaseSize.Width - k_LengthFromSideBoarder) / 2;
            r_ButtonOnePlayer.Height = m_ButtonHight;
            r_ButtonOnePlayer.Click += buttonOnePlayer_Click;
        }

        private void initTwoPlayerButton()
        {
            r_ButtonTwoPlayer.Text = "Play against your friend";
            r_ButtonTwoPlayer.Location = new Point((2 * k_LengthFromSideBoarder) + r_ButtonOnePlayer.Width, ClientSize.Height / 2);
            r_ButtonTwoPlayer.Width = (r_ButtonEncreaseSize.Width - k_LengthFromSideBoarder) / 2;
            r_ButtonTwoPlayer.Height = m_ButtonHight;
            r_ButtonTwoPlayer.Click += buttonTwoPlayers_Click;
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
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonTwoPlayers_Click(object i_Sender, EventArgs i_E)
        {
            m_NumOfPlayers = eNumOfPlayers.TwoPlayers;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}