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
    public partial class Examform : Form
    {
        private ExamControllers examController = new ExamControllers();
        private StudentController studentController = new StudentController();
        private SubjectController subjectController = new SubjectController();
        //private int selectedExamId = -1;
        public Examform()
        {
            InitializeComponent();
            LoadStudents();
            LoadSubjects();
            LoadExams();
        }

        private void LoadStudents()
        {
            var students = studentController.GetAllStudents();
            comboBox2.DataSource = students;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "StudentId";
        }

        private void LoadSubjects()
        {
            var subjects = subjectController.GetAllSubjects();
            comboBox1.DataSource = subjects;
            comboBox1.DisplayMember = "SubjectName";
            comboBox1.ValueMember = "SubjectId";
        }

        private void LoadExams()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = examController.GetExamRecords();
            dataGridView1.ClearSelection();
            ClearInputs();
        }

        private void ClearInputs()
        {
            //txtExamName.Clear();
            //txtExamType.Clear();
            //txtMarks.Clear();
            //dateTimePicker1.Value = DateTime.Now;
            //selectedExamId = -1;
            //cmbStudent.SelectedIndex = 0;
            //cmbSubject.SelectedIndex = 0;
        }


        private void ConfigureDatePicker()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
           
        }

        private void Examform_Load(object sender, EventArgs e)
        {

        }
    }
}
