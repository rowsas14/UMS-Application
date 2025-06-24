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
    public partial class TeacherFormnew : Form
    {

        private TeacherController teacherController = new TeacherController();
        public TeacherFormnew()
        {
            InitializeComponent();
            LoadTeachers();
            LoadCourses();
        }

        private void LoadTeachers()
        {
            dgvTeachers.DataSource = null;
            dgvTeachers.DataSource = teacherController.GetAllTeachers();
            dgvTeachers.ClearSelection();
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            txtDesignation.Clear();
            combocourse.SelectedIndex = -1;
        }
        private void LoadCourses()
        {

            var courseController = new CourseController();
            List<Course> courses = courseController.GetAllCourses();
            combocourse.DataSource = courses;
            combocourse.DisplayMember = "CourseName";
            combocourse.ValueMember = "CourseId";
            combocourse.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TeacherFormnew_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (combocourse.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a course.");
                return;
            }

            var teacher = new Teacher
            {
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Designation = txtDesignation.Text.Trim(),
                CourseId = (int)combocourse.SelectedValue,
                Role = "Teacher"
            };

            MessageBox.Show(teacherController.AddTeacher(teacher));
            LoadTeachers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count > 0 && combocourse.SelectedIndex != -1)
            {
                var row = dgvTeachers.SelectedRows[0];
                int teacherId = Convert.ToInt32(row.Cells["TeacherId"].Value);
                int userId = Convert.ToInt32(row.Cells["UserId"].Value);

                var teacher = new Teacher
                {
                    TeacherId = teacherId,
                    UserId = userId,
                    Name = txtName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Designation = txtDesignation.Text.Trim(),
                    CourseId = (int)combocourse.SelectedValue,
                    Role = "Teacher"
                };

                MessageBox.Show(teacherController.UpdateTeacher(teacher));
                LoadTeachers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count > 0)
            {
                var row = dgvTeachers.SelectedRows[0];
                int teacherId = Convert.ToInt32(row.Cells["TeacherId"].Value);
                int userId = Convert.ToInt32(row.Cells["UserId"].Value);

                MessageBox.Show(teacherController.DeleteTeacher(teacherId, userId));
                LoadTeachers();
            }
        }

        private void dgvTeachers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count > 0)
            {
                var row = dgvTeachers.SelectedRows[0];
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDesignation.Text = row.Cells["Designation"].Value.ToString();

                if (row.Cells["CourseId"].Value != DBNull.Value)
                {
                    combocourse.SelectedValue = Convert.ToInt32(row.Cells["CourseId"].Value);
                }
                else
                {
                    combocourse.SelectedIndex = -1;
                }
            }
        }
    }
    
}
