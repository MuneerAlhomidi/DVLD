using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrDrivierLicenseInfo : UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _Licenses;

        public ctrDrivierLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectLicensesInfo
        {
            get { return _Licenses; }
        }

        private void _LoadImage()
        {
            if (_Licenses.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Licenses.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not FindBaseApplication this Image " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LoadInfo(int LicenseID)
        {

            _LicenseID = LicenseID;
            _Licenses = clsLicense.Find(_LicenseID);
            if (_Licenses == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                
                return;
            }
            lblClass.Text = _Licenses.LicensClassesIfo.ClassName;
            lblFullName.Text = _Licenses.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _Licenses.LicenseID.ToString();
            lblNationalNo.Text = _Licenses.DriverInfo.PersonInfo.NationalNo;
            lblIssueDate.Text = clsFormat.DateToShort(_Licenses.IssueDate);
            lblIssueReason.Text = _Licenses.IssueReason.ToString();
            lblNotes.Text = _Licenses.Notes == ""? "No,Notes" : _Licenses.Notes;
            lblIsActive.Text = _Licenses.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_Licenses.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _Licenses.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_Licenses.ExpirationDate);
            lblIssueReason.Text = _Licenses.IssueReasonText;
            lblIsDetained.Text = _Licenses.IsDetained ? "Yes":"No";

            lblGendor.Text = _Licenses.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            _LoadImage();
        }
    }
}