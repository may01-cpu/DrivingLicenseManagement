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


        private static DataTable _dtAllUsers= clsUser.ListAllUsers();
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
            if (e.Button == MouseButtons.Right && e.RowIndex>=0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(dataGridView1,e.Location );
            }
        }

        private void _ShowUserDetails()
        {
            ShowUserInfo showUserInfo = new ShowUserInfo();
            showUserInfo.ShowDialog();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _ShowUserDetails();
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowUserDetails();
        }
        
        //
        private void _AddNewUser()
        {
            //add user code;
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

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
