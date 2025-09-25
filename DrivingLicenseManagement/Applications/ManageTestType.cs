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

namespace DrivingLicenseManagement.Applications
{
    public partial class ManageTestType : Form
    {
        private DataTable _dtAllTestTypes;
        public ManageTestType()
        {
            InitializeComponent();
        }
       
        private void _RefreshTestType()
        {
            _dtAllTestTypes = clsTestType.ListAllTestTypes();
            dataGridView1.DataSource = _dtAllTestTypes;
            dataGridView1.Columns["TestTypeID"].HeaderText = "ID";
            dataGridView1.Columns["TestTypeTitle"].HeaderText = "Title";
            dataGridView1.Columns["TestTypeDescription"].HeaderText = "Description";
            dataGridView1.Columns["TestTypeFees"].HeaderText = "Fees";
            LblRecordsNum.Text = _dtAllTestTypes.Rows.Count.ToString();
        }
        private void ManageTestType_Load(object sender, EventArgs e)
        {
            _RefreshTestType();
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }
        

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTestTypes updateApplicationType = new UpdateTestTypes(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TestTypeID"].Value));
            updateApplicationType.ShowDialog();
            _RefreshTestType();
        }
    }
}
