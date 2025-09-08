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
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
            ctrlTableViewer1.RowRightClick += ctrlTableViewer1_RowRightClick;
        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            ctrlTableViewer1.DataSource = clsPeople.GetAllPeople();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlTableViewer1_Load(object sender, EventArgs e)
        {
           

        }
        private void ctrlTableViewer1_RowRightClick(object sender, DataGridViewCellMouseEventArgs e)
        { 
            peopleMenu.Show(ctrlTableViewer1, e.Location);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = ctrlTableViewer1.SelectedRow;
            ShowPersonDetails showPersonDetails = new ShowPersonDetails(Convert.ToInt32(row.Cells["PersonID"].Value));
            showPersonDetails.ShowDialog();

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("we will emplement this later", "Send Email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("we will emplement this later", "Phone Call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPerson addNewPerson
                = new AddNewPerson();
            addNewPerson.ShowDialog();
            ctrlTableViewer1.DataSource = clsPeople.GetAllPeople();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(ctrlTableViewer1.SelectedRow.Cells["PersonID"].Value);
            UpdatePerson updatePerson = new UpdatePerson(PersonID);
            updatePerson.ShowDialog();
            ctrlTableViewer1.DataSource = clsPeople.GetAllPeople();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(ctrlTableViewer1.SelectedRow.Cells["PersonID"].Value);
            if (MessageBox.Show("Are you sure you want to delete this person?", "Delete Person", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsPeople.DeletePerson(PersonID))
                {
                    MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ctrlTableViewer1.DataSource = clsPeople.GetAllPeople();
                }
                else
                {
                    MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
   
    
    }
}
