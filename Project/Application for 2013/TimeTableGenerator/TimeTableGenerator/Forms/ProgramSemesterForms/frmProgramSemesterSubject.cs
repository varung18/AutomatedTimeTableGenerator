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

namespace TImeTableGenerator.Forms.ProgramSemesterForms
{
    public partial class frmProgramSemesterSubject : Form
    {
        public frmProgramSemesterSubject()
        {
            InitializeComponent();
        }

        public void FIllGrid (string searchvalue)
        {
            try
            {
                string query = string.Empty;
                if (string.IsNullOrEmpty(searchvalue.Trim()))
                {
                    query = "Select [ProgramSemesterSubjectID] [ID], [ProgramID], [Program], ProgramSemesterID, Title [Semester], LectureSubjectID, SemesterSubjectTitle [Subject], " +
                            "Capacity, IsSubjectActive[Status] from v_AllSemesterSubjects where [ProgramSemesterIsActive] = 1 and[ProgramIsActive] = 1 and[SemesterIsActive] = 1 " +
                                " order by ProgramSemesterID";
                }
                else
                {
                    query = "Select [ProgramSemesterSubjectID] [ID], [ProgramID], [Program], ProgramSemesterID, Title [Semester], LectureSubjectID, SemesterSubjectTitle [Subject], " +
                            "Capacity, IsSubjectActive[Status] from v_AllSemesterSubjects where [ProgramSemesterIsActive] = 1 and[ProgramIsActive] = 1 and[SemesterIsActive] = 1 " +
                                " and (Program + ' ' + Title + ' ' + SemesterSubjectTitle) like '%" + searchvalue + "%' order by ProgramSemesterID";
                }

                DataTable programsemesterlist = DatabaseLayer.Retrieve(query);
                dgvTeacherSubjects.DataSource = programsemesterlist;
                if (dgvTeacherSubjects.Rows.Count > 0)
                {
                    dgvTeacherSubjects.Columns[0].Visible = false; // ProgramSemesterSubjectID
                    dgvTeacherSubjects.Columns[1].Visible = false; // ProgramID
                    dgvTeacherSubjects.Columns[2].Width = 120; // Program
                    dgvTeacherSubjects.Columns[3].Visible = false; // ProgramSemesterID
                    dgvTeacherSubjects.Columns[4].Width = 150; // Title
                    dgvTeacherSubjects.Columns[5].Visible = false; // LectureSubjectID
                    dgvTeacherSubjects.Columns[6].Width = 300; // SemesterSubjectTitle
                    dgvTeacherSubjects.Columns[7].Width = 80; // Capacity
                    dgvTeacherSubjects.Columns[8].Width = 80; // IsSubjectActive
                    dgvTeacherSubjects.ClearSelection();
                }
            }
            catch 
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        private void frmProgramSemesterSubject_Load(object sender, EventArgs e)
        {
            ComboHelper.AllProgramSemesters(cmbSemesters);
            ComboHelper.AllTeacherSubjects(cmbSubjects);
            FIllGrid(string.Empty);
        }

        private void cmbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTitle.Text = cmbSubjects.SelectedIndex == 0 ? string.Empty : cmbSubjects.Text;
        }

        private void FormClear()
        {
            txtTitle.Clear();
            cmbSubjects.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FormClear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FIllGrid(txtSearch.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtTitle.Text.Trim().Length == 0)
            {
                ep.SetError(txtTitle, "Please Enter Semester Subject Title");
                txtTitle.Focus();
                txtTitle.SelectAll();
                return;
            }

            if (cmbSemesters.SelectedIndex == 0)
            {
                ep.SetError(cmbSubjects, "Please Select Semester Accordingly!");
                cmbSemesters.Focus();
                return;
            }
            if (cmbSubjects.SelectedIndex == 0)
            {
                ep.SetError(cmbSubjects, "Please Select Subject Accordingly!");
                cmbSubjects.Focus();
                return;
            }

            string checkquery = "select * from ProgramSemesterSubjectTable where ProgramSemesterID = '" + cmbSemesters.SelectedValue + "' and LectureSubjectID = '" + cmbSubjects.SelectedValue + " '";
            DataTable dt = DatabaseLayer.Retrieve(checkquery);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ep.SetError(cmbSubjects, "Already Registered!");
                    cmbSubjects.Focus();
                    return;
                }
            }

            string insertquery = string.Format("insert into ProgramSemesterSubjectTable (SemesterSubjectTitle, ProgramSemesterID,LectureSubjectID) values ('{0}', '{1}', '{2}')",
                    txtTitle.Text.Trim(), cmbSemesters.SelectedValue, cmbSubjects.SelectedValue);

            bool result = DatabaseLayer.Insert(insertquery);

            if (result == true)
            {
                MessageBox.Show("Selected Subject has ben assigned Successfully");
                FIllGrid(string.Empty);
                return;
            }
            else
            {
                MessageBox.Show("Please Provide Correct Details and then try again");
            }
        }

        private void cmsedit_Click(object sender, EventArgs e)
        {
            if (dgvTeacherSubjects != null)
            {
                if (dgvTeacherSubjects.Rows.Count > 0)
                {
                    if (dgvTeacherSubjects.SelectedRows.Count == 1)
                    {
                        if (MessageBox.Show("Are you sure you want to update the Selected Record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            bool existstatus = Convert.ToBoolean(dgvTeacherSubjects.CurrentRow.Cells[8].Value);
                            int semestersubjectid = Convert.ToInt32(dgvTeacherSubjects.CurrentRow.Cells[0].Value);
                            bool status = false;

                            if (existstatus == true)
                                status = false;
                            else
                                status = true;

                            string updatequery = string.Format("update ProgramSemesterSubjectTable set IsSubjectActive = '{0}' where ProgramSemesterSubjectID = '{1}'",
                                    status,  semestersubjectid);

                            bool result = DatabaseLayer.Update(updatequery);

                            if (result == true)
                            {
                                MessageBox.Show("Status Changed Successfully");
                                FIllGrid(string.Empty);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Some Unexpected Issues have been occured. Please Contact Administrator or Try Again");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select 1 Record");
                    }
                }
                else
                {
                    MessageBox.Show("List is Empty. Row Count is 0");
                }
            } else
            {
                MessageBox.Show("List is Empty");
            }
        }
    }
}
