using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement.Users
{
    public partial class frmAddEditUser : Form
    {
        enum eMode { Add, Edit }
        private eMode FormMode;
        public frmAddEditUser()
        {
            InitializeComponent();
            FormMode = eMode.Add;

        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            FormMode = eMode.Edit;
        }
        private clsUser _User;

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (FormMode == eMode.Add) { 
               
            }
            tabControl1.SelectedTab = tabPage2;
        }
    }
}
