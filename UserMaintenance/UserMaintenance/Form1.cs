using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource.FullName; // label1
            
            btnAdd.Text = Resource.Add; // button1


            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
             var u = new User()
            {
                FullName = txtLastName.Text,
                
            };
            users.Add(u);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog mentes = new SaveFileDialog();
            mentes.InitialDirectory = Application.StartupPath;
            mentes.Filter = "Comma Seperated Values (*.csv) | *.csv";
            mentes.DefaultExt = "csv";
            mentes.AddExtension = true;

            if (mentes.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(mentes.FileName, false, Encoding.UTF8))
            {
                foreach (var u in users)
                {
                    sw.WriteLine(u.FullName);
                    sw.Write(";");
                    sw.WriteLine(u.ID);
                    sw.Write(";");
                }
            }
        }
    }
}
