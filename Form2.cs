using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {

        public static int level;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.Equals(comboBox1.Text, "Easy", StringComparison.OrdinalIgnoreCase))
                level = 0;
            else if (string.Equals(comboBox1.Text, "Normal", StringComparison.OrdinalIgnoreCase))
            {
                level = 1;
            }
            else {
                level = 2;
            }
            Form1 f = new Form1();
            f.Show();
            this.Hide();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            HistoryForm history = new HistoryForm();
            history.Show();
            this.Hide();
        }
    }
}
