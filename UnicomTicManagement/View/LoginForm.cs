using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Controller;

namespace UnicomTicManagement.View
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var controller = new LoginController();

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text; 
            var result = controller.Login(username, password);

            if (result.Success)
            {
                MessageBox.Show($"Welcome! Role: {result.Role}");
        
                if (result.Role == "Admin")
                {
                    AdminDashboard dashboard = new AdminDashboard();
                    dashboard.Show();
                    this.Hide();
                }
                else if (result.Role == "Student")
                {
                   
                }
               
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}
