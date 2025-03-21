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
using DVLD;


namespace DVLD
{
    public partial class ctrDriverLicense : UserControl
    {
        private int _DriverID = -1;
        private clsDriver _Driver;
        private DataTable _dtLocalDriverLicenseHistory;
        private DataTable _dtInternationalDriverLicenseHistory;

        public ctrDriverLicense()
        {
            InitializeComponent();
            
        }

        public void LoadLocalDriver()
        {
            _dtLocalDriverLicenseHistory = clsDriver.GetLicense(_DriverID);
            dgvLocalLicense.DataSource = _dtLocalDriverLicenseHistory;

            lbRecordLocalLicens.Text = _dtLocalDriverLicenseHistory.Rows.Count.ToString();

            if(dgvLocalLicense.Rows.Count >0)
            {
                dgvLocalLicense.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicense.Columns[0].Width = 110;

                dgvLocalLicense.Columns[1].HeaderText = "App.ID";
                dgvLocalLicense.Columns[1].Width = 110;

                dgvLocalLicense.Columns[2].HeaderText = "Class Name";
                dgvLocalLicense.Columns[2].Width = 150;

                dgvLocalLicense.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicense.Columns[3].Width = 170;

                dgvLocalLicense.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicense.Columns[4].Width = 170;

                dgvLocalLicense.Columns[5].HeaderText = "Is Active";
                dgvLocalLicense.Columns[5].Width = 110;
            }
        }

        private void _LoadInternationalDriverLicense()
        {
            _dtInternationalDriverLicenseHistory = clsInternationalLicense.AllInternationalLicense();
            dgvInternational.DataSource = _dtInternationalDriverLicenseHistory;

            lbRecordInternational.Text = _dtInternationalDriverLicenseHistory.Rows.Count.ToString();

            if(dgvInternational.Rows.Count > 0 )
            {
                dgvInternational.Columns[0].HeaderText = "Int.License ID";
                dgvInternational.Columns[0].Width = 130;

                dgvInternational.Columns[1].HeaderText = "Appliction ID";
                dgvInternational.Columns[1].Width = 130;

                dgvInternational.Columns[2].HeaderText = "Driver ID";
                dgvInternational.Columns[2].Width = 130;

                dgvInternational.Columns[3].HeaderText = "Local Licene ID";
                dgvInternational.Columns[3].Width = 130;

                dgvInternational.Columns[4].HeaderText = "Issue Date";
                dgvInternational.Columns[4].Width = 170;

                dgvInternational.Columns[5].HeaderText = "Expiration Date";
                dgvInternational.Columns[5].Width = 170;

                dgvInternational.Columns[6].HeaderText = "Is Active";
                dgvInternational.Columns[6].Width = 110;

            }

        }


        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(DriverID);

           _LoadInternationalDriverLicense();
            LoadLocalDriver();
        }

        public void LoadInfoByPerson(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);
            if(_Driver != null)
            {
                _DriverID = clsDriver.FindByPersonID(PersonID).DriverID;
            }
            LoadLocalDriver();
            _LoadInternationalDriverLicense();
        }


        public void Clear()
        {
            _dtLocalDriverLicenseHistory.Clear();

        }

        private void ShowLocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicense.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }
    }
}
