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
    }
}
