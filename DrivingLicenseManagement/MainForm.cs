using DrivingLicenseManagement.Applications;
using DrivingLicenseManagement.Applications.LocalDrivingLicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople people = new ManagePeople();
            people.ShowDialog();
   

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void usersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageUsers Users=new ManageUsers();
            Users.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageAppTypes appTypes=new ManageAppTypes();
            appTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTestType manageTestType=new ManageTestType();
            manageTestType.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDLApp frmManageLocalDLApp = new frmManageLocalDLApp();
            frmManageLocalDLApp.ShowDialog();
        }

        private void detainLicencesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDLApp frmAddEditLocalDLApp = new frmAddEditLocalDLApp();
            frmAddEditLocalDLApp.ShowDialog();
        }
    }
}
