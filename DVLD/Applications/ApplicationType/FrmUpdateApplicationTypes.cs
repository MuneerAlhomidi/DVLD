using DVLD.Classes;
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
    public partial class FrmUpdateApplicationTypes : Form
    {
        private int _ApplicationID = -1;
        clsApplicationTypes _ApplicationType;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public FrmUpdateApplicationTypes()
        {
            InitializeComponent();
        }

        public FrmUpdateApplicationTypes(int ApplicationID)
        {
            InitializeComponent();
           _ApplicationID = ApplicationID;
        }

        private void FrmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            //_FillApplication();
            _LoadData();
        }

        private void _FillApplication()
        {
            lbApplicationID.Text = "[???]";
            txtFees.Text = "";
            txtTitle.Text = "";

        }

        private void _LoadData()
        {
            lbApplicationID.Text = _ApplicationID.ToString();

            _ApplicationType = clsApplicationTypes.Find(_ApplicationID);
            if(_ApplicationType != null )
            {
                txtTitle.Text = _ApplicationType.ApplicationTypeTitl;
                txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s)" +
                    " to see the erro ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitl = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees =Convert.ToSingle( txtFees.Text.Trim());

            if(_ApplicationType.Save())
            {
               // lbApplicationID.Text = _ApplicationType.ApplicationTypeID.ToString();

                MessageBox.Show("Data Save is Successfully.","Save",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Data is Not Save Succesfuly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void FrmUpdateApplicationTypes_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot blanl!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot blank!");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

            if(!clsValidatoin.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "InValid Number");

            }
            else
            {
                errorProvider1.SetError(txtFees,null);
            }
        }
    }
}
