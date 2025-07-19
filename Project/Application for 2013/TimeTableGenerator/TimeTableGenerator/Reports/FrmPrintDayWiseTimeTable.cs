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
    public partial class FrmPrintDayWiseTimeTable : Form
    {
        public FrmPrintDayWiseTimeTable()
        {
            InitializeComponent();
            rpt_PrintDayWiseTimeTables rpt = new rpt_PrintDayWiseTimeTables();
            rpt.Refresh();
            crv.ReportSource = rpt;
        }

        private void FrmPrintDayWiseTimeTable_Load(object sender, EventArgs e)
        {

        }
    }
}
