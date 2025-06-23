using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Controller;
using UnicomTicManagement.Database;
using UnicomTicManagement.Model;

namespace UnicomTicManagement.View
{
    public partial class RegisterForm : Form
    {

        private StudentController studentController;
        private UserController userController;
        private int selectedStudentId = -1;
        private int selectedUserId = -1;
        public RegisterForm()
        {
            InitializeComponent();
            studentController = new StudentController();
            userController = new UserController();
            LoadCourses();
            LoadStudents();

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadStudents();
        }

        

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtnumberl.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            var student = new Student
            {
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = "Student",
                StudentNumber =txtnumberl.Text.Trim(),
                CourseId = Convert.ToInt32(cmbCourse.SelectedValue)
            };

            string result = userController.RegisterUser(student);
            MessageBox.Show(result);
            LoadStudents();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == -1 || selectedUserId == -1)
            {
                MessageBox.Show("Please select a student to delete.");
                return;
            }

            var result = studentController.DeleteStudent(selectedStudentId, selectedUserId);
            MessageBox.Show(result);
            LoadStudents();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == -1 || selectedUserId == -1)
            {
                MessageBox.Show("Please select a student to update.");
                return;
            }

            var student = new Student
            {
                StudentId = selectedStudentId,
                UserId = selectedUserId,
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = "Student",
                StudentNumber = txtnumberl.Text.Trim(),
                CourseId = Convert.ToInt32(cmbCourse.SelectedValue)
            };

            string result = studentController.UpdateStudent(student);
            MessageBox.Show(result);
            LoadStudents();
        }






        private void ClearInputs()
        {
            txtName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtnumberl.Text = "";
            cmbCourse.SelectedIndex = 0;
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void LoadStudents()
        {
            dgvStudents.DataSource = studentController.GetAllStudents();
            dgvStudents.ClearSelection();
            ClearInputs();
            selectedStudentId = -1;
            selectedUserId = -1;
        }


        private void LoadCourses()
        {
            using (var conn = DbCon.GetConnection())
            {
                var cmd = new SQLiteCommand("SELECT CourseId, CourseName FROM Courses", conn);
                var reader = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);

                cmbCourse.DataSource = dt;
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseId";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
              if (dgvStudents.SelectedRows.Count > 0)
        {
            var row = dgvStudents.SelectedRows[0];
            var student = row.DataBoundItem as Student;

            if (student != null)
            {
                selectedStudentId = student.StudentId;
                selectedUserId = student.UserId;
                txtName.Text = student.Name;
                txtUsername.Text = student.Username;
                txtPassword.Text = student.Password;
                txtEmail.Text = student.Email;
                txtnumberl.Text = student.StudentNumber;
                cmbCourse.SelectedValue = student.CourseId;
            }
        }
        else
        {
            ClearInputs();
            selectedStudentId = -1;
            selectedUserId = -1;
        }
        }
    }
}
