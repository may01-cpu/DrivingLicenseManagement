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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DrivingLicenseManagement.Applications.LocalDrivingLicense
{
    public partial class frmAddEditLocalDLApp : Form
    {
        enum eMode { Add,Edit}
        private eMode FormMode;

        private clsLocalDLApplication _Application;
        public frmAddEditLocalDLApp()
        {
            InitializeComponent();
            FormMode = eMode.Add;
            lblTitle.Text = "Add New Local Driving License Application";
            _Application = new clsLocalDLApplication();
            _Application.ApplicationType = clsApplicationType.FindApplicationType(1);

        }
        public frmAddEditLocalDLApp(int ID)
        {
            InitializeComponent();
            FormMode = eMode.Edit;
            lblTitle.Text = "Edit Local Driving License Application";

        }

        private DataTable _LicenseClasses = clsLicenseClasses.ListAllLicenseClasses();

        private void _LoadLicenseClasses()
        {
            
            foreach (DataRow row in _LicenseClasses.Rows)
            {
                cmbLicenseClass.Items.Add(row["ClassName"].ToString());
            }
            
        }
        private void frmAddEditLocalDLApp_Load(object sender, EventArgs e)
        {
            tabPage2.Enabled=false;
            _LoadLicenseClasses();
          
            ctrlUserFilter1.Enabled = (FormMode == eMode.Add);

        }
        private void _ResetLocalAppInfo()
        {
            lblDLAppID.Text = "????";
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblAppFees.Text = "????";
            lblCreatedBy.Text = "????";
            cmbLicenseClass.SelectedIndex = -1;
        }
        private void _LoadLocalAppInfo()
        {
            //lblDLAppID.Text = _Application;
            //lblAppDate.Text = DateTime.Now.ToShortDateString();
            //lblAppFees.Text = "????";
            //lblCreatedBy.Text = "????";
            //cmbLicenseClass.SelectedIndex = -1;

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlUserFilter1.SelectedPersonInfo != null)
            {
                tabPage2.Enabled = true;
                tabControl1.SelectedTab = tabPage2;

                if (FormMode == eMode.Add )
                {
                    //_User.Person = ctrlUserFilter1.SelectedPersonInfo;
                   _ResetLocalAppInfo();

                }
                else if (FormMode == eMode.Edit)
                {
                    _LoadLocalAppInfo();
                }
              
            }
            else
            {
                tabPage2.Enabled = false;
                MessageBox.Show("You have to choose a Person");

            }



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbLicenseClass.SelectedIndex != -1)
            {

                _Application.ApplicationFees = Convert.ToDecimal(lblAppFees.Text);
                _Application.Applicant = ctrlUserFilter1.SelectedPersonInfo;
                _Application.LicenseClass = clsLicenseClasses.FromDataRow(_LicenseClasses.Rows[cmbLicenseClass.SelectedIndex]);
                if (!clsLocalDLApplication.IsApplicationExistByLicenseClass(_Application.Applicant.PersonID, _Application.LicenseClass.LicenseClassID))
                {
                    if (_Application.Save())
                    {
                        string message = FormMode == eMode.Add ? "Application Added Successfuly" : "Application Updated Successfuly";

                        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Faild to save Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Choose another License Class .the selected Person already have an active application for the selected class with  ");
                }




            }
            else
            {
                MessageBox.Show("You must fill in all the fields.");

                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAppFees.Text = _LicenseClasses.Select("ClassName='" + cmbLicenseClass.SelectedItem.ToString() + "'")[0]["ClassFees"].ToString();
            lblCreatedBy.Text = clsUser.LoggedInUser.Username;

        }
    }
}
