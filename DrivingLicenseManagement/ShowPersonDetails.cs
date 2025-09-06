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
    public partial class ShowPersonDetails : Form
    {
      
        public ShowPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonInfo1.PersonID = PersonID;
            ctrlPersonInfo1.RefreshPersonInfo();
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
