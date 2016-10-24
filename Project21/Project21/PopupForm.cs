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
    public partial class PopupForm : Form
    {
        public PopupForm(string labelText, Form formParent)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(  formParent.Location.X + formParent.Width / 2 - this.Width / 2, 
                                        formParent.Location.Y + formParent.Height / 2 - this.Height / 2);
            label1.Text = labelText;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
