using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project21
{
    public partial class Graph : Form
    {
        public List<string[]> graphdata { get; set; } = new List<string[]>();

        public Graph()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new object[] {
            "pulse",
            "rpm",
            "kmh",
            "distance",
            "energy"});
        }

        public void updatechart()
        {
            comboBox1.SelectedIndex = 1;
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            foreach (var VARIABLE in graphdata)
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "pulse":
                        chart1.Series[0].Points.Add(Int32.Parse(VARIABLE[3]));
                        break;
                    case "rpm":
                        chart1.Series[0].Points.Add(Int32.Parse(VARIABLE[4]));
                        break;
                    case "kmh":
                        chart1.Series[0].Points.Add(Int32.Parse(VARIABLE[5]));
                        break;
                    case "distance":
                        chart1.Series[0].Points.Add(Int32.Parse(VARIABLE[6]));
                        break;
                    case "energy":
                        chart1.Series[0].Points.Add(Int32.Parse(VARIABLE[8]));
                        break;

                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                graphdata.Clear();
                this.Hide();
                e.Cancel = true;
            }
        }
    }
}

