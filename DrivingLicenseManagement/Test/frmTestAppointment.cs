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

namespace DrivingLicenseManagement.Test
{
    public partial class frmTestAppointment : Form
    {
        //clsLocalDLApplication LDLApp;
        private static int _ApplicationID=-1;
        private static int _LocalApplicationID=-1;

        private static DataTable _dtAllTestAppoinments = clsTestAppointment.ListAllTestAppointments(_LocalApplicationID);



        private void _RefreshAppointmentsList()
        {
            _dtAllTestAppoinments = clsTestAppointment.ListAllTestAppointments(_LocalApplicationID);
            dataGridView1.DataSource = _dtAllTestAppoinments;

        }
        
        public frmTestAppointment(string title ,int LocalApplicationID)
        {
            InitializeComponent();
            lbltitle.Text= title;
            _LocalApplicationID = LocalApplicationID;
            //LDLApp = clsLocalDLApplication.FindLocalApplication(LocalApplicationID);
            _ApplicationID = ctrlDLApplicationInfo1.LoadLocalApplicationInfo(LocalApplicationID);
            ctrlApplicationInfo1.LoadApplicationInfo(_ApplicationID);

        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dtAllTestAppoinments;
            //lblRecords.Text= _dtAllTestAppoinments.  //size of dt
            
        }

        private void pictureBox2_Click(object sender, EventArgs e) { 
            

            frmScheduleTest sheduleTest = new frmScheduleTest( _LocalApplicationID );
            sheduleTest.ShowDialog();
            _RefreshAppointmentsList();
            
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
