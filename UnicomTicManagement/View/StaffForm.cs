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
    public partial class StaffForm : Form
    {
        private StaffController staffController = new StaffController();

        public StaffForm()
        {
            InitializeComponent();
            LoadStaffs();
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {

        }


        private void LoadStaffs()
        {
            dgvStaffs.DataSource = null;
            dgvStaffs.DataSource = staffController.GetAllStaff();
            dgvStaffs.ClearSelection();
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

        private void button2_Click(object sender, EventArgs e)
        {

            if (dgvStaffs.SelectedRows.Count > 0)
            {
                var row = dgvStaffs.SelectedRows[0];
                int staffId = Convert.ToInt32(row.Cells["StaffId"].Value);
                int userId = Convert.ToInt32(row.Cells["UserId"].Value);

                MessageBox.Show(staffController.DeleteStaff(staffId, userId));
                LoadStaffs();
            }

        }

        private void txtDesignation_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var staff = new Staff
            {
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Designation = txtDesignation.Text.Trim(),
                Role = "Staff"
            };

            MessageBox.Show(staffController.AddStaff(staff));
            LoadStaffs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStaffs.SelectedRows.Count > 0)
            {
                var row = dgvStaffs.SelectedRows[0];
                int staffId = Convert.ToInt32(row.Cells["StaffId"].Value);
                int userId = Convert.ToInt32(row.Cells["UserId"].Value);

                var staff = new Staff
                {
                    StaffId = staffId,
                    UserId = userId,
                    Name = txtName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Designation = txtDesignation.Text.Trim(),
                    Role = "Staff"
                };

                MessageBox.Show(staffController.UpdateStaff(staff));
                LoadStaffs();
            }
        }

        private void dgvStaffs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStaffs.SelectedRows.Count > 0)
            {
                var row = dgvStaffs.SelectedRows[0];
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDesignation.Text = row.Cells["Designation"].Value.ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}
