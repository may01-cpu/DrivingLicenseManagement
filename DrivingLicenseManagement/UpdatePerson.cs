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
    public partial class UpdatePerson :Form
    {
        public UpdatePerson(int PersonID)
        {
            InitializeComponent();
            
            ctrlAddPerson1.PrepareForUpdate(PersonID); 
        }

        private void UpdatePerson_Load(object sender, EventArgs e)
        {

        }
    }
}
