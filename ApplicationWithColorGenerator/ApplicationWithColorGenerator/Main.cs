using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ApplicationWithColor
{
    public partial class Main : Form
    {
        private static string colorKey = "${NGJYRA}";
        private Regex regex;

        public Main()
        {
            regex = new Regex(@"^#(?:[0-9a-fA-F]{6}){1,2}$");
            InitializeComponent();
        }

        private void btnGjenero_Click(object sender, EventArgs e)
        {
            if (!regex.Match(txtColor.Text).Success)
            {
                MessageBox.Show("Ngjyra ne HEX jo valide.\nFormati: '#000000' (pa thojza)");
                return;
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] appWithColorBytes;
                Assembly a = Assembly.GetExecutingAssembly();
                using (Stream resFilestream = a.GetManifestResourceStream("ApplicationWithColor.Resources.app-with-color.exe"))
                {
                    appWithColorBytes = new byte[resFilestream.Length];
                    resFilestream.Read(appWithColorBytes, 0, appWithColorBytes.Length);
                }
                // Check length just to make sure everything is OK
                if (appWithColorBytes.Length == 0)
                {
                    MessageBox.Show("PROBLEM !");
                    return;
                }

                string appWithColorString = Utils.ByteArrayToString(appWithColorBytes);
                string colorToReplace = txtColor.Text + "  ";
                string toFindString = Utils.ByteArrayToString(Utils.GetBytes(colorKey));
                string toReplaceString = Utils.ByteArrayToString(Utils.GetBytes(colorToReplace));
                string newAppWithColorString = appWithColorString.Replace(toFindString, toReplaceString);

                string path = saveFileDialog.FileName;
                File.WriteAllBytes(path, Utils.StringToByteArray(newAppWithColorString));

                txtColor.Text = "";
                MessageBox.Show("Aplikacioni u gjenerua me sukses!");
            }
        }

    }
}
