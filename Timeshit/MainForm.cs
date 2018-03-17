using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timeshit
{
    public partial class MainForm : Form
    {
        public int Interval
        {
            get
            {
                int minutes;
                if (Int32.TryParse(textBox2.Text, out minutes))
                    return minutes;

                return 30;
            }
        }

        public string LogFile
        {
            get { return textBox1.Text; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Settings.Instance.LogFile;
            textBox2.Text = Settings.Instance.Interval.ToString();
        }
    }
}