using System;
using System.Data;
using System.Windows.Forms;

namespace DrivingLicenseManagement
{
    public partial class ctrlTableViewer : UserControl
    {
        public ctrlTableViewer()
        {
            InitializeComponent();

            // Hook the right-click handler
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
        }

        public DataTable DataSource
        {
            set { ShowTable(value, dataGridView1); }
        }

        public DataGridViewRow SelectedRow
        {
            get
            {
                if (dataGridView1.SelectedRows.Count > 0)
                    return dataGridView1.SelectedRows[0];
                return null;
            }
        }

        // Event that parent forms can subscribe to
        public event EventHandler<DataGridViewCellMouseEventArgs> RowRightClick;

        // Private helper that fills the grid
        private void ShowTable(DataTable table, DataGridView grid)
        {
          
            grid.DataSource = table;
        }

        // Detect right-clicks
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;

                // Notify whoever is using this control
                RowRightClick?.Invoke(this, e);
            }
        }
    }
}
