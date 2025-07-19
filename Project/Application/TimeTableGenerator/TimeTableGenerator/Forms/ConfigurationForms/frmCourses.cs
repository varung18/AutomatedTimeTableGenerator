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

namespace TImeTableGenerator.Forms.ConfigurationForms
{
    public partial class frmCourses : Form
    {
        public frmCourses()
        {
            InitializeComponent();
        }

        public void FillGrid(string searchvalue)
        {
            try
            {
                string query = string.Empty;
                if (String.IsNullOrEmpty(searchvalue.Trim()))
                {
                    query = "select CourseID [ID], Title [Subject], Credit" +
                        "Hours, RoomTypeID, TypeName [Type], IsActive from v_AllSubjects";
                }
                else
                {
                    query = "select CourseID [ID], Title [Subject], CreditHours, RoomTypeID, TypeName [Type], IsActive from v_AllSubjects where (Title + ' ' + TypeName) like '%" + searchvalue.Trim() + "%'";
                }

                DataTable Lecturerlist = DatabaseLayer.Retrieve(query);
                dgvCourse.DataSource = Lecturerlist;
                if (dgvCourse.Rows.Count > 0)
                {
                    dgvCourse.Columns[0].Width = 80; // CourseID
                    dgvCourse.Columns[1].Width = 250; // Title
                    dgvCourse.Columns[2].Width = 70; // CrHrs
                    dgvCourse.Columns[3].Visible = false; // RoomTypeID
                    dgvCourse.Columns[4].Width = 250; // TypeName
                    dgvCourse.Columns[5].Width = 80; // IsActive
                }
            }
            catch
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        private void frmCourses_Load(object sender, EventArgs e)
        {
            cmbCreditHours.SelectedIndex = 0;
            ComboHelper.Types(cmbSelectType);
            FillGrid(string.Empty);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid(txtSearch.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtSubjectTitle.Text.Length == 0)
            {
                ep.SetError(txtSubjectTitle, "Please Enter Subject Title Accordingly!");
                txtSubjectTitle.Focus();
                txtSubjectTitle.SelectAll();
                return;
            }

            if (cmbSelectType.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectType, "Please Select Type");
                cmbSelectType.Focus();
                return;
            }


            DataTable checktitle = DatabaseLayer.Retrieve("select * from CourseTable where Title = '" + txtSubjectTitle.Text.Trim() + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtSubjectTitle, "Already Exist!");
                    txtSubjectTitle.Focus();
                    txtSubjectTitle.SelectAll();
                    return;
                }
            }

            string insertquery = string.Format("insert into CourseTable(Title, CreditHours, RoomTypeID, IsActive) values ('{0}','{1}', '{2}', '{3}')",
                                txtSubjectTitle.Text.Trim(), cmbCreditHours.Text, cmbSelectType.SelectedValue, chkStatus.Checked);
            bool result = DatabaseLayer.Insert(insertquery);
            if (result == true)
            { 
                MessageBox.Show("Saved Successfully");
                SaveClearForm();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Details. Then Try Again.");
            }
        }

        public void ClearForm()
        {
            txtSubjectTitle.Clear();
            cmbSelectType.SelectedIndex = 0;
            cmbCreditHours.SelectedIndex = 0;
            chkStatus.Checked = false;
        }

        public void SaveClearForm()
        {
            txtSubjectTitle.Clear();
            chkStatus.Checked = true;
            FillGrid(string.Empty);
        }

        public void EnableComponents()
        {
            dgvCourse.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvCourse.Enabled = true;
            btnClear.Visible = true;
            btnSave.Visible = true;
            btnCancel.Visible = false;
            btnUpdate.Visible = false;
            txtSearch.Enabled = true;
            ClearForm();
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

        private void cmsedit_Click(object sender, EventArgs e)
        {
            if (dgvCourse != null)
            {
                if (dgvCourse.Rows.Count > 0)
                {
                    if (dgvCourse.SelectedRows.Count == 1)
                    {
                        txtSubjectTitle.Text = Convert.ToString(dgvCourse.CurrentRow.Cells[1].Value);
                        cmbSelectType.SelectedValue = Convert.ToString(dgvCourse.CurrentRow.Cells[3].Value); //RoomTypeID
                        cmbCreditHours.Text = Convert.ToString(dgvCourse.CurrentRow.Cells[2].Value); //Crhrs
                        chkStatus.Checked = Convert.ToBoolean(dgvCourse.CurrentRow.Cells[5].Value);
                        EnableComponents();
                    }
                    else
                    {
                        MessageBox.Show("Please Select One Record!");
                    }
                }
                else
                {
                    MessageBox.Show("List is Empty!");
                }
            }
            else
            {
                MessageBox.Show("List is Empty!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtSubjectTitle.Text.Length == 0)
            {
                ep.SetError(txtSubjectTitle, "Please Enter Subject Title Accordingly!");
                txtSubjectTitle.Focus();
                txtSubjectTitle.SelectAll();
                return;
            }

            if (cmbSelectType.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectType, "Please Select Type");
                cmbSelectType.Focus();
                return;
            }


            DataTable checktitle = DatabaseLayer.Retrieve("select * from CourseTable where Title = '" + txtSubjectTitle.Text.Trim() + "' and CourseID != '" + Convert.ToString(dgvCourse.CurrentRow.Cells[0].Value)+ "' ");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtSubjectTitle, "Already Exist!");
                    txtSubjectTitle.Focus();
                    txtSubjectTitle.SelectAll();
                    return;
                }
            }

            string updatequery = string.Format("update CourseTable  set  Title = '{0}', CreditHours = '{1}', RoomTypeID = '{2}', IsActive = '{3}' where CourseID = '{4}'",
                                txtSubjectTitle.Text.Trim(), cmbCreditHours.Text, cmbSelectType.SelectedValue, chkStatus.Checked, Convert.ToString(dgvCourse.CurrentRow.Cells[0].Value));
            bool result = DatabaseLayer.Update(updatequery);
            if (result == true)
            {
                MessageBox.Show("Updated Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Subject Details. Then Try Again.");
            }
        }
    }
}
