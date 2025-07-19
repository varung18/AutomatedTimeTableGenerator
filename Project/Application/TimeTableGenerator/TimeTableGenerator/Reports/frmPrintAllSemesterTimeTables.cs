using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TImeTableGenerator.Reports
{
    public partial class frmPrintAllSemesterTimeTables : Form
    {
        public frmPrintAllSemesterTimeTables()
        {
            InitializeComponent();

            rpt_PrintSemesterWiseTimeTable rpt = new rpt_PrintSemesterWiseTimeTable();
            rpt.Refresh();
            crv.ReportSource = rpt;
        }

        private void frmPrintAllSemesterTimeTables_Load(object sender, EventArgs e)
        {

        }
    }
}
