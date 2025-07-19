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
    public partial class frmSession : Form
    {
        public frmSession()
        {
            InitializeComponent();
        }

        public void FillGrid (string searchvalue)
        {
            try
            {
                string query = string.Empty;
                if (String.IsNullOrEmpty(searchvalue.Trim()))
                {
                    query = "select SessionID [ID], Title, IsActive [Status] from SessionTable";
                }
                else
                {
                    query = "select SessionID [ID], Title, IsActive [Status] from SessionTable where Title like '%" + searchvalue.Trim() + "%'";
                }

                DataTable sessonlist = DatabaseLayer.Retrieve(query);
                dgvSession.DataSource = sessonlist;
                if (dgvSession.Rows.Count > 0)
                {
                    dgvSession.Columns[0].Width = 80;
                    dgvSession.Columns[1].Width = 150;
                    dgvSession.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch 
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid(txtSearch.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtSessionTitle.Text.Length < 9)
            {
                ep.SetError(txtSessionTitle, "Please Enter Correct Session Title");
                txtSessionTitle.Focus();
                txtSessionTitle.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from SessionTable where Title = '" + txtSessionTitle.Text.Trim() + "'");
            if (checktitle !=  null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtSessionTitle, "Already Exist!");
                    txtSessionTitle.Focus();
                    txtSessionTitle.SelectAll();
                    return;
                }
            }

            string insertquery = string.Format("insert into SessionTable(Title, IsActive) values ('{0}','{1}')",
                                txtSessionTitle.Text.Trim(), chkStatus.Checked);
            bool result = DatabaseLayer.Insert(insertquery);
            if (result == true)
            {
                MessageBox.Show("Saved Successfully");
                DisableComponents();
            } else
            {
                MessageBox.Show("Please Provide Correct Session Details. Then Try Again.");
            }
        }

        public void ClearForm ()
        {
            txtSessionTitle.Clear();
            chkStatus.Checked = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();

        }

        private void frmSession_Load(object sender, EventArgs e)
        {
            FillGrid(string.Empty);
        }

        public void EnableComponents()
        {
            dgvSession.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvSession.Enabled = true;
            btnClear.Visible = true;
            btnSave.Visible = true;
            btnCancel.Visible = false;
            btnUpdate.Visible = false;
            txtSearch.Enabled = true;
            ClearForm();
            FillGrid(string.Empty);
        }

        private void cmsedit_Click(object sender, EventArgs e)
        {
            if (dgvSession != null)
            {
                if (dgvSession.Rows.Count > 0)
                {
                    if (dgvSession.SelectedRows.Count == 1)
                    {
                        txtSessionTitle.Text = Convert.ToString(dgvSession.CurrentRow.Cells[1].Value);
                        chkStatus.Checked = Convert.ToBoolean(dgvSession.CurrentRow.Cells[2].Value);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisableComponents();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtSessionTitle.Text.Length < 9)
            {
                ep.SetError(txtSessionTitle, "Please Enter Correct Session Title");
                txtSessionTitle.Focus();
                txtSessionTitle.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from SessionTable where Title = '" + txtSessionTitle.Text.Trim() + "' and SessionID != '"+Convert.ToString(dgvSession.CurrentRow.Cells[0].Value) + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtSessionTitle, "Already Exist!");
                    txtSessionTitle.Focus();
                    txtSessionTitle.SelectAll();
                    return;
                }
            }

            string updatequery = string.Format("update SessionTable set Title = '{0}', IsActive = '{1}' where SessionID = '{2}'",
                                txtSessionTitle.Text.Trim(), chkStatus.Checked, Convert.ToString(dgvSession.CurrentRow.Cells[0].Value));
            bool result = DatabaseLayer.Update(updatequery);
            if (result == true)
            {
                MessageBox.Show("Updated Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Session Details. Then Try Again.");
            }
        }
    }
}
