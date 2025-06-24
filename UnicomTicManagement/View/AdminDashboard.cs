using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicomTicManagement.View
{
    public partial class AdminDashboard : Form
    {

        private Form activeForm = null;

        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            panel2.Dock = DockStyle.Left;
            panel3.Dock = DockStyle.Fill;

        }


        private void LoadFormInPanel(Form form)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = form;

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panel3.Controls.Clear();         
            panel3.Controls.Add(form);       
            form.BringToFront();
            form.Show();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new RegisterForm());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new TeacherFormnew());
        }
        

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new StaffForm());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadFormInPanel(new AdminForm());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LoadFormInPanel(new CourseForm());
        }
    }
    
    
}
