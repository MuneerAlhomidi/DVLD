using DVLD.Properties;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctsPersonCard : UserControl
    {
        clsPerson _person;
        private int _PersonID = -1;

        public int PersonID
        {
           get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _person; }
        }

        public ctsPersonCard()
        
        {
            InitializeComponent();
        }

        private void _LoadImage()
        {
            if (_person.Gendor == 0)
                pbImagePath.Image = Resources.Male_512;
            else
                pbImagePath.Image = Resources.Female_512;
            string ImagePath = _person.ImagePath;
            if(ImagePath != "")
                if(File.Exists(ImagePath))
                    pbImagePath.ImageLocation= ImagePath;
            else
                    MessageBox.Show("Cloud not Found this Image "+ ImagePath,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
        }
        private void _FillPersonInfo()
        {
            llbEditPersonInfo.Enabled = true;
            _PersonID = _person.PresonID;
            lbpersonID.Text = _person.PresonID.ToString();
            lbName.Text = _person.FullName;
            lbDateOfBirth.Text = _person.DateOfBirth.ToShortDateString();
            lbGender.Text = _person.Gendor == 0 ? "Male" : "Female";
            lbAddress.Text = _person.Address;
            lbEmail.Text = _person.Email;
            lbNationalNo.Text = _person.NationalNo;
            lbPhone.Text = _person.Phone;
            lbCountry.Text = clsCountry.Find(_person.NationlityCountryID).CountryName;

            _LoadImage();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            ResetPersonInfo();
            _person = clsPerson.Find(NationalNo);
            if( _person == null )
            {
                MessageBox.Show("No,PersonInfo with NationalNo = " + NationalNo,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _person = clsPerson.Find(PersonID);
            if (_person == null)
            {
                MessageBox.Show("No,Person Info with ID = "+_PersonID ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

        }

        private void llbEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePersons frm = new frmAddUpdatePersons(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lbpersonID.Text = "[????]";
            lbNationalNo.Text = "[????]";
            lbName.Text = "[????]";
            pbGender.Image = Resources.Man_32;
            lbGender.Text = "[????]";
            lbEmail.Text = "[????]";
            lbPhone.Text = "[????]";
            lbDateOfBirth.Text = "[????]";
            lbCountry.Text = "[????]";
            lbAddress.Text = "[????]";
            pbImagePath.Image = Resources.Male_512;

        }
    }
}
