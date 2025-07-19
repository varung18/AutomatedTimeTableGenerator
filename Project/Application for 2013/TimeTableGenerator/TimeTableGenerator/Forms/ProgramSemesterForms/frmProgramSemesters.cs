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

namespace TImeTableGenerator.Forms.ConfigurationForms.ProgramSemesterForms
{
    public partial class frmProgramSemesters : Form
    {
        public frmProgramSemesters()
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
                    query = "select ProgramSemesterID [ID], Title, Capacity, ProgramSemesterIsActive [Status], ProgramID, SemesterID from v_ProgramSemesterActiveList";
                }
                else
                {
                    query = "select ProgramSemesterID [ID], Title, Capacity, ProgramSemesterIsActive [Status], ProgramID, SemesterID from v_ProgramSemesterActiveList where Title like '%" + searchvalue.Trim() + "%'";
                }

                DataTable Lecturerlist = DatabaseLayer.Retrieve(query);
                dgvProgramSemester.DataSource = Lecturerlist;
                if (dgvProgramSemester.Rows.Count > 0)
                {
                    dgvProgramSemester.Columns[0].Width = 80; // ProgramSemesterID
                    dgvProgramSemester.Columns[1].Width = 150; // Title
                    dgvProgramSemester.Columns[2].Width = 100; // Capacity
                    dgvProgramSemester.Columns[3].Width = 100; // ProgramSemesterIsActive
                    dgvProgramSemester.Columns[4].Visible = false; // ProgramID
                    dgvProgramSemester.Columns[5].Visible = false; // SemesterID
                }
            }
            catch
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        private void frmProgramSemesters_Load(object sender, EventArgs e)
        {
            ComboHelper.Semesters(cmbSelectSemester);
            ComboHelper.Programs(cmbSelectProgram);
            ComboHelper.ActiveSessions(cmbSelectSession);
            FillGrid(string.Empty);
        }

        private void cmbSelectProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbSelectProgram.Text.Contains("Select"))
            {
                if (cmbSelectSemester.SelectedIndex > 0)
                {
                    txtProgramSemesterName.Text = cmbSelectProgram.Text + " " + cmbSelectSemester.Text;
                }
            }
        }

        private void cmbSelectSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbSelectSemester.Text.Contains("Select"))
            {
                if (cmbSelectProgram.SelectedIndex > 0)
                {
                    txtProgramSemesterName.Text = cmbSelectProgram.Text + " " + cmbSelectSemester.Text;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid(txtSearch.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtProgramSemesterName.Text.Length == 0 )
            {
                ep.SetError(txtProgramSemesterName, "Please Select Accordingly! Title is empty.");
                txtProgramSemesterName.Focus();
                txtProgramSemesterName.SelectAll();
                return;
            }

            if (cmbSelectProgram.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectProgram, "Please Select the Program");
                cmbSelectProgram.Focus();
                return;
            }

            if (cmbSelectSemester.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectProgram, "Please Select the Semester");
                cmbSelectSemester.Focus();
                return;
            }

            if (cmbSelectSession.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectSession, "Please Select the Session");
                cmbSelectSession.Focus();
                return;
            }


            if (txtCapacity.Text.Trim().Length == 0)
            {
                ep.SetError(txtCapacity, "Please Enter Number of Students Accordingly!");
                txtCapacity.Focus();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from ProgramSemesterTable where ProgramID = '" + cmbSelectProgram.SelectedValue + "' and SemesterID = '" + cmbSelectSemester.SelectedValue + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtProgramSemesterName, "Already Exist!");
                    txtProgramSemesterName.Focus();
                    txtProgramSemesterName.SelectAll();
                    return;
                }
            }

            string insertquery = string.Format("insert into ProgramSemesterTable(Title, ProgramID, SemesterID, IsActive, Capacity, SessionID) values ('{0}','{1}', '{2}', '{3}', '{4}', '{5}')",
                                txtProgramSemesterName.Text.Trim(), cmbSelectProgram.SelectedValue, cmbSelectSemester.SelectedValue, chkStatus.Checked, txtCapacity.Text.Trim(), cmbSelectSession.SelectedValue);

            bool result = DatabaseLayer.Insert(insertquery);
            if (result == true)
            {
                MessageBox.Show("Saved Successfully");
                SaveClearForm();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Lecturer Details. Then Try Again.");
            }
        }

        public void ClearForm()
        {
            txtProgramSemesterName.Clear();
            cmbSelectSemester.SelectedIndex = 0;
            cmbSelectProgram.SelectedIndex = 0;
            chkStatus.Checked = false;
        }

        public void SaveClearForm()
        {
            txtProgramSemesterName.Clear();
            cmbSelectSemester.SelectedIndex = 0;
            chkStatus.Checked = true;
            FillGrid(string.Empty);
        }

        public void EnableComponents()
        {
            dgvProgramSemester.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvProgramSemester.Enabled = true;
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
            if (dgvProgramSemester != null)
            {
                if (dgvProgramSemester.Rows.Count > 0)
                {
                    if (dgvProgramSemester.SelectedRows.Count == 1)
                    {
                        txtProgramSemesterName.Text = Convert.ToString(dgvProgramSemester.CurrentRow.Cells[1].Value);
                        txtCapacity.Text = Convert.ToString(dgvProgramSemester.CurrentRow.Cells[2].Value);
                        cmbSelectProgram.SelectedValue = Convert.ToString(dgvProgramSemester.CurrentRow.Cells[4].Value); //ProgramID
                        cmbSelectSemester.SelectedValue = Convert.ToString(dgvProgramSemester.CurrentRow.Cells[5].Value); //SemesterID
                        chkStatus.Checked = Convert.ToBoolean(dgvProgramSemester.CurrentRow.Cells[3].Value);
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
            if (txtProgramSemesterName.Text.Length == 0)
            {
                ep.SetError(txtProgramSemesterName, "Please Select Accordingly! Title is empty.");
                txtProgramSemesterName.Focus();
                txtProgramSemesterName.SelectAll();
                return;
            }

            if (cmbSelectSemester.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectProgram, "Please Select the Semester");
                cmbSelectSemester.Focus();
                return;
            }

            if (cmbSelectProgram.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectProgram, "Please Select the Program");
                cmbSelectProgram.Focus();
                return;
            }

            if (cmbSelectSemester.SelectedIndex == 0)
            {
                ep.SetError(cmbSelectProgram, "Please Select the Semester");
                cmbSelectSemester.Focus();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from ProgramSemesterTable where ProgramID = '" + cmbSelectProgram.SelectedValue + "' and SemesterID = '" + cmbSelectSemester.SelectedValue + "' and ProgramSemesterID != '" +Convert.ToString(dgvProgramSemester.CurrentRow.Cells[0].Value)+ "' and SessionID = '" + cmbSelectSession.SelectedValue + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtProgramSemesterName, "Already Exist!");
                    txtProgramSemesterName.Focus();
                    txtProgramSemesterName.SelectAll();
                    return;
                }
            }

            string updatequery = string.Format("update ProgramSemesterTable  set  Title = '{0}', ProgramID = '{1}', SemesterID = '{2}', IsActive = '{3}', Capacity = '{4}',SessionID = '{5}' where ProgramSemesterID = '{6}'",
                                txtProgramSemesterName.Text.Trim(), cmbSelectProgram.SelectedValue, cmbSelectSemester.SelectedValue, chkStatus.Checked, txtCapacity.Text.Trim(), cmbSelectSession.SelectedValue, Convert.ToString(dgvProgramSemester.CurrentRow.Cells[0].Value));
            bool result = DatabaseLayer.Update(updatequery);
            if (result == true)
            {
                MessageBox.Show("Updated Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Semester and Program Details. Then Try Again.");
            }
        }
    }
}
