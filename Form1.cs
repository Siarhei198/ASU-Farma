using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void типыСИToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 tt = new Form2();
            tt.Owner = this;
            tt.ShowDialog();
        }

        private void журналToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 tt = new Form3();
            tt.Owner = this;
            tt.ShowDialog();
        }

        private void производителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 tt = new Form4();
            tt.Owner = this;
            tt.ShowDialog();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 tt = new Form5();
            tt.Owner = this;
            tt.ShowDialog();
        }

        private void статсусыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 tt = new Form6();
            tt.Owner = this;
            tt.ShowDialog();
        }
    }
}
