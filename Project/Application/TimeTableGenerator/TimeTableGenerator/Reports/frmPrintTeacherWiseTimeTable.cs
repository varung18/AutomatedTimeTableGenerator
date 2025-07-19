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
    public partial class frmPrintTeacherWiseTimeTable : Form
    {
        public frmPrintTeacherWiseTimeTable()
        {
            InitializeComponent();
            rpt_PrintTeacherWiseTimeTable rpt = new rpt_PrintTeacherWiseTimeTable();
            rpt.Refresh();
            crystalReportViewer1.ReportSource = rpt;
        }

       
        private void frmPrintTeacherWiseTimeTable_Load(object sender, EventArgs e)
        {

        }
    }
}
