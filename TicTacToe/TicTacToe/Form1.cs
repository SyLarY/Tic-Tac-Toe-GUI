using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool turn = true; //if true then display X, else display O
        int turnCount = 0; //To keep track of number of button clicked
        List<int> xValue = new List<int>();
        List<int> oValue = new List<int>();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This application is written by Yayun (Kim) Yang","About",MessageBoxButtons.OK);

        }

        //Method to draw X or O
        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "") //Only button with no text will be draw, this is to prevent over draw for clicked button
            {
                if (turn)
                {
                    button.Text = "X";
                    button.ForeColor = Color.Blue;
                    xValue.Add(button.TabIndex); //X is drawed, and the tabIndex of clicked button will be added to a int list called xValue.
                }
                else
                {
                    button.Text = "O";
                    button.ForeColor = Color.Red;
                    oValue.Add(button.TabIndex);//O is drawed, and the tabIndex of clicked button will be added to a int list called oValue.
                }
                turnCount++; 
                turn = !turn;
                checkForWinner(); 
            }
        }

        //Method to check if there is a winner for the game
        private void checkForWinner()
        {
            bool winner = false;
            string winnerChar = ""; //it is used for annoucing which one wins the game
            List<int> loopList = new List<int>((turn ? oValue : xValue)); //new int list equals to oValue or xValue list based on what's drawed.

            /* Go through the list to check if there's any 3 number that add up to 15
             * The reason of the sum is 15 is because each button is assigned to a different TabIndex
             * And the way those TabIndex is arranged let the TabIndex of any 3 buttons in a row, colomn or diagonal add up to 15
             * Therefore, if there's any 3 number in the list which contains TabIndex of regarding string Char ( X or O) sum up to 15,
             * That relative string Char is the winner. 
             */
            for (int i = 0; i < loopList.Count - 2; i++)
            {
                int num1 = loopList[i];
                for (int j = i+1; j < loopList.Count - 1; j++)
                {
                    int num2 = loopList[j];
                    for (int k = j+1; k < loopList.Count; k++)
                    {
                        int num3 = loopList[k];
                        if ((num1 + num2 + num3) == 15) 
                        {
                            winner = true;
                            winnerChar = turn ? "O" : "X";
                        }
                    }
                }
            }

            if (winner)
            {
                MessageBox.Show(winnerChar + " won the game!", "Yay!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (Control c in Controls)
                {
                    if (c.Text == "")
                        c.Enabled = false;
                }
            }
            else if (turnCount == 9)
            {
                MessageBox.Show("Tie! No one win the game!", ": (", MessageBoxButtons.OK);
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Control c in Controls)
            {
                if(c.GetType() == typeof(Button))
                {
                    c.Text = "";
                    xValue.Clear();
                    oValue.Clear();
                    turn = true;
                    turnCount = 0;
                    c.Enabled = true;
                }
            }
        }
    }
}
