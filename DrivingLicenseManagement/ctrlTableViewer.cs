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
    public partial class ctrlTableViewer : UserControl
    {
        public ctrlTableViewer()
        {
            InitializeComponent();
        }
        public DataTable DataSource
        {
            set { ShowTable(value, dataGridView1); }
        }

        // Private helper that fills the grid
        private void ShowTable(DataTable table, DataGridView grid)
        {
            if (table == null) return;

            grid.Columns.Clear();
            grid.Rows.Clear();

            // Add columns
            foreach (DataColumn col in table.Columns)
            {
                grid.Columns.Add(col.ColumnName, col.ColumnName);
            }

            // Add rows
            foreach (DataRow row in table.Rows)
            {
                grid.Rows.Add(row.ItemArray);
            }
        }
    }
}
