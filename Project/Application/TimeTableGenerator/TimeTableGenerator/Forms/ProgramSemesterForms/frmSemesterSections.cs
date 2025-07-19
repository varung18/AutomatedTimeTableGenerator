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
    public partial class frmSemesterSections : Form
    {
        public frmSemesterSections()
        {
            InitializeComponent();
        }

        public void FillGrid (string searchvalue)
        {
            try
            {
                string query = string.Empty;
                if (string.IsNullOrEmpty(searchvalue.Trim()))
                {
                    query = "select SectionID, SectionTitle [Section], ProgramSemesterID, Title [Semester],IsActive [Status] from v_AllSemesterSections order by ProgramSemesterID ";
                }
                else
                {
                    query = "select SectionID, SectionTitle [Section], ProgramSemesterID, Title [Semester],IsActive [Status] from v_AllSemesterSections" +
                            "where ( SectionTitle + ' '+ Title) like '%" + searchvalue.Trim() + "%' order by ProgramSemesterID";
                }

                DataTable sectionlist = DatabaseLayer.Retrieve(query);
                dgvSections.DataSource = sectionlist;
                if(dgvSections.Rows.Count > 0)
                {
                    dgvSections.Columns[0].Visible = false; // SectionID
                    dgvSections.Columns[1].Width = 200; // SectionTitle
                    dgvSections.Columns[2].Visible = false; //ProgramSemesterID
                    dgvSections.Columns[3].Width = 200; // Title
                    dgvSections.Columns[4].Width = 80; // IsActive

                    dgvSections.ClearSelection();
                }
            }
            catch 
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        private void FormClear()
        {
            txtTitle.Clear();
            cmbSemesters.SelectedIndex = 0;
        }

        private void frmSemesterSections_Load(object sender, EventArgs e)
        {
            ComboHelper.AllProgramSemesters(cmbSemesters);
            FillGrid(string.Empty);
        }

        private void txtCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FormClear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillGrid(txtSearch.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ep.Clear();
                if (txtTitle.Text.Trim().Length == 0)
                {
                    ep.SetError(txtTitle, "Please Enter Section Title");
                    txtTitle.Focus();
                    return;
                }

                if (cmbSemesters.SelectedIndex == 0)
                {
                    ep.SetError(cmbSemesters, "Please Enter Section Title");
                    cmbSemesters.Focus();
                    return;
                }

                //Check existing record, if found show "Already Exists" message near txttitle
                DataTable dt = DatabaseLayer.Retrieve("select * from SectionTable where SectionTitle = '" + txtTitle.Text.Trim() + "' and ProgramSemesterID = '" + cmbSemesters.SelectedValue + "'");
                if (dt!= null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ep.SetError(txtTitle, "Already Exists");
                        txtTitle.Focus();
                        return;
                    }
                }

                //Query for save/insert record to sectiontable
                string insertquery = string.Format("insert into SectionTable(SectionTitle, ProgramSemesterID, SectionCapacity) values ('{0}', '{1}', '{2}')", txtTitle.Text.Trim(), cmbSemesters.SelectedValue, txtCapacity.Text.Trim());
                bool result = DatabaseLayer.Insert(insertquery);
                if (result == true)
                {
                    MessageBox.Show("Saved Successfully!");
                    FillGrid(string.Empty);
                    FormClear();
                }
                else
                {
                    MessageBox.Show("Please Try Again");
                }
            }
            catch (Exception ex)//Show error message in any case if exception generates
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmsedit_Click(object sender, EventArgs e)
        {
            if (dgvSections != null)
            {
                if (dgvSections.Rows.Count > 0)
                {
                    if (dgvSections.SelectedRows.Count == 1)
                    {

                        if (MessageBox.Show("Are you sure you want to change status?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            bool existstatus = Convert.ToBoolean(dgvSections.CurrentRow.Cells[4].Value);
                            int sectionid = Convert.ToInt32(dgvSections.CurrentRow.Cells[0].Value);
                            bool status = false;
                            if (existstatus == true)
                            {
                                status = false;
                            }
                            else
                            {
                                status = true;
                            }
                            string updatequery = string.Format("update SectionTable set IsActive = '{0}' where SectionID = '{1}'", status, sectionid);
                            bool result = DatabaseLayer.Update(updatequery);
                            if (result == true)
                            {
                                MessageBox.Show("Change Successfully!");
                                FillGrid(string.Empty);
                            }
                            else
                            {
                                MessageBox.Show("Please Try Again!");
                            }
                        }
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
    }
}
