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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class ctrApllicationBasicInformation : UserControl
    {
        private int _ApplicationID;
        clsApplication _Application;
       public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public ctrApllicationBasicInformation()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.FindBaseApplication(ApplicationID);
            if (_Application == null)
            {
                MessageBox.Show("ApplicationID is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                _FillApplicationInfo();
        }

        private void _FillApplicationInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblApplicant.Text = _Application.ApplicationFullName;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.LastDateStatus);
            lblType.Text = _Application.AppliactionTypeInfo.ApplicationTypeTitl;
            lblStatus.Text = _Application.StatusNext;
            lblFees.Text = _Application.PaidFees.ToString();
            lblCreatedByUser.Text = _Application.CreatedByUserID.ToString();
        }

        public void RestApplicationInfo()
        {
            _ApplicationID = -1;
            lblApplicationID.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblFees.Text = "[????]";
            lblStatus.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
            lblType.Text = "[????]";


        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_Application.ApplicationPersonID);
            frm.ShowDialog();

            LoadApplicationInfo(_ApplicationID);
        }

        private void llViewPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_Application.ApplicationPersonID);
            frm.ShowDialog();
        }
    }
}
