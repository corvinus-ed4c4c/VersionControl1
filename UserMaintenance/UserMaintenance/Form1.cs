using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource.LastName; // label1
            lblFirstName.Text = Resource.FirstName; // label2
            btnAdd.Text = Resource.Add; // button1
        }
    }
}
