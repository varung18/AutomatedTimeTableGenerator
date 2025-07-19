

using System.Data;
using System.Windows.Forms;

namespace TImeTableGenerator.SourceCode
{
    public class ComboHelper
    {
        public static void Semesters (ComboBox cmb)
        {
            DataTable dtSemesters = new DataTable();
            dtSemesters.Columns.Add("SemesterID");
            dtSemesters.Columns.Add("SemesterName");
            dtSemesters.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select SemesterID, SemesterName from SemesterTable where IsActive = 1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow semester in dt.Rows)
                        {
                            dtSemesters.Rows.Add(semester["SemesterID"], semester["SemesterName"]);
                        }
                    }
                }
                cmb.DataSource = dtSemesters;
                cmb.ValueMember = "SemesterID";
                cmb.DisplayMember = "SemesterName";
            }
            catch
            {
                cmb.DataSource = dtSemesters;
            }
        }

        public static void Programs(ComboBox cmb)
        {
            DataTable dtPrograms = new DataTable();
            dtPrograms.Columns.Add("ProgramID");
            dtPrograms.Columns.Add("Name");
            dtPrograms.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select ProgramID, Name from ProgramTable where IsActive = 1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow program in dt.Rows)
                        {
                            dtPrograms.Rows.Add(program["ProgramID"], program["Name"]);
                        }
                    }
                }
                cmb.DataSource = dtPrograms;
                cmb.ValueMember = "ProgramID";
                cmb.DisplayMember = "Name";
            }
            catch
            {
                cmb.DataSource = dtPrograms;
            }
        }

        public static void Types(ComboBox cmb)
        {
            DataTable dtTypes = new DataTable();
            dtTypes.Columns.Add("RoomTypeID");
            dtTypes.Columns.Add("TypeName");
            dtTypes.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select RoomTypeID, TypeName from RoomTypeTable");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow type in dt.Rows)
                        {
                            dtTypes.Rows.Add(type["RoomTypeID"], type["TypeName"]);
                        }
                    }
                }
                cmb.DataSource = dtTypes;
                cmb.ValueMember = "RoomTypeID";
                cmb.DisplayMember = "TypeName";
            }
            catch
            {
                cmb.DataSource = dtTypes;
            }
        }

        public static void AllDays(ComboBox cmb)
        {
            DataTable dtDays = new DataTable();
            dtDays.Columns.Add("DayID");
            dtDays.Columns.Add("Name");
            dtDays.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select DayID, Name from DayTable where IsActive = 1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow day in dt.Rows)
                        {
                            dtDays.Rows.Add(day["DayID"], day["Name"]);
                        }
                    }
                }
                cmb.DataSource = dtDays;
                cmb.ValueMember = "DayID";
                cmb.DisplayMember = "Name";
            }
            catch
            {
                cmb.DataSource = dtDays;
            }
        }

        public static void TimeSlotsNumbers(ComboBox cmb)
        {
            DataTable dtX = new DataTable();
            dtX.Columns.Add("ID");
            dtX.Columns.Add("Number");
            dtX.Rows.Add("0", "---Select---");
            dtX.Rows.Add("1", "1");
            dtX.Rows.Add("2", "2");
            dtX.Rows.Add("3", "3");
            dtX.Rows.Add("4", "4");
            dtX.Rows.Add("5", "5");
            dtX.Rows.Add("6", "6");
            dtX.Rows.Add("7", "7");
            dtX.Rows.Add("8", "8");
            dtX.Rows.Add("9", "9");
            dtX.Rows.Add("10", "10");


            cmb.DataSource = dtX;
            cmb.ValueMember = "ID";
            cmb.DisplayMember = "Number";
            
        }


        public static void AllTeachers(ComboBox cmb)
        {
            DataTable dtTeachers = new DataTable();
            dtTeachers.Columns.Add("LectureID");
            dtTeachers.Columns.Add("FullName");
            dtTeachers.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select LectureID, FullName from LectureTable where IsActive = 1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow day in dt.Rows)
                        {
                            dtTeachers.Rows.Add(day["LectureID"], day["FullName"]);
                        }
                    }
                }
                cmb.DataSource = dtTeachers;
                cmb.ValueMember = "LectureID";
                cmb.DisplayMember = "FullName";
            }
            catch
            {
                cmb.DataSource = dtTeachers;
            }
        }

        public static void AllSubjects(ComboBox cmb)
        {
            DataTable dtSubjects = new DataTable();
            dtSubjects.Columns.Add("CourseID");
            dtSubjects.Columns.Add("Title");
            dtSubjects.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select CourseID, Title from CourseTable where IsActive = 1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow course in dt.Rows)
                        {
                            dtSubjects.Rows.Add(course["CourseID"], course["Title"]);
                        }
                    }
                }
                cmb.DataSource = dtSubjects;
                cmb.ValueMember = "CourseID";
                cmb.DisplayMember = "Title";
            }
            catch
            {
                cmb.DataSource = dtSubjects;
            }
        }

        public static void AllProgramSemesters(ComboBox cmb)
        {
            DataTable dtPS = new DataTable();
            dtPS.Columns.Add("ProgramSemesterID");
            dtPS.Columns.Add("Title");
            dtPS.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select ProgramSemesterID, Title from v_ProgramSemesterActiveList where ProgramSemesterIsActive = 1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow prosem in dt.Rows)
                        {
                            dtPS.Rows.Add(prosem["ProgramSemesterID"], prosem["Title"]);
                        }
                    }
                }
                cmb.DataSource = dtPS;
                cmb.ValueMember = "ProgramSemesterID";
                cmb.DisplayMember = "Title";
            }
            catch
            {
                cmb.DataSource = dtPS;
            }
        }

        public static void AllTeacherSubjects(ComboBox cmb)
        {
            DataTable dtPS = new DataTable();
            dtPS.Columns.Add("LectureSubjectID");
            dtPS.Columns.Add("SubjectTitle");
            dtPS.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select LectureSubjectID, SubjectTitle from v_AllSubjectsTeachers where IsActive = 1 order by SubjectTitle");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow prosem in dt.Rows)
                        {
                            dtPS.Rows.Add(prosem["LectureSubjectID"], prosem["SubjectTitle"]);
                        }
                    }
                }
                cmb.DataSource = dtPS;
                cmb.ValueMember = "LectureSubjectID";
                cmb.DisplayMember = "SubjectTitle";
            }
            catch
            {
                cmb.DataSource = dtPS;
            }
        }

        public static void ActiveSessions(ComboBox cmb)
        {
            DataTable dtSS = new DataTable();
            dtSS.Columns.Add("SessionID");
            dtSS.Columns.Add("Title");
            dtSS.Rows.Add("0", "---Select---");
            try
            {
                DataTable dt = DatabaseLayer.Retrieve("Select SessionID, Title from SessionTable where IsActive = 1");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow ss in dt.Rows)
                        {
                            dtSS.Rows.Add(ss["SessionID"], ss["Title"]);
                        }
                    }
                }
                cmb.DataSource = dtSS;
                cmb.ValueMember = "SessionID";
                cmb.DisplayMember = "Title";
            }
            catch
            {
                cmb.DataSource = dtSS;
            }
        }
    }
}
