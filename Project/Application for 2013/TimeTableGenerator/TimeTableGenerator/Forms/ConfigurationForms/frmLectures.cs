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
    public partial class frmLectures : Form
    {
        public frmLectures()
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
                    query = "select LectureID [ID], FullName [Lecture] , ContactNo [Contact No] , IsActive [Status] from LectureTable";
                }
                else
                {
                    query = "select LectureID [ID], FullName [Lecture] , ContactNo [Contact No] , IsActive [Status] from LectureTable where (FullName +  ' ' + ContactNo) like '%" + searchvalue.Trim() + "%'";
                }

                DataTable Lecturerlist = DatabaseLayer.Retrieve(query);
                dgvLecturer.DataSource = Lecturerlist;
                if (dgvLecturer.Rows.Count > 0)
                {
                    dgvLecturer.Columns[0].Width = 80;
                    dgvLecturer.Columns[1].Width = 150;
                    dgvLecturer.Columns[2].Width = 100;
                    dgvLecturer.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        private void frmLectures_Load(object sender, EventArgs e)
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
            if (txtLecturerName.Text.Length <5)
            {
                ep.SetError(txtLecturerName, "Please Enter Full Name");
                txtLecturerName.Focus();
                txtLecturerName.SelectAll();
                return;
            }

            if (txtContactNo.Text.Length <12)
            {
                ep.SetError(txtContactNo, "Please Enter Contact No Correctly");
                txtContactNo.Focus();
                txtContactNo.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from LectureTable where FullName = '" + txtLecturerName.Text.Trim() + "' and Contact No = '" + txtContactNo.Text.Trim() + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtLecturerName, "Already Exist!");
                    txtLecturerName.Focus();
                    txtLecturerName.SelectAll();
                    return;
                }
            }

            string insertquery = string.Format("insert into LectureTable(FullName, ContactNo, IsActive) values ('{0}','{1}', '{2}')",
                                txtLecturerName.Text.Trim(), txtContactNo.Text.Trim(), chkStatus.Checked);
            bool result = DatabaseLayer.Insert(insertquery);
            if (result == true)
            {
                MessageBox.Show("Saved Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Lecturer Details. Then Try Again.");
            }
        }

        public void ClearForm()
        {
            txtLecturerName.Clear();
            txtContactNo.Clear();
            chkStatus.Checked = false;
        }

        public void EnableComponents()
        {
            dgvLecturer.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvLecturer.Enabled = true;
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
            if (dgvLecturer != null)
            {
                if (dgvLecturer.Rows.Count > 0)
                {
                    if (dgvLecturer.SelectedRows.Count == 1)
                    {
                        txtLecturerName.Text = Convert.ToString(dgvLecturer.CurrentRow.Cells[1].Value);
                        txtContactNo.Text = Convert.ToString(dgvLecturer.CurrentRow.Cells[2].Value);
                        chkStatus.Checked = Convert.ToBoolean(dgvLecturer.CurrentRow.Cells[3].Value);
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
            if (txtLecturerName.Text.Length < 5)
            {
                ep.SetError(txtLecturerName, "Please Enter Full Name");
                txtLecturerName.Focus();
                txtLecturerName.SelectAll();
                return;
            }

            if (txtContactNo.Text.Length < 12)
            {
                ep.SetError(txtContactNo, "Please Enter Contact No Correctly");
                txtContactNo.Focus();
                txtContactNo.SelectAll();
                return;
            }

            DataTable checktitle = DatabaseLayer.Retrieve("select * from LectureTable where FullName = '" + txtLecturerName.Text.Trim() + "'and ContactNo = '"+txtContactNo.Text.Trim()+ "' and LectureID != '"  + Convert.ToString(dgvLecturer.CurrentRow.Cells[0].Value) + "'");
            if (checktitle != null)
            {
                if (checktitle.Rows.Count > 0)
                {
                    ep.SetError(txtLecturerName, "Already Exist!");
                    txtLecturerName.Focus();

                    txtLecturerName.SelectAll();
                    return;
                }
            }

            string updatequery = string.Format("update LectureTable set FullName = '{0}', ContactNo = '{1}' , IsActive = '{2}' where LectureID = '{3}'",
                                txtLecturerName.Text.Trim(), txtContactNo.Text.Trim(), chkStatus.Checked, Convert.ToString(dgvLecturer.CurrentRow.Cells[0].Value));
            bool result = DatabaseLayer.Update(updatequery);
            if (result == true)
            {
                MessageBox.Show("Updated Successfully");
                DisableComponents();
            }
            else
            {
                MessageBox.Show("Please Provide Correct Lab Details. Then Try Again.");
            }
        }
    }
}
