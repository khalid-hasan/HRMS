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
    public partial class HomeHRD : MetroFramework.Forms.MetroForm
    {
        EmployeeInfoRepo repo= new EmployeeInfoRepo();
        UserAccountRepo repoUserAccount= new UserAccountRepo();
        private EmployeeInfo currentData= new EmployeeInfo();
        private EmployeeInfo selectedData= new EmployeeInfo();
        private int userID = LoginHelper.UserProfile.ID;
        public HomeHRD()
        {
            InitializeComponent();
        }

        private void HomeHRDirector_Load(object sender, EventArgs e)
        {
              try
            {
                this.Welcome();
                this.Init();
            }
            catch (Exception exception)
            {
                MetroFramework.MetroMessageBox.Show(this, exception.Message);
            }
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
            txtboxNameHomeHRD.Text = currentData.EmpName;
            dateTimeHomeHRD.Text = currentData.DOB.ToString();
            comboBoxBloodHomeHRD.Text = currentData.BloodGroup;
            comboBoxGenderHomeHRD.Text = currentData.Gender;
            txtboxNationalityHomeHRD.Text = currentData.Nationality;
            txtboxAddressHomeHRD.Text = currentData.Address;
            txtboxEmailHomeHRD.Text = currentData.Email;
            txtboxContactHomeHRD.Text = currentData.Contact;
            txtboxReligionHomeHRD.Text = currentData.Religion;
            comboBoxMaritalHomeHRD.Text = currentData.MaritalStatus;
            txtboxNationalIDHomeHRD.Text = currentData.NID.ToString();
            pictureBoxHomeHRD.ImageLocation = currentData.ImagePath;
        }

        private void Welcome()
        {
            lblWelcome.Text = "Welcome, " + LoginHelper.UserProfile.Name + " (" + LoginHelper.UserProfile.ID + ")";
        }

        private void tileEmployeeManagerHRD_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeManagerHRD empHRD =new EmployeeManagerHRD();
            empHRD.Show();
        }

        private void tileAttendanceHRD_Click(object sender, EventArgs e)
        {
            this.Hide();
            AttendanceHRD attnHRD= new AttendanceHRD();
            attnHRD.Show();
        }

        private void tileLeaveHRD_Click(object sender, EventArgs e)
        {
            this.Hide();
            LeaveHRD leaveHRD= new LeaveHRD();
            leaveHRD.Show();
        }

        private void tilePayrollHRD_Click(object sender, EventArgs e)
        {
            this.Hide();
            PayrollHRD payrollHRD= new PayrollHRD();
            payrollHRD.Show();
        }

        private void tilePayslipHRD_Click(object sender, EventArgs e)
        {
            PayslipHRD payslipHRD= new PayslipHRD();
            this.Hide();
            payslipHRD.Show();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            EmployeeSalaryHRD smHRD= new EmployeeSalaryHRD();
            this.Hide();
            smHRD.Show();
        }

        private void tileLogoutHRD_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginManager lm= new LoginManager("");
            lm.Show();
        }

        private void btnEditHomeHRD_Click(object sender, EventArgs e)
        {
            txtboxNameHomeHRD.ReadOnly = false;
            dateTimeHomeHRD.Enabled = true;
            comboBoxBloodHomeHRD.Enabled = true;
            comboBoxGenderHomeHRD.Enabled = true;
            txtboxNationalityHomeHRD.ReadOnly = false;
            txtboxAddressHomeHRD.ReadOnly = false;
            txtboxEmailHomeHRD.ReadOnly = false;
            txtboxContactHomeHRD.ReadOnly = false;
            txtboxPasswordHomeHRD.ReadOnly = false;
            txtboxPasswordHomeHRD.Enabled = true;
            txtboxReligionHomeHRD.ReadOnly = false;
            comboBoxMaritalHomeHRD.Enabled = true;
            txtboxNationalIDHomeHRD.ReadOnly = false;
            pictureBoxHomeHRD.Enabled = true;
            btnUploadHomeHRD.Enabled = true;
        }

        private void btnSaveHomeHRD_Click(object sender, EventArgs e)
        {
            var user = repoUserAccount.GetById(userID, "");
            if (string.IsNullOrEmpty(txtboxPasswordHomeHRD.Text))
            {
                selectedData.UserAccount.Password = user.Data.Password;
            }
            else
            {
                selectedData.UserAccount.Password = txtboxPasswordHomeHRD.Text;
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

            txtboxPasswordHomeHRD.Text = "";
        }

        private void Fill()
        {
            selectedData.EmpName = txtboxNameHomeHRD.Text;
            selectedData.DOB = dateTimeHomeHRD.Value;
            selectedData.BloodGroup = comboBoxBloodHomeHRD.Text;
            selectedData.Gender = comboBoxGenderHomeHRD.Text;
            selectedData.Nationality = txtboxNationalityHomeHRD.Text;
            selectedData.Address = txtboxAddressHomeHRD.Text;
            selectedData.Email = txtboxEmailHomeHRD.Text;
            selectedData.Contact = txtboxContactHomeHRD.Text;
            selectedData.Religion = txtboxReligionHomeHRD.Text;
            selectedData.MaritalStatus = comboBoxMaritalHomeHRD.Text;
            selectedData.NID = txtboxNationalIDHomeHRD.Text;
            selectedData.ImagePath = pictureBoxHomeHRD.ImageLocation;         
        }

        private void btnUploadHomeHRD_Click(object sender, EventArgs e)
        {
            this.ImageUpload();
        }

        private void ImageUpload()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "images| *.JPG; *.PNG; *.GIF";  
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxHomeHRD.ImageLocation = ofd.FileName;
            }
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            DepartmentHRD dept= new DepartmentHRD();
            this.Hide();
            dept.Show();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            DesignationHRD desig= new DesignationHRD();
            this.Hide();
            desig.Show();
        }
    }
}
