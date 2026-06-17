using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement.Test
{
    public partial class frmScheduleTest : Form
    {

        public frmScheduleTest(int LocalDLApp)
        {
            InitializeComponent();
            ctrlRetakeTestInfo1.Enabled = false;
            ctrlSchedule1.LoadScheduleInfo(LocalDLApp);

        }

    
    
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
