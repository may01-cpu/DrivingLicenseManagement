using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseManagement.Applications
{
    public partial class ManageAppTypes : Form
    {
        private DataTable _dtAllApplicationTypes;
        public ManageAppTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationType()
        {
            _dtAllApplicationTypes = DVLDBusinessLayer.clsApplicationType.ListAllApplicationTypes();
            dataGridView1.DataSource = _dtAllApplicationTypes;
            dataGridView1.Columns["ApplicationTypeID"].HeaderText = "ID";
            dataGridView1.Columns["ApplicationTypeTitle"].HeaderText = "Title";
            LblRecordsNum.Text = _dtAllApplicationTypes.Rows.Count.ToString();
        }
        private void ManageAppTypes_Load(object sender, EventArgs e)
        {
          _RefreshApplicationType();
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }
        private void editApplicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           UpdateApplicationType updateApplicationType = new UpdateApplicationType(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ApplicationTypeID"].Value));
            updateApplicationType.ShowDialog();
            _RefreshApplicationType();
        }
    }
}
