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
    public partial class frmListDriver : Form
    {
        private DataTable _dgvDriver;
        private clsDriver _Driver;
       

        public frmListDriver()
        {
            InitializeComponent();
            
        }

        private void frmListDriver_Load(object sender, EventArgs e)
        {
            _dgvDriver = clsDriver.GetAllDrivers();
            dgvDriver.DataSource = _dgvDriver;

            lbRecords.Text = _dgvDriver.Rows.Count.ToString();
            
            if(_dgvDriver.Rows.Count > 0 )
            {
                dgvDriver.Columns[0].HeaderText = "Driver ID";
                dgvDriver.Columns[0].Width = 120;

                dgvDriver.Columns[1].HeaderText = "Person ID";
                dgvDriver.Columns[1].Width = 120;

                dgvDriver.Columns[2].HeaderText = "National No";
                dgvDriver.Columns[2].Width = 140;

                dgvDriver.Columns[3].HeaderText = "Full Name";
                dgvDriver.Columns[3].Width = 320;

                dgvDriver.Columns[4].HeaderText = " Date";
                dgvDriver.Columns[4].Width = 170;

                dgvDriver.Columns[5].HeaderText = " Active Licenses";
                dgvDriver.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFliter.Visible = (cbFilterBy.Text != "None");
            if (cbFilterBy.Text == "None")
            {
                txtFliter.Enabled = false;


            }
            else
            {
                txtFliter.Enabled = true;
            }
            txtFliter.Text = "";
            cbFilterBy.Focus();
        }

        private void txtFliter_TextChanged(object sender, EventArgs e)
        {
            string FilterColomn = "";
            switch(cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColomn = "DriverID";
                    break;
                case "Person ID":
                    FilterColomn = "PersonID";
                    break;
                case "National No ":
                    FilterColomn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColomn = "FullName";
                    break;
                case "Number Of Active Licenses":
                    FilterColomn = "NumberOfActiveLicenses";
                    break;

                default:
                    FilterColomn = "None";
                    break;
            }

            if (txtFliter.Text.Trim() == "" || FilterColomn == "None")
            {
                _dgvDriver.DefaultView.RowFilter = "";
               lbRecords.Text =  _dgvDriver.Rows.Count.ToString();
                return;
            }

            if (FilterColomn != "FullName" && FilterColomn != "NationalNo")
            
                _dgvDriver.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColomn, txtFliter.Text.Trim());
            
            else
                _dgvDriver.DefaultView.RowFilter = string.Format("[{0}]  LIKE '{1}%'", FilterColomn, txtFliter.Text.Trim());


           
            lbRecords.Text = _dgvDriver.Rows.Count.ToString();

        }

        private void txtFliter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "DriverID" ||cbFilterBy.Text == "PersonID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
