using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeClient
{
    public partial class Form1 : Form
    {
        private int userID;
        private String symbol;

        private Client client;
        private Button[] buttons;

        public Form1(string ip, int port)
        {
            InitializeComponent();
            InitClient(ip, port);
            InitButtons();
        }

        /*--------- Buttons ---------*/
        private void startButton_Click(object sender, EventArgs e)
        {
            if (this.userID == 2)
            {
                this.turnLabel.Text = "Первым ходит соперник";
                WaitTurn();
            }
            else
            {
                this.turnLabel.Text = "Первым ходите Вы";
            }
            startButton.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = symbol;
            SendTurn("1");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = symbol;
            SendTurn("2");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Text = symbol;
            SendTurn("3");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = symbol;
            SendTurn("4");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button5.Text = symbol;
            SendTurn("5");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            button6.Text = symbol;
            SendTurn("6");
        }
        private void button7_Click(object sender, EventArgs e)
        {
            button7.Text = symbol;
            SendTurn("7");
        }
        private void button8_Click(object sender, EventArgs e)
        {
            button8.Text = symbol;
            SendTurn("8");
        }
        private void button9_Click(object sender, EventArgs e)
        {
            button9.Text = symbol;
            SendTurn("9");
        }

        /*--------- Private ---------*/

        private void InitClient(string ip, int port)
        {
            this.client = new Client(ip, port);
            this.userID = client.getMyID();
            if (this.userID == 2)
            {
                this.symbol = "O";
            } else
            {
                this.symbol = "X";
            }
        }

        private void InitButtons()
        {
            this.buttons = new Button[] 
            { 
                this.button1, this.button2, this.button3,this.button4,this.button5,this.button6,this.button7, this.button8, this.button9
            };
            if (userID == 2)
            {
                foreach (Button btn in buttons)
                    btn.Enabled = false;
            }
        }

        private void SendTurn(String buttonNum) {
            client.sendTurn("send_turn" + " " + userID + " " + buttonNum);

            string isOver = gameIsOver();
            if (isOver == null)
            {
                int fillBtns = 0;
                foreach (Button btn in buttons)
                {
                    btn.Enabled = true;
                    if (btn.Text != "") fillBtns++;
                }
                if (fillBtns == 9)
                {
                    this.turnLabel.Text = "Ничья";
                    foreach (Button btn in buttons)
                        btn.Enabled = false;
                    return;
                }
                else this.turnLabel.Text = "";
                WaitTurn();
            } else
            {
                if (isOver == symbol) turnLabel.Text = "Вы победили";
                foreach (Button btn in buttons)
                    btn.Enabled = false;
            }  
        }

        public void WaitTurn()
        {
            int numChangedBtn = client.waitTurn(userID);
            if (symbol.Equals("O")) buttons[numChangedBtn - 1].Text = "X";
            else buttons[numChangedBtn - 1].Text = "O";

            string isOver = gameIsOver();
            if (isOver == null)
            {
                int fillBtns = 0;
                foreach (Button btn in buttons)
                {
                    if (btn.Text == "") btn.Enabled = true;
                    if (btn.Text != "") fillBtns++;
                }

                if (fillBtns == 9)
                {
                    this.turnLabel.Text = "Ничья";
                    foreach (Button btn in buttons)
                        btn.Enabled = false;
                    return;
                }
                else this.turnLabel.Text = "";
            }
            else
            {
                if (isOver == symbol) turnLabel.Text = "Вы победили";
                else turnLabel.Text = "Вы проиграли";
                foreach (Button btn in buttons)
                    btn.Enabled = false;
            }
        }

        private string gameIsOver()
        {
            // Horizontal
            if (button1.Text == button2.Text && button2.Text == button3.Text && button1.Text != "") return button1.Text;
            if (button4.Text == button5.Text && button4.Text == button6.Text && button4.Text != "") return button4.Text;
            if (button7.Text == button8.Text && button7.Text == button9.Text && button7.Text != "") return button7.Text;

            // Vertical
            if (button1.Text == button4.Text && button1.Text == button7.Text && button1.Text != "") return button1.Text;
            if (button2.Text == button5.Text && button2.Text == button8.Text && button2.Text != "") return button2.Text;
            if (button3.Text == button6.Text && button3.Text == button9.Text && button3.Text != "") return button3.Text;

            // Diagonal
            if (button1.Text == button5.Text && button1.Text == button9.Text && button1.Text != "") return button1.Text;
            if (button3.Text == button5.Text && button3.Text == button7.Text && button3.Text != "") return button3.Text;

            // Not over
            return null;

        }

    }
}
