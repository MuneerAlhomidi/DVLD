using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListTestTypes : Form
    {
     
        private DataTable _dataAllListTypes = clsTestTypes.GetAllTestTypes();

        public frmListTestTypes()
        {
            InitializeComponent();
        }



        //private void _RefrchLoad()
        //{
        //    dgvListTestType.DataSource = _dataAllListTypes.AsDataView();
        //    lbRecords.Text = (dgvListTestType.RowCount.ToString());
        //}


        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
           
            dgvListTestType.DataSource = _dataAllListTypes;
            lbRecords.Text = dgvListTestType.Rows.Count.ToString();

            if(dgvListTestType.Rows.Count > 0 )
            {
                dgvListTestType.Columns[0].HeaderText = "ID";
                dgvListTestType.Columns[0].Width = 100;

                dgvListTestType.Columns[1].HeaderText = "Title";
                dgvListTestType.Columns[1].Width = 200;

                dgvListTestType.Columns[2].HeaderText = "Description";
                dgvListTestType.Columns[2].Width = 400;

                dgvListTestType.Columns[3].HeaderText = "Fees";
                dgvListTestType.Columns[3].Width = 100;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestTypes frm = new frmUpdateTestTypes((clsTestTypes.enTestType)dgvListTestType.CurrentRow.Cells[0].Value);
            frm .ShowDialog();
            frmListTestTypes_Load(null,null);
        }
    }
}
