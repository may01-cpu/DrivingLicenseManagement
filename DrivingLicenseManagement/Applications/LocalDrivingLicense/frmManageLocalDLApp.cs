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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DrivingLicenseManagement.Applications.LocalDrivingLicense
{
    public partial class frmManageLocalDLApp : Form
    {
        private DataTable _dtLocalApps;
        public frmManageLocalDLApp()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDLApp frm = new frmAddEditLocalDLApp();
            frm.ShowDialog();
            _Refresh();
        }
        private void newDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDLApp frm = new frmAddEditLocalDLApp();
            frm.ShowDialog();
            _Refresh();
        }

        private void _Refresh()
        {
            _dtLocalApps = clsApplication.ListAllLocalApps();
            dataGridView1.DataSource = _dtLocalApps;
            _ApplyFilter();
        }

        
        private void frmManageLocalDLApp_Load(object sender, EventArgs e)
        {
            _Refresh();
            

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView1, dataGridView1.PointToClient(Cursor.Position));
                
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cancelAppllicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0) return;
            if (clsLocalDLApplication.CancelLocalApplication(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value)))
            {
                MessageBox.Show("Application Canceled Successfully");
                _Refresh();
            }
            else
            {
                MessageBox.Show("Faild to Cancel Application");
            }
        }

        private string _FilterColumn="";
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.SelectedItem == null || cmbFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cmbStatus.Visible = false;
                _FilterColumn = "";
                dataGridView1.DataSource = _dtLocalApps;
                return;
            }

            string selected = cmbFilter.SelectedItem.ToString();

            if (selected == "Status")
            {
                cmbStatus.Visible = true;
                txtFilter.Visible = false;
            }
            else if (selected == "App.ID")
            {
                _FilterColumn = "LocalDrivingLicenseApplicationID";
                cmbStatus.Visible = false;
                txtFilter.Visible = true;
                return;
            }
            else
            {
                cmbStatus.Visible = false;
                txtFilter.Visible = true;
                
            }

            _FilterColumn = selected.Replace(" ", "");
        }

        private void _ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(_FilterColumn))
            {
                dataGridView1.DataSource = _dtLocalApps;
                return;
            }

            string filterExpression = "";

            if (_FilterColumn == "Status")
            {
                if (cmbStatus.SelectedIndex == -1)
                {
                    dataGridView1.DataSource = _dtLocalApps;
                    return;
                }

                filterExpression = string.Format("[{0}] LIKE '%{1}%'",
                                                  _FilterColumn,
                                                  cmbStatus.SelectedItem);
            }
            else if (_FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                if (string.IsNullOrWhiteSpace(txtFilter.Text))
                {
                    dataGridView1.DataSource = _dtLocalApps;
                    return;
                }

                if (int.TryParse(txtFilter.Text, out int number))
                {
                    filterExpression = string.Format("[{0}] = {1}", _FilterColumn, number);
                }
                else
                {
                    dataGridView1.DataSource = _dtLocalApps;
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtFilter.Text))
                {
                    dataGridView1.DataSource = _dtLocalApps;
                    return;
                }

                filterExpression = string.Format("[{0}] LIKE '%{1}%'",
                                                  _FilterColumn,
                                                  txtFilter.Text.Replace("'", "''"));
            }

            try
            {
                DataView dv = new DataView(_dtLocalApps);
                dv.RowFilter = filterExpression;
                dataGridView1.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ApplyFilter();
        }


        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_FilterColumn == "PersonID")
            {
                // Allow digits, backspace, and control keys
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // block the key
                }
            }
        }
    }
    
}
