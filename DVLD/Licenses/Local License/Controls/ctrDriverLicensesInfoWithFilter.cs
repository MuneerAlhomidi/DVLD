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
    public partial class ctrDriverLicensesInfoWithFilter : UserControl
    {
        public event Action<int> onPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> Handler = onPersonSelected;
            if(Handler != null )
            {
                Handler(PersonID);
            }
        }

        private clsLicense _licenses;
        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrDrivierLicenseInfo1.LicenseID; }
        }

        public clsLicense SelectLicenseInfo
        {
            get { return ctrDrivierLicenseInfo1.SelectLicensesInfo; }
        }
        public ctrDriverLicensesInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gpFilter.Enabled = _FilterEnabled;
            }
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            
            ctrDrivierLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrDrivierLicenseInfo1.LicenseID;

            if (onPersonSelected != null && FilterEnabled)
                onPersonSelected(_LicenseID);

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro" ,"Validation Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID );
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This Field is required!");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        public void txtLicenseIDfocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar == (char)13)
            {
                btnFind. PerformClick();
            }
        }

        
    }
}
