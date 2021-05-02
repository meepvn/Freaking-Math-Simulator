﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
namespace FreakingMath
{
    public partial class FormGame : Form
    {
        int score = -1,time;
        Random rd = new Random();
        void openMenu() {
            formMain f = new formMain();
            f.Show();
            this.Close();
        }
        void createEquation() {
            time = setTime();
            lbTime.Text = "Time: "+ time.ToString();
            bool correct = rd.Next(0, 2) == 1 ? true : false;
            int operation = rd.Next(0, 2);
            lbOperation.Text = operation == 1 ? "+" : "-";
            int num1, num2;
            if (score < 10)
            {
                num1 = rd.Next(1, 10);
                num2 = rd.Next(1, 10);
                if (num1 == num2) num2 = rd.Next(1, 10);
            }
            else if (score >= 10 && score <= 20)
            {
                num1 = rd.Next(10, 21);
                num2 = rd.Next(10, 21);
                if (num1 == num2) num2 = rd.Next(1, 21);
            }
            else {
                num1 = rd.Next(20, 31);
                num2 = rd.Next(10, 31);
                if (num1 == num2) num2 = rd.Next(10, 31);
            }
            if (num1 < num2&&operation==0)
            {
                int temp = num1;
                num1 = num2;
                num2 = temp;
            }
            lbNum1.Text = num1.ToString();
            lbNum2.Text = num2.ToString();
            lbResult.Text = operation == 1 ? (num1 + num2).ToString() : (num1 - num2).ToString();
            if (!correct)
                lbResult.Text = (int.Parse(lbResult.Text) + rd.Next(1, 3)).ToString();
        }
        int setTime() {
            return score <= 10 ? 3 : 2;
        }
        int calculateAnswer() {
            char ch = lbOperation.Text[0];
            if (ch == '+') return int.Parse(lbNum1.Text) + int.Parse(lbNum2.Text);
            return int.Parse(lbNum1.Text) - int.Parse(lbNum2.Text);
        }
        void showScore() {
            txbScore.Text = "Score: " + (++score);
        }
        bool isHighScore() {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2K5RFTQ;Initial Catalog=FreakingMath;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select min(score) from HighScore", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int lowestScore = int.Parse(reader[0].ToString());
            connection.Close();
            return score > lowestScore ? true : false;
        }
        public FormGame()
        {
            InitializeComponent();
            showScore();
            createEquation();
            lbEqual.Text = "=";
        }
        public void gameOver() {
            MessageBox.Show("Game over\nScore: "+score,"Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            if (isHighScore())
            {
                DialogResult DR = MessageBox.Show("Save your high score ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DR == DialogResult.Yes)
                {
                    FormAddHs fAddHS = new FormAddHs();
                    fAddHS.lbHS.Text = score.ToString();
                    fAddHS.Show();
                    this.Close();
                    return;
                }
                openMenu();
            }
            else openMenu();
        }
        private void btnCorrect_Click(object sender, EventArgs e)
        {
            timer1.Start();
            int result = calculateAnswer();
            if (result != int.Parse(lbResult.Text)) {
                timer1.Stop();
                gameOver();
            }
            showScore();
            createEquation();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time == 0) {
                timer1.Stop();
                gameOver();
            }
            time--;
            lbTime.Text = "Time: " + time.ToString();
        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnIncorrect_Click(object sender, EventArgs e)
        {
            timer1.Start();
            int result = calculateAnswer();
            if (result == int.Parse(lbResult.Text)) {
                timer1.Stop();
                gameOver();
            }
            showScore();
            createEquation();
        }
    }
}
