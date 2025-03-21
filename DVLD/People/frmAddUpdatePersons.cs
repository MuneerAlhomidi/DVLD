using DVLD.Classes;
using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddUpdatePersons : Form
    {
        public delegate void DataBackHandel(object sender, int PersonID);
        public event DataBackHandel DataBack;

        public enum enMode { AddNew=0, Update=1}
        public enMode _Mode = enMode.AddNew;

        public enum enGegor { Male=0, Female=1}


        private int _PersonID = -1;
        
        
        clsPerson _Person;

        public frmAddUpdatePersons()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePersons(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
        }

        private void _FillPersonCombox()
        {
            DataTable dt = clsCountry.GetCountryDataTable();
            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _ReatDefultValue()
        {
            _FillPersonCombox();

            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
                //_Mode = enMode.Update;
                

            }
            else
            {
                lblTitle.Text = "Update Person";

            }

            if(rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image= Resources.Female_512;

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            

        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if( _Person == null )
            {
                MessageBox.Show("No,Person with ID ="+ _PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
            txtEmail.Text = _Person.Email;
           
            if(_Person.Gendor == 0)
            {
                rbMale.Enabled = true;
            }
            else
            {
                rbFemale.Enabled = true;
            }

            if(_Person.ImagePath !="")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }
            llRemoveImage.Visible = (_Person.ImagePath != "");

        }

        private void frmAddUpdatePersons_Load(object sender, EventArgs e)
        {
            _ReatDefultValue();
            if(_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Faild is valid! put  the mouse over the red icon(s)" +
                    " to see the erro","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            if(!_HandlePersonImage())
            { return; }
            int NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            if (txtThirdName.Text != null)
                _Person.ThirdName = txtThirdName.Text.Trim();
            else
                _Person.ThirdName = "";
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Address = txtAddress.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.NationlityCountryID = NationalityCountryID;
            if (rbMale.Checked)
                _Person.Gendor = (byte)enGegor.Male;
            else
                _Person.Gendor = (byte)enGegor.Female;
            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if(_Person.Save())
            {
                lblPersonID.Text = _PersonID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";
                MessageBox.Show("Data Save SuccessFully.","Save",MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _PersonID);
            }
            else
            {
                MessageBox.Show("Data Is valid Successfully.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ValidatEmptyCompox_Validating(object sender, CancelEventArgs e)
        {
            TextBox temp = ((TextBox)sender);
            if(string.IsNullOrEmpty(temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This Faild is Requerd!");
            }
            else
            {
                errorProvider1.SetError(temp, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtNationalNo, "This Faild is Requerd!");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            if(txtNationalNo.Text.Trim() != _Person.NationalNo &&   clsPerson.isExistPepole(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This Person Is Found!");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private bool _HandlePersonImage()
        {
            if(_Person.ImagePath != pbPersonImage.ImageLocation)
            {
               if(_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch(IOException )
                    {

                    }

                }



               if(pbPersonImage.ImageLocation != null)
                {
                    string SourceFile = pbPersonImage.ImageLocation.ToString();
                    if(clsUtil.CopyImageToProjectImagesFolder( ref SourceFile))
                    {
                        pbPersonImage.ImageLocation = SourceFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            return true;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty (txtEmail.Text.Trim()))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtEmail, "This Faild is Requerd!");
            }
            else
            {
                errorProvider1.SetError(txtEmail , null);
            }
            if(!clsValidatoin.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtEmail, "Valid is Requerd!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ImagePath = openFileDialog1.FileName;
                pbPersonImage.Load(ImagePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

                if (rbMale.Checked)
                    pbPersonImage.Image = Resources.Male_512;
                else
                    pbPersonImage.Image = Resources.Female_512;
            llRemoveImage .Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
