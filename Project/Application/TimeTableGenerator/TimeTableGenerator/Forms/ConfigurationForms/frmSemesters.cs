using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TImeTableGenerator.Forms.ConfigurationForms
{
    public partial class frmSemesters : Form
    {
        public frmSemesters()
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
                    query = "select SemesterID [ID], SemesterName [Semester], IsActive [Status] from SemesterTable";
                }
                else
                {
                    query = "select SemesterID [ID], SemesterName [Semester], IsActive [Status] from SemesterTable where SemesterName like '%" + searchvalue.Trim() + "%'";
                }

                DataTable semesterlist = DatabaseLayer.Retrieve(query);
                dgvSemester.DataSource = semesterlist;
                if (dgvSemester.Rows.Count > 0)
                {
                    dgvSemester.Columns[0].Width = 80;
                    dgvSemester.Columns[1].Width = 150;
                    dgvSemester.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
           }
            catch
               {
                    MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
               }
        }

        private void frmSemesters_Load(object sender, EventArgs e)
        {
            FillGrid(string.Empty);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid(txtSearch.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtSemesterName.Text.Length < 9)
            {
                ep.SetError(txtSemesterName, "Please Enter Semester Name Correctly");
                txtSemesterName.Focus();
                txtSemesterName.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from SemesterTable where SemesterName = '" + txtSemesterName.Text.Trim() + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtSemesterName, "Already Exist!");
                    txtSemesterName.Focus();
                    txtSemesterName.SelectAll();
                    return;
                }
            }

            string insertquery = string.Format("insert into SemesterTable(SemesterName, IsActive) values ('{0}','{1}')",
                                txtSemesterName.Text.Trim(), chkStatus.Checked);
            bool result = DatabaseLayer.Insert(insertquery);
            if (result == true)
            {
                MessageBox.Show("Saved Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Semester Details. Then Try Again.");
            }
        }

        public void ClearForm()
        {
            txtSemesterName.Clear();
            chkStatus.Checked = false;
        }

        public void EnableComponents()
        {
            dgvSemester.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvSemester.Enabled = true;
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
            if (dgvSemester != null)
            {
                if (dgvSemester.Rows.Count > 0)
                {
                    if (dgvSemester.SelectedRows.Count == 1)
                    {
                        txtSemesterName.Text = Convert.ToString(dgvSemester.CurrentRow.Cells[1].Value);
                        chkStatus.Checked = Convert.ToBoolean(dgvSemester.CurrentRow.Cells[2].Value);
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
            if (txtSemesterName.Text.Length < 9)
            {
                ep.SetError(txtSemesterName, "Please Enter Semester Name Correctly");
                txtSemesterName.Focus();
                txtSemesterName.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from SemesterTable where SemesterName = '" + txtSemesterName.Text.Trim() + "' and SemesterID != '" + Convert.ToString(dgvSemester.CurrentRow.Cells[0].Value) + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtSemesterName, "Already Exist!");
                    txtSemesterName.Focus();

                    txtSemesterName.SelectAll();
                    return;
                }
            }

            string updatequery = string.Format("update SemesterTable set SemesterName = '{0}', IsActive = '{1}' where SemesterID = '{2}'",
                                txtSemesterName.Text.Trim(), chkStatus.Checked, Convert.ToString(dgvSemester.CurrentRow.Cells[0].Value));
            bool result = DatabaseLayer.Update(updatequery);
            if (result == true)
            {
                MessageBox.Show("Updated Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Semester Details. Then Try Again.");
            }
        }

        private void dgvSession_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
