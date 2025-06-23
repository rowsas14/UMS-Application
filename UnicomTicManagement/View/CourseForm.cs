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
    public partial class CourseForm : Form
    {
        private CourseController controller;
        private int selectedCourseId = -1;

        public CourseForm()
        {
            InitializeComponent();
            controller = new CourseController();
            LoadCourses();
        }

        private void label1_Click(object sender, EventArgs e)

        {
            Application.Exit();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadCourses()
        {
            List<Course> courses = controller.GetAllCourses();
            dgvCourse.DataSource = courses;
            dgvCourse.ClearSelection();
            ClearInputs();
            selectedCourseId = -1;
        }

        private void dgvCourse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btncourse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textCourse.Text))
            {
                MessageBox.Show("Please enter course name.");
                return;
            }

            Course course = new Course
            {
                CourseName = textCourse.Text.Trim()

            };

            string result = controller.AddCourse(course);
            MessageBox.Show(result);
            LoadCourses();
        }

        private void ClearInputs()
        {
            textCourse.Text = "";
        }

        private void dgvCourse_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourse.SelectedRows.Count > 0)
            {
                var row = dgvCourse.SelectedRows[0];
                Course course = row.DataBoundItem as Course;
                if (course != null)
                {
                    selectedCourseId = course.CourseId;
                    textCourse.Text = course.CourseName;
                }
            }
            else
            {
                ClearInputs();
                selectedCourseId = -1;
            }

        }
    }
}
