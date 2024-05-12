using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bank_mangement_system
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

       

        private void PictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_button_Click_1(object sender, EventArgs e)
        {
             DBconfig DB = new DBconfig();
            DB.Open_connection();
            string insertQuery = $"INSERT INTO AdminTbl (AdName, AdPass) VALUES ('{userINP.Text}', '{passINP.Text}')";
            DB.Insert_data(insertQuery);
            DB.Close_connection();
        }



    }
}
