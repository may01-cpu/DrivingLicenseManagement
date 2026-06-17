using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement.Applications.LocalDrivingLicense
{
    public partial class frmShowApplicationDetails : Form
    {
        public frmShowApplicationDetails(int LocalApplicationID)
        {
            InitializeComponent();
            int ApplicationID=ctrlDLApplicationInfo1.LoadLocalApplicationInfo(LocalApplicationID);
            ctrlApplicationInfo1.LoadApplicationInfo(ApplicationID);
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowApplicationDetails_Load(object sender, EventArgs e)
        {

        }
    }
}

