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
    public partial class frmUpdateTestTypes : Form
    {
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.visionTest;
        private clsTestTypes _TestType;

        public frmUpdateTestTypes()
        {
            InitializeComponent();
        }

        public frmUpdateTestTypes(clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            _TestTypeID = TestType;
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
           
            _TestType = clsTestTypes.Find(_TestTypeID );

            if (_TestType != null )
            {
                lbTestTypeID.Text =((int) _TestTypeID).ToString();
                txtTitle.Text = _TestType.TestTypeTital;
                txtDescription.Text = _TestType.TestTypeDescription;
                txtFees.Text = _TestType.TestTypeFees.ToString();
            }
            else

            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }

        }

        private void btnSaved_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s)" +
                    " to see the erro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _TestType.TestTypeTital = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestTypeFees =Convert.ToSingle( txtFees.Text.Trim());

            if(_TestType.Save())
            {
                MessageBox.Show("Data Save Successfully","Save",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Data Save not Successfully","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void changBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if(string.IsNullOrEmpty(Temp.Text) )
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This faild is blank!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }
    }
}
