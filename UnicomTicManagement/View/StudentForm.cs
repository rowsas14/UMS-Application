using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Controller;

namespace UnicomTicManagement.View
{
    public partial class StudentForm : Form
    {

        private StudentController studentController = new StudentController();

        public StudentForm()
        {
            InitializeComponent();
            LoadStudents();
            LoadCourses();
        }

        private void LoadStudents()
        {
            //dgvStudents.DataSource = null;
            //dgvStudents.DataSource = studentController.GetAllStudents();
            //dgvStudents.ClearSelection();
            //ClearInputs();
        }

        private void LoadCourses()
        {
            
            //var courseController = new CourseController();
            //var courses = courseController.GetAllCourses();
            //cmbCourse.DataSource = courses;
            //cmbCourse.DisplayMember = "CourseName";
            //cmbCourse.ValueMember = "CourseId";
        }


        private void ClearInputs()
        {
            //txtName.Clear();
            //txtUsername.Clear();
            //txtPassword.Clear();
            //txtEmail.Clear();
            //txtStudentNumber.Clear();
            //cmbCourse.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
