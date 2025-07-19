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
    public partial class frmProgram : Form
    {
        public frmProgram()
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
                    query = "select ProgramID [ID], Name [Program], IsActive [Status] from ProgramTable";
                }
                else
                {
                    query = "select ProgramID [ID], Name [Program], IsActive [Status] from ProgramTable where Name like '%" + searchvalue.Trim() + "%'";
                }

                DataTable programlist = DatabaseLayer.Retrieve(query);
                dgvPrograms.DataSource = programlist;
                if (dgvPrograms.Rows.Count > 0)
                {
                    dgvPrograms.Columns[0].Width = 80;
                    dgvPrograms.Columns[1].Width = 150;
                    dgvPrograms.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        private void frmProgram_Load(object sender, EventArgs e)
        {
            FillGrid(string.Empty);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid(string.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtProgramName.Text.Length == 0)
            {
                ep.SetError(txtProgramName, "Please Enter Program Name Correctly");
                txtProgramName.Focus();
                txtProgramName.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from ProgramTable where Name = '" + txtProgramName.Text.Trim() + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtProgramName, "Already Exist!");
                    txtProgramName.Focus();
                    txtProgramName.SelectAll();
                    return;
                }
            }

            string insertquery = string.Format("insert into ProgramTable(Name, IsActive) values ('{0}','{1}')",
                                txtProgramName.Text.Trim(), chkStatus.Checked);
            bool result = DatabaseLayer.Insert(insertquery);
            if (result == true)
            {
                MessageBox.Show("Saved Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Program Details. Then Try Again.");
            }
        }

        public void ClearForm()
        {
            txtProgramName.Clear();
            chkStatus.Checked = false;
        }

        public void EnableComponents()
        {
            dgvPrograms.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvPrograms.Enabled = true;
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
            if (dgvPrograms != null)
            {
                if (dgvPrograms.Rows.Count > 0)
                {
                    if (dgvPrograms.SelectedRows.Count == 1)
                    {
                        txtProgramName.Text = Convert.ToString(dgvPrograms.CurrentRow.Cells[1].Value);
                        chkStatus.Checked = Convert.ToBoolean(dgvPrograms.CurrentRow.Cells[2].Value);
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
            if (txtProgramName.Text.Length == 0)
            {
                ep.SetError(txtProgramName, "Please Enter Program Name Correctly");
                txtProgramName.Focus();
                txtProgramName.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from ProgramTable where Name = '" + txtProgramName.Text.Trim() + "' and ProgramID != '" + Convert.ToString(dgvPrograms.CurrentRow.Cells[0].Value) + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtProgramName, "Already Exist!");
                    txtProgramName.Focus();

                    txtProgramName.SelectAll();
                    return;
                }
            }

            string updatequery = string.Format("update ProgramTable set Name = '{0}', IsActive = '{1}' where ProgramID = '{2}'",
                                txtProgramName.Text.Trim(), chkStatus.Checked, Convert.ToString(dgvPrograms.CurrentRow.Cells[0].Value));
            bool result = DatabaseLayer.Update(updatequery);
            if (result == true)
            {
                MessageBox.Show("Updated Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Program Details. Then Try Again.");
            }
        }
    }
}
