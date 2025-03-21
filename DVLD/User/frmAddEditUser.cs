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
using DVLD.Properties;



namespace DVLD
{
    public partial class frmAddEditUser : Form
    {
       

        private enum enMode { AddNew=0, Update=1}
        private enMode _Mode = enMode.AddNew;

        private int _UserID = -1;
        clsUsers _User;

        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
           
        }

        public frmAddEditUser(int User)
        {
            InitializeComponent();
            _UserID = User;
            _Mode = enMode.Update;


        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _RestDefuletValue();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void _RestDefuletValue()
        {
            if (_Mode == enMode.AddNew)
            {
                lbTital.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUsers();

                tbLoginInfo.Enabled = false;
                ctrPersonCardWithFilter1.FilterFocus();
   
            }
            else
            {
                lbTital.Text = "Update User";
                this.Text = "Update User";

                tbLoginInfo.Enabled = true;
                btnSaved.Enabled = true;
            }

           
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            ckIsActive.Checked = true;




        }

        private void _LoadData()
        {
          
            _User = clsUsers.FindByUserID(_UserID);
            ctrPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User With ID =  "+ _User.UserID,"User Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }


            lbUserID.Text = _User.PersonID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            ckIsActive.Checked = _User.IsActiv;
            ctrPersonCardWithFilter1.LoadPresonInfo(_User.PersonID);


        }

        

    
        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            int PesonID = obj;
            MessageBox.Show("Selected PersonID = " + PesonID.ToString());
        }

      

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
           

            if(txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim() )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaved_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {

                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrPersonCardWithFilter1.personID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActiv = ckIsActive.Checked;

            if (_User.Save())
            {
                lbUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;

                lbTital.Text = "Update User";
                this.Text  = "Update User";

                MessageBox.Show("Save is successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save in valid ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbLoginInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnNextPersonInfo_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSaved.Enabled = true;
                tbLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tbLoginInfo"];
                
                return;

            }

            if (ctrPersonCardWithFilter1.personID != -1)
            {
                if (clsUsers.IsExistForPersonID(ctrPersonCardWithFilter1.personID))
                {
                    MessageBox.Show("Selected Person already has a User,choose anther one.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ctrPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSaved.Enabled = true;
                    tbLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tbLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Plese Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrPersonCardWithFilter1.FilterFocus();
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User Name is not Blank!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if(_Mode == enMode.AddNew)
            {
                if(clsUsers.IsExistUserByUserName(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }
            else
            {
                if(_User.UserName != txtUserName.Text.Trim())
                {
                    if(clsUsers.IsExistUserByUserName(_User.UserName.Trim()))
                    {
                        e.Cancel =true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    };
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password is not blank!");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void frmAddEditUser_Activated(object sender, EventArgs e)
        {
            ctrPersonCardWithFilter1.FilterFocus();
        }

        
    }
}
