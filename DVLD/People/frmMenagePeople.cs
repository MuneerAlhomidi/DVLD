using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace DVLD
{
    public partial class frmMenagePeople : Form
    {
        //enum enMode { AddNew =0, Update=1}
        //enMode Mode = enMode.AddNew;

        //int _PersonID;
        //clsPerson _Person;
        private static DataTable _dataAllPeolple = clsPerson.GetAllPerson();

        private DataTable _dataPeople = _dataAllPeolple.DefaultView.ToTable(false,"PersonID","NationalNo","FirstName","SecondName","ThirdName",
                                                                                  "LastName","GendorCaption","DateOfBirth","CountryName","Phone","Email");

        public frmMenagePeople()
        {
            InitializeComponent();
            //_PersonID = PersonID;

            //if (_PersonID == -1)
            //    Mode = enMode.AddNew;
            //else
            //    Mode = enMode.Update;

        }

        public void _RefrechPerson()
        {
            dgvListPeople.DataSource = _dataPeople.AsDataView();
            lbCount.Text = (dgvListPeople.RowCount).ToString();
            
        }

        private void frmMenagePeople_Load(object sender, EventArgs e)
        {
            dgvListPeople.DataSource = _dataPeople;
            cbFilterBy.SelectedIndex = 0;
            lbCount.Text = _dataPeople.Rows.Count.ToString();

            if (dgvListPeople.Rows.Count > 0) 
            {
                dgvListPeople.Columns[0].HeaderText = "Person ID";
                dgvListPeople.Columns[0].Width = 110;

                dgvListPeople.Columns[1].HeaderText = "National No";
                dgvListPeople.Columns[1].Width = 100;

                dgvListPeople.Columns[2].HeaderText = "First Name";
                dgvListPeople.Columns[2].Width = 120;

                dgvListPeople.Columns[3].HeaderText = "Second Name";
                dgvListPeople.Columns[3].Width = 120;

                dgvListPeople.Columns[4].HeaderText = "Third Name";
                dgvListPeople.Columns[4].Width = 100;

                dgvListPeople.Columns[5].HeaderText = "List Name";
                dgvListPeople.Columns[5].Width = 120;

                dgvListPeople.Columns[6].HeaderText = "Gendor Caption";
                dgvListPeople.Columns[6].Width = 120;

                dgvListPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvListPeople.Columns[7].Width = 150;

                dgvListPeople.Columns[8].HeaderText = "Country Name";
                dgvListPeople.Columns[8].Width = 110;

                dgvListPeople.Columns[9].HeaderText = "Phone";
                dgvListPeople.Columns[9].Width = 110;

                dgvListPeople.Columns[10].HeaderText = "Email";
                dgvListPeople.Columns[10].Width = 200;



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersons frm = new frmAddUpdatePersons();
            frm.ShowDialog();
            _RefrechPerson();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersons frm = new frmAddUpdatePersons((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefrechPerson();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersons frm = new frmAddUpdatePersons();
            frm.ShowDialog();
            _RefrechPerson();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgvListPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)dgvListPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");

                }

                else
                    MessageBox.Show("Person is not deleted.");

            }
            _RefrechPerson();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            frmPersonDetails frm = new frmPersonDetails((int)dgvListPeople.CurrentRow.Cells[0].Value);
            
            frm.ShowDialog();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Focus();
           
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Sacond Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "Last Name";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                case "Country Name":
                    FilterColumn = "CountryName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            
            if(txtFilter.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dataPeople.DefaultView.RowFilter = "";
                lbCount.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "PersonID")

                _dataPeople.DefaultView.RowFilter = string.Format("[{0}] = {1} ",FilterColumn,txtFilter.Text.Trim());
            else
                _dataPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%' ", FilterColumn, txtFilter.Text.Trim());

            lbCount.Text = dgvListPeople.Rows.Count.ToString();
        }

        public void FilterValue()
        {
            if(cbFilterBy.Text == "None")
            {
                txtFilter.Enabled = false;
            }

           // if(cbFilterBy.Text == "PersoID")

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if(cbFilterBy.Text == "None") { txtFilter.Visible = false; }
            
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilterBy.Text != "None");
            if(txtFilter.Visible )
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }
    }
}
