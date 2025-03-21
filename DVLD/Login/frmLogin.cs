using DVLD.Classes;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        private int _FildLoginTrials = 0;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential1(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Password = clshashing.ComputeHash(txtPassword.Text.Trim());
            clsUsers User = clsUsers.FindByUserNameAndPassword(txtUserName.Text.Trim(),txtPassword.Text.Trim());
            if(User != null)
            {
                if(chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword1(txtUserName.Text.Trim(),txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword1("", "");
                }

                if(!User.IsActiv)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = User;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();

            }
            else
            {
                _FildLoginTrials++;
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials",MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(_FildLoginTrials >= 3)
                {
                    clsGlobal.SaveToEventViewer("3 Fild Login Trials",EventLogEntryType.Error);
                    btnLogin.Enabled = false;
                }
            }
        }

    }
}

