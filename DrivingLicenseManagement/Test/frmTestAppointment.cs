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
        private int _ApplicationID=-1;
        private int _LocalApplicationID=-1;
        private int _TestTypeID = -1;
        private DataTable _dtAllTestAppoinments;
        private void _RefreshAppointmentsList()
        {
            _dtAllTestAppoinments = clsTestAppointment.ListAllTestAppointments(_LocalApplicationID,_TestTypeID);
            dataGridView1.DataSource = _dtAllTestAppoinments;
            lblRecords.Text = _dtAllTestAppoinments.Rows.Count.ToString();

        }
        
        public frmTestAppointment(string title ,int LocalApplicationID,int testTypeID)
        {
            InitializeComponent();
            lbltitle.Text= title;
            _LocalApplicationID = LocalApplicationID;
            _TestTypeID = testTypeID;
            _ApplicationID = ctrlDLApplicationInfo1.LoadLocalApplicationInfo(LocalApplicationID);
            ctrlApplicationInfo1.LoadApplicationInfo(_ApplicationID);
           _dtAllTestAppoinments = clsTestAppointment.ListAllTestAppointments(_LocalApplicationID,_TestTypeID);
        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dtAllTestAppoinments;
            lblRecords.Text = _dtAllTestAppoinments.Rows.Count.ToString();


        }

        private void pictureBox2_Click(object sender, EventArgs e) {
       
            // 1 — already has an active (unlocked) appointment
            if (clsLocalDLApplication.IsThereAnActiveScheduledTest(_LocalApplicationID, _TestTypeID))
            {
                MessageBox.Show("This applicant already has an active scheduled test. " +
                                "Please wait until it is taken before scheduling a new one.",
                                "Already Scheduled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2 — already passed this test, no need for another
            if (clsLocalDLApplication.DoesPassTestType(_LocalApplicationID, _TestTypeID))
            {
                MessageBox.Show("This applicant already passed this test.",
                                "Test Already Passed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmScheduleTest scheduleTest = new frmScheduleTest(_LocalApplicationID, _TestTypeID);
            scheduleTest.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView1, dataGridView1.PointToClient(Cursor.Position));
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int appointmentID = Convert.ToInt32(
                dataGridView1.SelectedRows[0].Cells["TestAppointmentID"].Value);

            frmScheduleTest scheduleTest = new frmScheduleTest(_LocalApplicationID, _TestTypeID, appointmentID);
            scheduleTest.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int appointmentID = Convert.ToInt32(
              dataGridView1.SelectedRows[0].Cells["TestAppointmentID"].Value);

            frmTakeTest takeTest = new frmTakeTest(appointmentID,_TestTypeID);
            takeTest.ShowDialog();
            _RefreshAppointmentsList();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            bool isLocked = Convert.ToBoolean(
                dataGridView1.SelectedRows[0].Cells["IsLocked"].Value);

            takeTestToolStripMenuItem.Enabled = !isLocked; 
            editToolStripMenuItem.Enabled = !isLocked;
        }
    }
}
