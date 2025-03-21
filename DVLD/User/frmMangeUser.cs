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
    public partial class frmMangeUser : Form
    {
        public frmMangeUser()
        {
            InitializeComponent();
        }

        private static DataTable _DataAllUsers;

        private void frmMangeUser_Load(object sender, EventArgs e)
        {
            _DataAllUsers = clsUsers.GetAllUser();
            dgvUserList.DataSource = _DataAllUsers;

            cbFilter.SelectedIndex = 0;

            lbRecords.Text = (dgvUserList.Rows.Count).ToString();

            if(dgvUserList.Rows.Count > 0 )
            {
                dgvUserList.Columns[0].HeaderText = "User ID";
                dgvUserList.Columns[0].Width = 120;

                dgvUserList.Columns[1].HeaderText = "Person ID";
                dgvUserList.Columns[1].Width = 120;

                dgvUserList.Columns[2].HeaderText = "Full Name";
                dgvUserList.Columns[2].Width = 350;

                dgvUserList.Columns[3].HeaderText = "User Name";
                dgvUserList.Columns[3].Width = 120;

                dgvUserList.Columns[4].HeaderText = "Is Active";
                dgvUserList.Columns[4].Width = 120;

            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
         frmUserInfo frm = new frmUserInfo((int)dgvUserList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmMangeUser_Load(null,null);
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            frmMangeUser_Load(null, null); 
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvUserList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmMangeUser_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you like delete this User", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                if (clsUsers.DeleteUser((int)dgvUserList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User has been Deleled successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmMangeUser_Load(null, null);
                }
                else
                {
                    MessageBox.Show("User is not deleted due to data connected to it.", "Falid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
            {
                txtFilter.Visible = false;
                cbFilterIsactive.Visible = true;
                cbFilterIsactive.Focus();
                cbFilterIsactive.SelectedIndex = 0;


            }
            else
            {
                txtFilter.Visible = (cbFilter.Text != "None");
                cbFilterIsactive.Visible = false;
                if (cbFilter.Text == "None")
                {
                    txtFilter.Enabled = false;
                   
                   
                }
                else
                {
                    txtFilter.Enabled = true;
                }
                txtFilter.Text = "";
                txtFilter.Focus();

            }
        }

        private void cbFilterIsactive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbFilterIsactive.Text;

            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _DataAllUsers.DefaultView.RowFilter = "";
            else
                _DataAllUsers.DefaultView.RowFilter=string.Format("[{0}] = {1}",FilterColumn,FilterValue);
            lbRecords.Text = _DataAllUsers.Rows.Count.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;
                default:
                    FilterColumn = "None";
                    break;

            }

            if(txtFilter.Text.Trim()=="" || FilterColumn == "None")
            {
                _DataAllUsers.DefaultView.RowFilter = "";
                lbRecords.Text = dgvUserList.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                _DataAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());

           // _DataAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _DataAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());
            lbRecords.Text = _DataAllUsers.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "User ID" || cbFilter.Text == "Person ID")
                e.Handled= !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUserList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmMangeUser_Load(null, null);
        }
    }
}
