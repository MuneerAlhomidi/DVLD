using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class CtrPersonCardWithFilter : UserControl
    {

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }
        private int _PersonID = -1;

       public int personID
        {
            get { return ctsPersonCard1.PersonID; }
            
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctsPersonCard1.SelectedPersonInfo; }
        }

        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
         Action<int> handler = OnPersonSelected;
            if (handler!= null)
            {
                handler(PersonID);
            }
        }
        public CtrPersonCardWithFilter()
        {
            InitializeComponent();

        }

        //public CtrPersonCardWithFilter(int PersonID)
        //{
        //    InitializeComponent();
        //    _PersonID = PersonID;
        //}

        public void LoadPresonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            FindNew();
        }

        public void FindNew()
        {
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    ctsPersonCard1.LoadPersonInfo(int.Parse(txtFilter.Text));

                    break;
                case "National No":
                    ctsPersonCard1.LoadPersonInfo(txtFilter.Text);
                    break;

                default:
                    break;

                   
            }

            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctsPersonCard1.PersonID);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Focus();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersons frm = new frmAddUpdatePersons();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        public void DataBackEvent(object sender, int PessonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilter.Text = PessonID.ToString();
            ctsPersonCard1.LoadPersonInfo(PessonID);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSerchPerson.PerformClick();
            }

            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSerchPerson_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Faild are Not valid, put the mous over red iccess","FindBaseApplication",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindNew();
        }

        private void CtrPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilter.Focus();


        }

        private void txtFilter_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "this faild is requerd!");
            }
            else
            {
                errorProvider1.SetError(txtFilter, null); 
            }
        }

        public void FilterFocus()
        {
            txtFilter.Focus();
        }
    }
}
