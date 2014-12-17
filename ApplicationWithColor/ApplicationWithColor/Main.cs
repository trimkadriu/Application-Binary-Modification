using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationWithColor
{
    public partial class Main : Form
    {
        private static string color = "${NGJYRA}";

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!color.Contains("#"))
                MessageBox.Show("Ngjyra nuk eshte e definuar.");
            else
                this.BackColor = ColorTranslator.FromHtml(color.TrimEnd());
        }
    }
}
