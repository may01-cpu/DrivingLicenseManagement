using DrivingLicenseManagement.Users;
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

namespace DrivingLicenseManagement
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }


        private static DataTable _dtAllUsers = clsUser.ListAllUsers();
        private DataTable _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "FullName", "UserName", "IsActive");

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.ListAllUsers();
            _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID", "PersonID", "FullName", "UserName", "IsActive");
            dataGridView1.DataSource = _dtUsers;

        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dtUsers;
            txtFilter.Visible = false;
            cmbIsActive.Visible = false;
            dataGridView1.Columns["FullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["UserName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


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

        private void _ShowUserDetails()
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            frmShowUserInfo showUserInfo = new frmShowUserInfo(Convert.ToInt32(row.Cells["UserID"].Value), Convert.ToInt32(row.Cells["PersonID"].Value));
            showUserInfo.ShowDialog();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;
            _ShowUserDetails();
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowUserDetails();
        }

        //
        private void _AddNewUser()
        {
            frmAddEditUser addNewUser = new frmAddEditUser();
            addNewUser.ShowDialog();

            _RefreshUsersList();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            _AddNewUser();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddNewUser();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            frmAddEditUser editUser = new frmAddEditUser(Convert.ToInt32(row.Cells["UserID"].Value));
            editUser.ShowDialog();
            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];

            clsUser.DeleteUser(Convert.ToInt32(row.Cells["UserID"].Value));
            _RefreshUsersList();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];

            frmChangePassword frm = new frmChangePassword(Convert.ToInt32(row.Cells["UserID"].Value));
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private string FilterCol = "";

        private void cmbFilterUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterUsers.SelectedItem == null || cmbFilterUsers.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cmbIsActive.Visible = false;
                FilterCol = "";
                return;
            }

            string selected = cmbFilterUsers.SelectedItem.ToString();

            if (selected == "Is Active")
            {
                cmbIsActive.Visible = true;
                txtFilter.Visible = false;
            }
            else
            {
                cmbIsActive.Visible = false;
                txtFilter.Visible = true;
            }

            FilterCol = selected.Replace(" ", "");
        }

        private void cmbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIsActive.SelectedItem == null || string.IsNullOrEmpty(FilterCol))
                return;

            string selected = cmbIsActive.SelectedItem.ToString();

            if (selected == "All")
            {
                dataGridView1.DataSource = _dtUsers;
                return;
            }

            try
            {
                // Boolean column (0 = No, 1 = Yes)
                string filterExpression = string.Format("{0} = {1}",
                    FilterCol, (selected == "Yes" ? "1" : "0"));

                DataView dv = new DataView(_dtUsers);
                dv.RowFilter = filterExpression;
                dataGridView1.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FilterCol))
            {
                dataGridView1.DataSource = _dtUsers;
                return;
            }

            try
            {
                string filterExpression;

                if (FilterCol == "PersonID" || FilterCol == "UserID")
                {
                    // Numeric column filtering
                    if (int.TryParse(txtFilter.Text, out int number))
                    {
                        filterExpression = string.Format("{0} = {1}", FilterCol, number);
                    }
                    else
                    {
                        dataGridView1.DataSource = _dtUsers; // reset if invalid number
                        return;
                    }
                }
                else if (FilterCol == "IsActive")
                {
                    // Special case: boolean filtering
                    if (txtFilter.Text == "1" || txtFilter.Text.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                        filterExpression = "IsActive = 1";
                    else if (txtFilter.Text == "0" || txtFilter.Text.Equals("No", StringComparison.OrdinalIgnoreCase))
                        filterExpression = "IsActive = 0";
                    else
                    {
                        dataGridView1.DataSource = _dtUsers; // reset if invalid
                        return;
                    }
                }
                else
                {
                    // Text column filtering
                    filterExpression = string.Format("{0} LIKE '%{1}%'",
                        FilterCol, txtFilter.Text.Replace("'", "''"));
                }

                DataView dv = new DataView(_dtUsers);
                dv.RowFilter = filterExpression;
                dataGridView1.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }


    }
}
