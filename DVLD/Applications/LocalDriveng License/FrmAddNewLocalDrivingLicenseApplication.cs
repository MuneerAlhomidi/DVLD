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
    public partial class FrmAddNewLocalDrivingLicenseApplication : Form
    {
        public enum enMode { AddNew=0, Update=1}
        public enMode Mode = enMode.AddNew;

        private int _SelectedPersonID = -1;
        private int _LocalDrivingLicense = -1;  
        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplications;

        public FrmAddNewLocalDrivingLicenseApplication(int LocalDrivingLicense)
        {
            InitializeComponent();
            _LocalDrivingLicense = LocalDrivingLicense;
            Mode = enMode.Update;
        }


        public FrmAddNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        private void FrmAddNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _RestDevultValue();
            if (Mode == enMode.Update)
                _LoadData();
        }

        private DataTable _FillcompBoxLicesnseClass()
        {
            DataTable dt = clsLicensClasses.GetAllLicenseClasses();
            foreach(DataRow row in dt.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
            return dt;
        }

        private void _RestDevultValue()
        {
            _FillcompBoxLicesnseClass();

            if(Mode == enMode.AddNew)
            {
                llbTitle.Text = "New Local Driving License Applications";
                this.Text = "New Local Driving License Applications";
                _LocalDrivingLicenseApplications = new clsLocalDrivingLicenseApplications();
                ctrPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;
                btnSave.Enabled = false;
//                llbDrivingLicenseApplication.Text = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
                llbApplicationDate.Text = DateTime.Now.ToString();
                cbLicenseClasses.SelectedIndex = 2;
                llbFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                llbCreatedBy.Text = clsGlobal.CurrentUser.UserName;


            }
            else
            {
                    llbTitle.Text = "Update Local Driving License Applications";
                    this.Text = "Update Local Driving License Applications";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                
            }

           

             

        }

        private void _LoadData()
        {
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.FindLocalDrivingLicenseApplicationID(_LocalDrivingLicense);
            if( _LocalDrivingLicenseApplications == null )
            {
                MessageBox.Show("Local Driving License Applications Is not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrPersonCardWithFilter1.LoadPresonInfo(_LocalDrivingLicenseApplications.ApplicationPersonID);
//         
            llbApplicationDate.Text =clsFormat.DateToShort( _LocalDrivingLicenseApplications.ApplicationDate);
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(_LocalDrivingLicenseApplications.InfoLicesnseClass.ClassName);
            llbFees.Text = _LocalDrivingLicenseApplications.PaidFees.ToString();
            llbCreatedBy.Text = clsUsers.FindByUserID(_LocalDrivingLicenseApplications.CreatedByUserID).UserName;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcLocalDrivingLicenseApplication.SelectedTab = tcLocalDrivingLicenseApplication.TabPages["tpApplicationInfo"];

                return;
            }

            if(ctrPersonCardWithFilter1.personID != -1)
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcLocalDrivingLicenseApplication.SelectedTab = tcLocalDrivingLicenseApplication.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show("Please Select a person","Select a Person",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseClassID = clsLicensClasses.Find(cbLicenseClasses.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClasses.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(ctrPersonCardWithFilter1.personID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplications.ApplicationPersonID = ctrPersonCardWithFilter1.personID;
            _LocalDrivingLicenseApplications.AppliactionTypeID = 1;
            _LocalDrivingLicenseApplications.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplications.LastDateStatus = DateTime.Now;
            _LocalDrivingLicenseApplications.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplications.AppliactionStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplications.PaidFees = Convert.ToSingle(llbFees.Text);
            _LocalDrivingLicenseApplications.LicenseClassID = LicenseClassID;

            if(_LocalDrivingLicenseApplications.Save())
            {
                llbDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID.ToString();
                Mode = enMode.Update;
                llbTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Save Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Data save not successfully","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
