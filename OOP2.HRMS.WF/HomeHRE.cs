using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;
using OOP2.HRMS.REPO;

namespace OOP2.HRMS.WF
{
    public partial class HomeHRE : MetroFramework.Forms.MetroForm
    {
        EmployeeInfoRepo repo = new EmployeeInfoRepo();
        UserAccountRepo repoUserAccount= new UserAccountRepo();
        private EmployeeInfo currentData = new EmployeeInfo();
        private EmployeeInfo selectedData = new EmployeeInfo();
        private int userID = LoginHelper.UserProfile.ID;
        public HomeHRE()
        {
            InitializeComponent();
        }

        private void HomeHREmployee_Load(object sender, EventArgs e)
        {
            this.Welcome();
            this.Init();
        }

        private void Init()
        {
            this.LoadEmployeeInfo();
        }

        private void LoadEmployeeInfo()
        {
            var result = repo.GetById(userID, "");

            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }

            currentData = result.Data;
            selectedData = currentData;

            this.PopulateData();
        }

        private void PopulateData()
        {
            txtboxNameHomeHRE.Text = currentData.EmpName;
            dateTimeHomeHRE.Text = currentData.DOB.ToString();
            comboBoxBloodHomeHRE.Text = currentData.BloodGroup;
            comboBoxGenderHomeHRE.Text = currentData.Gender;
            txtboxNationalityHomeHRE.Text = currentData.Nationality;
            txtboxAddressHomeHRE.Text = currentData.Address;
            txtboxEmailHomeHRE.Text = currentData.Email;
            txtboxContactHomeHRE.Text = currentData.Contact;
            txtboxReligionHomeHRE.Text = currentData.Religion;
            comboBoxMaritalHomeHRE.Text = currentData.MaritalStatus;
            txtboxNationalIDHomeHRE.Text = currentData.NID.ToString();
            pictureBoxHomeHRE.ImageLocation = currentData.ImagePath;
        }

        private void Welcome()
        {
            lblWelcome.Text = "Welcome, " + LoginHelper.UserProfile.Name + "(" + LoginHelper.UserProfile.ID + ")";
        }

        private void tilePayslipHREmp_Click(object sender, EventArgs e)
        {
            this.Hide();
            PayslipHRE psHRE = new PayslipHRE();
            psHRE.Show();
        }

        private void tileLeaveHREmp_Click(object sender, EventArgs e)
        {
            this.Hide();
            LeaveHRE leaveHRE= new LeaveHRE();
            leaveHRE.Show();
        }

        private void tileAttendanceHREmp_Click(object sender, EventArgs e)
        {
            this.Hide();
            AttendanceHRE attnHRE= new AttendanceHRE();
            attnHRE.Show();
        }

        private void tileLogoutHREmp_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginManager lm= new LoginManager("");
            lm.Show();
        }

        private void btnEditHomeHRE_Click(object sender, EventArgs e)
        {
            txtboxNameHomeHRE.ReadOnly = false;
            dateTimeHomeHRE.Enabled = true;
            comboBoxBloodHomeHRE.Enabled = true;
            comboBoxGenderHomeHRE.Enabled = true;
            txtboxNationalityHomeHRE.ReadOnly = false;
            txtboxAddressHomeHRE.ReadOnly = false;
            txtboxEmailHomeHRE.ReadOnly = false;
            txtboxContactHomeHRE.ReadOnly = false;
            txtboxPasswordHomeHRE.ReadOnly = false;
            txtboxPasswordHomeHRE.Enabled = true;
            txtboxReligionHomeHRE.ReadOnly = false;
            comboBoxMaritalHomeHRE.Enabled = true;
            txtboxNationalIDHomeHRE.ReadOnly = false;
            pictureBoxHomeHRE.Enabled = true;
            btnUploadHomeHRE.Enabled = true;
        }

        private void btnSaveHomeHRE_Click(object sender, EventArgs e)
        {
            var user = repoUserAccount.GetById(userID, "");
            if (string.IsNullOrEmpty(txtboxPasswordHomeHRE.Text))
            {
                selectedData.UserAccount.Password = user.Data.Password;
            }
            else
            {
                selectedData.UserAccount.Password = txtboxPasswordHomeHRE.Text;
            }
            this.Fill();
            var result = repo.Save(selectedData);

            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }

            var objToChange = selectedData;

            if (objToChange != null)
            {
                objToChange.EmpName = result.Data.EmpName;
                objToChange.DOB = result.Data.DOB;
                objToChange.Email = result.Data.Email;
                objToChange.BloodGroup = result.Data.BloodGroup;
                objToChange.Gender = result.Data.Gender;
                objToChange.Nationality = result.Data.Nationality;
                objToChange.Address = result.Data.Address;
                objToChange.Contact = result.Data.Contact;
                objToChange.Religion = result.Data.Religion;
                objToChange.MaritalStatus = result.Data.MaritalStatus;
                objToChange.NID = result.Data.NID;
                objToChange.UserAccount.Password = result.Data.UserAccount.Password;
            }

            selectedData = result.Data;
            this.PopulateData();

            MetroFramework.MetroMessageBox.Show(this, "Operation Successful");

            txtboxPasswordHomeHRE.Text = "";
        }

        private void Fill()
        {
            selectedData.EmpName = txtboxNameHomeHRE.Text;
            selectedData.DOB = dateTimeHomeHRE.Value;
            selectedData.BloodGroup = comboBoxBloodHomeHRE.Text;
            selectedData.Gender = comboBoxGenderHomeHRE.Text;
            selectedData.Nationality = txtboxNationalityHomeHRE.Text;
            selectedData.Address = txtboxAddressHomeHRE.Text;
            selectedData.Email = txtboxEmailHomeHRE.Text;
            selectedData.Contact = txtboxContactHomeHRE.Text;
            selectedData.Religion = txtboxReligionHomeHRE.Text;
            selectedData.MaritalStatus = comboBoxMaritalHomeHRE.Text;
            selectedData.NID = txtboxNationalIDHomeHRE.Text;
            selectedData.ImagePath = pictureBoxHomeHRE.ImageLocation;
        }

        private void btnUploadHomeHRE_Click(object sender, EventArgs e)
        {
            this.ImageUpload();
        }

        private void ImageUpload()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "images| *.JPG; *.PNG; *.GIF";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxHomeHRE.ImageLocation = ofd.FileName;
            }
        }
    }
}
