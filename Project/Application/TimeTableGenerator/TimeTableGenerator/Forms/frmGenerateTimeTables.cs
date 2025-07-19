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

namespace TImeTableGenerator.Forms
{
    public partial class frmGenerateTimeTables : Form
    {
        public frmGenerateTimeTables()
        {
            InitializeComponent();
        }

        private void btnGenerateTimeTables_Click(object sender, EventArgs e)
        {
            try
            {
                ep.Clear();
                string Message = GenerateTimeTable.AutoGenerateTimeTable(dtpStartDate.Value, dtpEndDate.Value);
                MessageBox.Show(Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
