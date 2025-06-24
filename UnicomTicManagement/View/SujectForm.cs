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
    public partial class SujectForm : Form


    {
        private SubjectController subjectController = new SubjectController();
        private CourseController courseController = new CourseController();
        private int selectedSubjectId = -1;

        public SujectForm()
        {
            InitializeComponent();
            LoadCourses();
            LoadSubjects();
        }
        private void LoadCourses()
        {
            var courses = courseController.GetAllCourses();
            cmbCourses.DataSource = courses;
            cmbCourses.DisplayMember = "CourseName";
            cmbCourses.ValueMember = "CourseId";
        }

        private void LoadSubjects()
        {
            dgvSubjects.DataSource = null;
            dgvSubjects.DataSource = subjectController.GetAllSubjects();
            dgvSubjects.ClearSelection();
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtSuject.Clear();
            selectedSubjectId = -1;
            cmbCourses.SelectedIndex = 0;
        }


        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SujectForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var subject = new Subject
            {
                SubjectName = txtSuject.Text.Trim(),
                CourseId = Convert.ToInt32(cmbCourses.SelectedValue)
            };

            MessageBox.Show(subjectController.AddSubject(subject));
            LoadSubjects();
            ClearInputs();
        }

        private void dgvSubjects_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                var row = dgvSubjects.SelectedRows[0];
                selectedSubjectId = Convert.ToInt32(row.Cells["SubjectId"].Value);
                txtSuject.Text = row.Cells["SubjectName"].Value.ToString();

                
                int courseId = Convert.ToInt32(row.Cells["CourseId"].Value);
                cmbCourses.SelectedValue = courseId;
            }
        }
    }
}
