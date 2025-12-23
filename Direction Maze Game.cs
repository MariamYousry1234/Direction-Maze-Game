using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Direction_Maze_Game
{
    public partial class DirectionMazeGame : Form
    {
        public DirectionMazeGame()
        {
            InitializeComponent();
        }

    
        List<Panel> FixedPanels = new List<Panel>();

        void SaveEachFixedPanel()
        {
            FixedPanels.AddRange(new Panel[] { panel1,panel2, panel3, panel4,
           panel5, panel6,panel7,panel8,panel9,panel10,panel11,
           panel12,panel13, panel14,panel15, panel16, panel17, panel18,
            panel19,panel20,panel21,panel22});
        }

        void IncreaseRoundNumber()
        {
            lblRoundsNumber.Tag = Convert.ToInt32(lblRoundsNumber.Tag) + 1;
            lblRoundsNumber.Text = lblRoundsNumber.Tag.ToString();
        }

        void UpdateRoundTime()
        {
            lblRoundTime.Tag = Convert.ToSingle(lblRoundTime.Tag) - 1;
            lblRoundTime.Text = lblRoundTime.Tag.ToString();

            if (Convert.ToSingle(lblRoundTime.Tag) == 0)
            {
                timer1.Stop();
                MessageBox.Show("Time is up\nTry again");
                IncreaseRoundNumber();
                ResetGame();
            }
        }


        bool IsPlayerHitWall()
        {
            foreach (Panel p in FixedPanels)
            {
                if (pStart.Bounds.IntersectsWith(p.Bounds))
                    return true;
            }

            return false;
        }

        bool IsPlayerWin()
        {
            return pStart.Bounds.IntersectsWith(pEnd.Bounds);
        }

        void ResetGame()
        {
            pStart.Location = new Point(91, 147);
            pStart.BackColor = Color.Blue;
            lblRoundTime.Tag = 30;
            lblRoundTime.Text = "30";
            timer1.Start();
        }


        void EndRound()
        {
            if (IsPlayerHitWall())
            {
                timer1.Stop();
                pStart.BackColor = Color.Red;
                MessageBox.Show("You hit the wall\nTry again");
                IncreaseRoundNumber();
                ResetGame();
                return;
            }

            if (IsPlayerWin())
            {
                timer1.Stop();

                lblScoreNumber.Tag = Convert.ToInt32(lblScoreNumber.Tag) + 1;
                MessageBox.Show($"Perfect! You win this round\nYour Score Number: {lblScoreNumber.Tag.ToString()}");
                lblScoreNumber.Text = lblScoreNumber.Tag.ToString();
                IncreaseRoundNumber();
                ResetGame();
            }
        }


        void Move(Keys key)
        {
            if (key == Keys.Up)
            {
                pStart.Top -= 10;
            }

            if (key == Keys.Down)
            {
                pStart.Top += 10;
            }

            if (key == Keys.Left)
            {
                pStart.Left -= 10;
            }

            if (key == Keys.Right)
            {
                pStart.Left += 10;
            }
            EndRound();
        }


        private void DirectionMazeGame_Load(object sender, EventArgs e)
        {
            SaveEachFixedPanel();
            ResetGame();
        }

        private void DirectionMazeGame_KeyDown(object sender, KeyEventArgs e)
        {
            Move(e.KeyData); 
        }

     
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateRoundTime();
        }
    }
}
