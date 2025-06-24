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
using UnicomTicManagement.Model;

namespace UnicomTicManagement.View
{

   
    public partial class AdminForm : Form
    {
        private AdminController adminController = new AdminController();

        public AdminForm()
        {
            InitializeComponent();
            LoadAdmins();

        }

        private void LoadAdmins()
        {
            dgvAdmins.DataSource = null;
            dgvAdmins.DataSource = adminController.GetAllAdmins();
            dgvAdmins.ClearSelection();
            ClearInputs();
        }
        private void ClearInputs()
        {
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            txtDesignation.Clear();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
        }

       

       

        private void btnAdd_Click(object sender, EventArgs e)
        {

            var admin = new Admin
            {
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Designation = txtDesignation.Text.Trim(),
                Role = "Admin"
            };

            MessageBox.Show(adminController.AddAdmin(admin));
            LoadAdmins();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (dgvAdmins.SelectedRows.Count > 0)
            {
                var row = dgvAdmins.SelectedRows[0];
                int adminId = Convert.ToInt32(row.Cells["AdminId"].Value);
                int userId = Convert.ToInt32(row.Cells["UserId"].Value);

                var admin = new Admin
                {
                    AdminId = adminId,
                    UserId = userId,
                    Name = txtName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Designation = txtDesignation.Text.Trim(),
                    Role = "Admin"
                };

                MessageBox.Show(adminController.UpdateAdmin(admin));
                LoadAdmins();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvAdmins.SelectedRows.Count > 0)
            {
                var row = dgvAdmins.SelectedRows[0];
                int adminId = Convert.ToInt32(row.Cells["AdminId"].Value);
                int userId = Convert.ToInt32(row.Cells["UserId"].Value);

                MessageBox.Show(adminController.DeleteAdmin(adminId, userId));
                LoadAdmins();
            }


        }

        private void Name_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvAdmins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtUsename_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDesignation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_Click(object sender, EventArgs e)
        {

        }

        private void dgvAdmins_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvAdmins.SelectedRows.Count > 0)
            {
                var row = dgvAdmins.SelectedRows[0];
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDesignation.Text = row.Cells["Designation"].Value.ToString();
            }

        }


        //private void btnClear_Click(object sender, EventArgs e)
        //{
        //    ClearInputs();
        //    dgvAdmins.ClearSelection();
        //    selectedAdminId = -1;
        //    selectedUserId = -1;
        //}
    }
}
