using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TImeTableGenerator.SourceCode;

namespace TImeTableGenerator.Forms.LectureSubjectForms
{
    public partial class frmLectureSubjectForms : Form
    {
        public frmLectureSubjectForms()
        {
            InitializeComponent();
        }

        public void FillGrid(string searchvalue)
        {
            try
            {
                string query = string.Empty;
                if (string.IsNullOrEmpty(searchvalue.Trim()))
                {
                    query = "select LectureSubjectID [ID], SubjectTitle [Subject TItle], LectureID, FullName [Lecturer], CourseID, Title [Course], IsActive [Status] from v_AllSubjectsTeachers";
                }
                else
                {
                    query = "select LectureSubjectID [ID], SubjectTitle [Subject TItle], LectureID, FullName [Lecturer], CourseID, Title [Course], IsActive [Status] from v_AllSubjectsTeachers Where (SubjectTitle + ' ' + FullName + ' ' + Title) like '%" + searchvalue.Trim() + "%'";
                }

                DataTable daytimeslotlist = DatabaseLayer.Retrieve(query);
                dgvTeacherSubjects.DataSource = daytimeslotlist;
                if (dgvTeacherSubjects.Rows.Count > 0)
                {
                    dgvTeacherSubjects.Columns[0].Visible = false; // LectureSubjectID
                    dgvTeacherSubjects.Columns[1].Width = 250; // SubjectTitle
                    dgvTeacherSubjects.Columns[2].Visible = false; // LectureID
                    dgvTeacherSubjects.Columns[3].Width = 150; // FullName
                    dgvTeacherSubjects.Columns[4].Visible = false; // CourseID
                    dgvTeacherSubjects.Columns[5].Width = 400; // Title
                    dgvTeacherSubjects.Columns[6].Width = 100; // IsActive

                }
            }
            catch
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        public void ClearForm()
        {
            cmbTeachers.SelectedIndex = 0;
            cmbSubjects.SelectedIndex = 0;
            chkStatus.Checked = true;
        }

        public void EnableComponents()
        {
            dgvTeacherSubjects.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvTeacherSubjects.Enabled = true;
            btnClear.Visible = true;
            btnSave.Visible = true;
            txtSearch.Enabled = true;
            ClearForm();
            FillGrid(string.Empty);
        }

        private void frmLectureSubjectForms_Load(object sender, EventArgs e)
        {
            ComboHelper.AllSubjects(cmbSubjects);
            ComboHelper.AllTeachers(cmbTeachers);
            FillGrid(string.Empty);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisableComponents();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ep.Clear();
                if (cmbTeachers.SelectedIndex == 0)
                {
                    ep.SetError(cmbTeachers, "Please Select Teacher Accordingly!");
                    cmbTeachers.Focus();
                    return;
                }
                if (cmbSubjects.SelectedIndex == 0)
                {
                    ep.SetError(cmbSubjects, "Please Select Subject Accordingly!");
                    cmbSubjects.Focus();
                    return;
                }

                DataTable dt = DatabaseLayer.Retrieve("select * from LectureSubjectTable where LectureID = '" + cmbSubjects.SelectedValue + "' and CourseID = '" + cmbSubjects.SelectedValue + "'");
                if (dt!= null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ep.SetError(cmbSubjects, "Already Registered!");
                        cmbSubjects.Focus();
                        return;
                    }
                }
                string insertquery = string.Format("insert into LectureSubjectTable( SubjectTitle, LectureID, CourseID, IsActive) values ('{0}', '{1}', '{2}', '{3}')",
                        cmbSubjects.Text+" ("+cmbTeachers.Text+") ", cmbTeachers.SelectedValue, cmbSubjects.SelectedValue,  chkStatus.Checked);

                bool result = DatabaseLayer.Insert(insertquery);

                if (result == true)
                {
                    MessageBox.Show("Selected Subject has ben assigned to the Selected Teacher Successfully");
                    DisableComponents();
                    return;
                }
                else
                {
                    MessageBox.Show("Some Unexpected Issues have been occured. Please Contact Administrator or Try Again");
                }
            }
            catch 
            {
                MessageBox.Show("Please Contact with SQL Server Agent Connectivity");
            }
        }

        private void cmsedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTeacherSubjects != null)
                {
                    if (dgvTeacherSubjects.Rows.Count > 0)
                    {
                        if (dgvTeacherSubjects.SelectedRows.Count == 1)
                        {
                            if (MessageBox.Show("Are you sure you want to update the Selected Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string id = Convert.ToString(dgvTeacherSubjects.CurrentRow.Cells[0].Value);
                                bool status = (Convert.ToBoolean(dgvTeacherSubjects.CurrentRow.Cells[6].Value) == true ? false : true);
                                string updatequery = "update LectureSubjectTable set IsActive = '" + status + "' where LectureSubjectID = '" + id + "'";

                                bool result = DatabaseLayer.Update(updatequery);

                                if (result == true)
                                {
                                    MessageBox.Show("Status Change to the Selected Teacher done Successfully");
                                    DisableComponents();
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Some Unexpected Issues have been occured. Please Contact Administrator or Try Again");
                                }
                            }
                        }
                    }
                }
            }
            catch 
            {

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid(txtSearch.Text.Trim());
        }
    }
}
