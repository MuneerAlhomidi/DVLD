using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _ApplicationTypeTable = clsApplicationTypes.GetAllApplicationTypes();

      //  int _ApplicationTypeID = -1;
       

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        public frmListApplicationTypes(int ApplicationID)
        {
            InitializeComponent();
        }

        private void _RefrchLoad()
        {
            dgvListApplicationType.DataSource = _ApplicationTypeTable.AsDataView();
            llbCountRecord.Text = (dgvListApplicationType.RowCount).ToString();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
           dgvListApplicationType.DataSource = _ApplicationTypeTable;
           llbCountRecord.Text = dgvListApplicationType.Rows.Count.ToString();
            if (dgvListApplicationType.Rows.Count > 0)
            {
                dgvListApplicationType.Columns[0].HeaderText = "ID";
                dgvListApplicationType.Columns[0].Width = 100;

                dgvListApplicationType.Columns[1].HeaderText = "Titel";
                dgvListApplicationType.Columns[1].Width = 400;

                dgvListApplicationType.Columns[2].HeaderText = "Fees";
                dgvListApplicationType.Columns[2].Width = 100;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateFeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateApplicationTypes frm = new FrmUpdateApplicationTypes((int)dgvListApplicationType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefrchLoad();
        }
    }
}
