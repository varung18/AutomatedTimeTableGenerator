using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TImeTableGenerator.Forms.ConfigurationForms;
using TImeTableGenerator.Forms.ConfigurationForms.ProgramSemesterForms;
using TImeTableGenerator.Forms.LectureSubjectForms;
using TImeTableGenerator.Forms.ProgramSemesterForms;
using TImeTableGenerator.Forms.TimeSlotForms;
using TImeTableGenerator.Reports;

namespace TImeTableGenerator.Forms
{
    public partial class HomeForm : Form
    {
        frmCourses CoursesForm;
        frmDays DaysForm;
        frmLectures LecturesForm;
        frmLabs LabsForm;
        frmProgram ProgramForm;
        frmRooms RoomForm;
        frmSemesters SemestersForm;
        frmSession SessionForm;
        frmLectureSubjectForms LectureSubjectForms;
        frmProgramSemesters ProgramSemestersForm;
        frmProgramSemesterSubject ProgramSemesterSubjectForms;
        frmDayTimeSlots DayTimeSlotsForm;
        frmSemesterSections SemesterSectionsForm;
        frmGenerateTimeTables GenerateTimeTableForm;
        frmPrintAllSemesterTimeTables PrintAllSemesterTimeTableForm;
        frmPrintTeacherWiseTimeTable PrintTeacherWiseTimeTableForm;
        FrmPrintDayWiseTimeTable PrintDayWiseTimeTableForm;
        
        public HomeForm()
        {
            InitializeComponent();
            tsslblDateTime.Text = DateTime.Now.ToString("dd mm yyyy");
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (ProgramForm == null)
            {
                ProgramForm = new frmProgram();
            }
            ProgramForm.ShowDialog();
        }

        private void tsbSession_Click(object sender, EventArgs e)
        {
            if (SessionForm == null)
            {
                SessionForm = new frmSession();
            }
            SessionForm.ShowDialog();
        }

        private void tsbSubject_Click(object sender, EventArgs e)
        {
            if (CoursesForm == null)
            {
                CoursesForm = new frmCourses();
            }
            CoursesForm.ShowDialog();
        }

        private void tssdLectures_Click(object sender, EventArgs e)
        {

        }

        private void newLectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void newLectureToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (LecturesForm == null)
            {
                LecturesForm = new frmLectures();
            }
            LecturesForm.ShowDialog();
        }

        private void assignSubjectsToLectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LectureSubjectForms == null)
            {
                LectureSubjectForms = new frmLectureSubjectForms();
            }
            LectureSubjectForms.ShowDialog();
        }

        private void addRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RoomForm == null)
            {
                RoomForm = new frmRooms();
            }
            RoomForm.ShowDialog();
        }

        private void addLabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LabsForm == null)
            {
                LabsForm = new frmLabs();
            }
            LabsForm.ShowDialog();
        }

        private void newSemestersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SemestersForm == null)
            {
                SemestersForm = new frmSemesters();
            }
            SemestersForm.ShowDialog();
        }

        private void addSemesterSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SemesterSectionsForm == null)
            {
                SemesterSectionsForm = new frmSemesterSections();
            }
            SemesterSectionsForm.ShowDialog();
        }

        private void assignSemesterToProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProgramSemestersForm == null)
            {
                ProgramSemestersForm = new frmProgramSemesters();
            }
            ProgramSemestersForm.ShowDialog();
        }

        private void assignSubjectToSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProgramSemesterSubjectForms == null)
            {
                ProgramSemesterSubjectForms = new frmProgramSemesterSubject();
            }
            ProgramSemesterSubjectForms.ShowDialog();
        }

        private void addDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DaysForm == null)
            {
                DaysForm = new frmDays();
            }
            DaysForm.ShowDialog();
        }

        private void dayTimeSlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DayTimeSlotsForm == null)
            {
                DayTimeSlotsForm = new frmDayTimeSlots();
            }
            DayTimeSlotsForm.ShowDialog();
        }

        private void tsbGenerate_Click(object sender, EventArgs e)
        {
            if (GenerateTimeTableForm == null)
            {
                GenerateTimeTableForm = new frmGenerateTimeTables();
            }
            GenerateTimeTableForm.ShowDialog();
        }

        private void printAllTimeTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PrintAllSemesterTimeTableForm == null)
            {
                PrintAllSemesterTimeTableForm = new frmPrintAllSemesterTimeTables();
            }
            PrintAllSemesterTimeTableForm.TopLevel = false;
            panelHeader.Controls.Add(PrintAllSemesterTimeTableForm);
            PrintAllSemesterTimeTableForm.Dock = DockStyle.Fill;
            PrintAllSemesterTimeTableForm.FormBorderStyle = FormBorderStyle.None;
            PrintAllSemesterTimeTableForm.BringToFront();
            PrintAllSemesterTimeTableForm.Show();
        }

        private void printAllTeacherTimeTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PrintTeacherWiseTimeTableForm == null)
            {
                PrintTeacherWiseTimeTableForm = new frmPrintTeacherWiseTimeTable();
            }
            PrintTeacherWiseTimeTableForm.TopLevel = false;
            panelHeader.Controls.Add(PrintTeacherWiseTimeTableForm);
            PrintTeacherWiseTimeTableForm.Dock = DockStyle.Fill;
            PrintTeacherWiseTimeTableForm.FormBorderStyle = FormBorderStyle.None;
            PrintTeacherWiseTimeTableForm.BringToFront();
            PrintTeacherWiseTimeTableForm.Show();
        }

        private void printAllDayWiseTimeTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PrintDayWiseTimeTableForm == null)
            {
                PrintDayWiseTimeTableForm = new FrmPrintDayWiseTimeTable();
            }
            PrintDayWiseTimeTableForm.TopLevel = false;
            panelHeader.Controls.Add(PrintDayWiseTimeTableForm);
            PrintDayWiseTimeTableForm.Dock = DockStyle.Fill;
            PrintDayWiseTimeTableForm.FormBorderStyle = FormBorderStyle.None;
            PrintDayWiseTimeTableForm.BringToFront();
            PrintDayWiseTimeTableForm.Show();
        }
    }
}
