using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeClient
{
    public partial class InitForm : Form
    {
        public InitForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ip = this.ip.Text;
            int port = int.Parse(this.port.Text);
            Form1 form = new Form1(ip, port);
            form.Show();
            this.Hide();
        }
    }
}
