using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TImeTableGenerator.AllModels;
using TImeTableGenerator.SourceCode;

namespace TImeTableGenerator.Forms.TimeSlotForms
{
    public partial class frmDayTimeSlots : Form
    {
        public frmDayTimeSlots()
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
                    query = "select DayTimeSlotID, ROW_NUMBER() OVER (Order by DayTimeSlotID) AS [S_No], DayID, Name [Day], SlotTitle [Slot Title], StartTime [Start Time], EndTime [EndTime], IsActive [Status] from v_AllTimeSlots WHERE IsActive = '1'";
                }
                else
                {
                    query = "select DayTimeSlotID, ROW_NUMBER() OVER (Order by DayTimeSlotID) AS [S_No], DayID, Name [Day], SlotTitle [Slot Title], StartTime [Start Time], EndTime [EndTime], IsActive [Status] from v_AllTimeSlots" +
                            "WHERE IsActive = 1 and (Name + ' ' + SlotTitle) Like '%" + searchvalue.Trim() + "%'";
                }

                DataTable daytimeslotlist = DatabaseLayer.Retrieve(query);
                dgvSlots.DataSource = daytimeslotlist;
                if (dgvSlots.Rows.Count > 0)
                {
                    dgvSlots.Columns[0].Visible = false; // DayTimeSlotID
                    dgvSlots.Columns[1].Width = 80; // S_no
                    dgvSlots.Columns[2].Visible = false ; // DayID
                    dgvSlots.Columns[3].Width = 130; // Name
                    dgvSlots.Columns[4].Width = 150; // SlotTitle
                    dgvSlots.Columns[5].Width = 100; // StartTime
                    dgvSlots.Columns[6].Width = 100; // EndTime
                    dgvSlots.Columns[7].Width = 80; // IsActive

                }
            }
            catch
            {
                MessageBox.Show("Some Unexpected Issues Occured. Please Try Again.");
            }
        }

        public void ClearForm()
        {
            cmbDays.SelectedIndex = 0;
            cmbNumberofTimeSlots.SelectedIndex = 0;
            chkStatus.Checked = true;
        }
         
        public void EnableComponents()
        {
            dgvSlots.Enabled = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            txtSearch.Enabled = false;
        }

        public void DisableComponents()
        {
            dgvSlots.Enabled = true;
            btnClear.Visible = true;
            btnSave.Visible = true;
            btnCancel.Visible = false;
            btnUpdate.Visible = false;
            txtSearch.Enabled = true;
            ClearForm();
            FillGrid(string.Empty);
        }

        private void frmDayTimeSlots_Load(object sender, EventArgs e)
        {
            dtpStartTime.Value = new DateTime(2024, 12, 12, 8, 0, 0);
            dtpEndTime.Value = new DateTime(2024, 12, 12, 16, 0, 0);
            ComboHelper.AllDays(cmbDays);
            ComboHelper.TimeSlotsNumbers(cmbNumberofTimeSlots);
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
                if (cmbDays.SelectedIndex == 0)
                {
                    ep.SetError(cmbDays, "Please Select Day!");
                    cmbDays.Focus();
                    return;
                }

                if (cmbNumberofTimeSlots.SelectedIndex == 0)
                {
                    ep.SetError(cmbNumberofTimeSlots, "Please Select Time Slots Per Day.");
                    cmbNumberofTimeSlots.Focus();
                    return;
                }

                /*string UpdateQuery = "update DayTimeSlotTable set IsActive = 0 where DayID = '" + cmbDays.SelectedValue + "'";
                bool updateresult = DatabaseLayer.Update(UpdateQuery);*/
                bool a = true;
                if (a == true)
                {


                    List<TimeSlotsMV> timeslots = new List<TimeSlotsMV>();

                    TimeSpan time = dtpEndTime.Value - dtpStartTime.Value;
                    int totalminutes = (int)time.TotalMinutes;
                    int numberoftimeslots = Convert.ToInt32(cmbNumberofTimeSlots.SelectedValue);
                    int slot = totalminutes / numberoftimeslots;


                    int i = 0;
                    do
                    {
                        var timeslot = new TimeSlotsMV();
                        var FromTime = (dtpStartTime.Value).AddMinutes(slot * i);
                        i++;
                        var ToTime = (dtpStartTime.Value).AddMinutes(slot * i);
                        string title = FromTime.ToString("HH:mm") + "-" + ToTime.ToString("HH:mm");
                        timeslot.FromTime = FromTime;
                        timeslot.ToTime = ToTime;
                        timeslot.SlotTitle = title;
                        timeslots.Add(timeslot);
                    } while (i < numberoftimeslots);

                    bool insertstatus = true;
                    foreach (TimeSlotsMV slottime in timeslots)
                    {
                        string insertquery = string.Format("insert into DayTimeSlotTable (DayID, SlotTitle, StartTime, EndTime, IsActive) values ('{0}', '{1}', '{2}', '{3}', '{4}')",
                                    cmbDays.SelectedValue, slottime.SlotTitle, slottime.FromTime, slottime.ToTime, chkStatus.Checked);
                        bool result = DatabaseLayer.Insert(insertquery);
                        if (result == false)
                            insertstatus = false;
                    }

                    if (insertstatus == true)
                    {
                        MessageBox.Show("Slots Created Successfully");
                        DisableComponents();
                    }
                    else
                    {
                        MessageBox.Show("Please Provide Correct Details and then Try again!");
                    }
                }
                else
                {
                    MessageBox.Show("Please Check Update Query!");
                }
            }
            catch 
            {
                MessageBox.Show("Check SQL Server Agent Connectivity");
            }
        }

        private void cmsedit_Click(object sender, EventArgs e)
        {
            if (dgvSlots != null)
            {
                if (dgvSlots.Rows.Count > 0)
                {
                    if (dgvSlots.SelectedRows.Count == 1)
                    {
                        string slotif = Convert.ToString(dgvSlots.CurrentRow.Cells[0].Value);
                        string updatequery = "update DayTimeSlotTable set IsActive = 0 where DayTimeSlotID = '" + Convert.ToString(dgvSlots.CurrentRow.Cells[0].Value) + " '";
                        bool result = DatabaseLayer.Update(updatequery);
                        if (result == true)
                        {
                            MessageBox.Show("Break TIme is Marked! And Exclude from the Time Table");
                            DisableComponents();
                        }
                    }
                }
            }
        }
    }
}
