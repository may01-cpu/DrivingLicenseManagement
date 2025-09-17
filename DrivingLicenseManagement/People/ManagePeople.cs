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

        private static DataTable _dtAllPeople= clsPeople.GetAllPeople();


        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                     "FirstName", "SecondName", "ThirdName", "LastName",
                                                     "GendorCaption", "DateOfBirth", "NationalityCountryID",
                                                     "Phone", "Email");

        private void refreshPeopleList()
        {
            _dtAllPeople = clsPeople.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                     "FirstName", "SecondName", "ThirdName", "LastName",
                                                     "GendorCaption", "DateOfBirth", "NationalityCountryID",
                                                     "Phone", "Email");
            ctrlTableViewer1.DataSource = _dtPeople;
        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            refreshPeopleList();
            txtFilter.Visible = false;

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
            refreshPeopleList();
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
            refreshPeopleList();
           
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(ctrlTableViewer1.SelectedRow.Cells["PersonID"].Value);
            UpdatePerson updatePerson = new UpdatePerson(PersonID);
            updatePerson.ShowDialog();
            refreshPeopleList();
           
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ctrlTableViewer1.SelectedRow == null)
            {
                MessageBox.Show("Please select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int PersonID = Convert.ToInt32(ctrlTableViewer1.SelectedRow.Cells["PersonID"].Value);

            if (MessageBox.Show("Are you sure you want to delete this person?",
                                "Delete Person",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clsPeople Person = clsPeople.FindPersonByID(PersonID);

                if (Person == null)
                {
                    MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string ImagePath = Person.ImagePath;

                if (clsPeople.DeletePerson(PersonID))
                {
                    // try deleting photo, ignore errors
                    try
                    {
                        PhotoManager.DeletePersonPhoto(ImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Person deleted, but photo could not be deleted:\n" + ex.Message,
                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ctrlFilter1_Load(object sender, EventArgs e)
        {
          
        }
       
        private string FilterColumn = "";
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (cmbFilter.SelectedItem == null || cmbFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
            }
            switch (cmbFilter.SelectedItem.ToString())
            {
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case
                    "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Address":
                    FilterColumn = "Address";
                    break;
                case "None":
                    FilterColumn = "";
                    break;


            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterColumn))
            {
                ctrlTableViewer1.DataSource = _dtPeople;
            }
            else
            {
                string filterExpression;
                if (FilterColumn != "PersonID")
                {
                     filterExpression = string.Format("{0} LIKE '%{1}%'", FilterColumn, txtFilter.Text.Replace("'", "''"));
                  
                }
                else
                {
                      filterExpression = string.Format("{0} = {1}", FilterColumn, txtFilter.Text.Replace("'", "''"));
                }
                DataView dv = new DataView(_dtPeople);
                dv.RowFilter = filterExpression;
                ctrlTableViewer1.DataSource = dv.ToTable();

            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (FilterColumn == "PersonID")
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
