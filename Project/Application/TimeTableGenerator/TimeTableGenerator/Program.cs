using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TImeTableGenerator.Forms;
using TImeTableGenerator.Forms.ConfigurationForms;
using TImeTableGenerator.Forms.ConfigurationForms.ProgramSemesterForms;
using TImeTableGenerator.Forms.LectureSubjectForms;
using TImeTableGenerator.Forms.ProgramSemesterForms;
using TImeTableGenerator.Forms.TimeSlotForms;
using TImeTableGenerator.Reports;

namespace TImeTableGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*            Application.Run(new frmPrintAllSemesterTimeTables());*/

            Application.Run(new HomeForm());
        }
    }
}
