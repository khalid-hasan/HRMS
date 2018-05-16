using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;
using OOP2.HRMS.REPO;

namespace OOP2.HRMS.WF
{
    public partial class EmployeeManagerHRD : MetroForm
    {
        EmployeeInfoRepo emprepo = new EmployeeInfoRepo();
        DepartmentRepo deptrepo = new DepartmentRepo();
        DesignationRepo desigrepo = new DesignationRepo();

        private EmployeeInfo SelectedData { get; set; }
        private List<EmployeeInfo> CurrentData = new List<EmployeeInfo>();


        public EmployeeManagerHRD()
        {
            InitializeComponent();
        }

        private void EmployeeManagerHRD_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitialMode();
            }
            catch (Exception exception)
            {
                MetroMessageBox.Show(this, exception.Message);
            }

        }

        private void InitialMode()
        {
            txtBoxSearchEMHRD.Text = "";
            var resultDept = deptrepo.GetAll("");
            var resultDesig = desigrepo.GetAll("");

            comboBoxDeptEMHRD.DataSource = resultDept.Data.ToList();
            comboBoxDeptEMHRD.DisplayMember = "DeptName";
            comboBoxDeptEMHRD.ValueMember = "DeptID";


            comboBoxDesigEMHRD.DataSource = resultDesig.Data.ToList();
            comboBoxDesigEMHRD.DisplayMember = "DesigName";
            comboBoxDesigEMHRD.ValueMember = "DesigID";

            comboBoxUserTypeEMHRD.DataSource = EnumCollection.GetAllUserTypes();
            comboBoxUserTypeEMHRD.DisplayMember = "Name";
            comboBoxUserTypeEMHRD.ValueMember = "ID";

            this.LoadEmpInfo();
        }

        private void LoadEmpInfo()
        {
            var result = emprepo.GetAll(txtBoxSearchEMHRD.Text);


            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            CurrentData = result.Data.ToList();

            if (CurrentData.Count > 0)
            {
                SelectedData = CurrentData[0];

            }
            else
            {
                SelectedData = new EmployeeInfo() { DOB = DateTime.Now, JoiningDate = DateTime.Now, HireDate = DateTime.Now, UserAccount = new UserAccount() };
            }

            this.PopulateData();
            this.DisableEdit();
            this.RefreshDGV();

        }

        private void RefreshDGV()
        {
            dgvEmployeeManagerHRD.AutoGenerateColumns = false;
            dgvEmployeeManagerHRD.DataSource = CurrentData.ToList();
            dgvEmployeeManagerHRD.Refresh();

            dgvEmployeeManagerHRD.ClearSelection();

            for (int i = 0; i < dgvEmployeeManagerHRD.Rows.Count; i++)
            {
                if (dgvEmployeeManagerHRD.Rows[i].Cells[0].Value.ToString() == SelectedData.EmpID.ToString())
                {
                    dgvEmployeeManagerHRD.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void PopulateData()
        {

            txtBoxNameEMHRD.Text = SelectedData.EmpName;
            txtBoxIdEMHRD.Text = SelectedData.EmpID == 0 ? "Auto-Generated" : SelectedData.EmpID.ToString();
            txtBoxAddressEMHRD.Text = SelectedData.Address;
            txtBoxEmailEMHRD.Text = SelectedData.Email;
            txtBoxContactEMHRD.Text = SelectedData.Contact;
            txtBoxNIDEMHRD.Text = SelectedData.NID;
            txtBoxNationalityEMHRD.Text = SelectedData.Nationality;
            txtBoxReligionEMHRD.Text = SelectedData.Religion;
            comboBoxStatusEMHRD.Text = SelectedData.Status;
            comboBoxDeptEMHRD.SelectedValue = SelectedData.DeptID;
            comboBoxDesigEMHRD.SelectedValue = SelectedData.DesigID;


            dateTimeDOBEMHRD.Value = SelectedData.DOB;
            dateTimeHireEMHRD.Value = SelectedData.HireDate;
            dateTimeJoinEMHRD.Value = SelectedData.JoiningDate;
            comboBoxBloodEMHRD.Text = SelectedData.BloodGroup;
            comboBoxGenderEMHRD.Text = SelectedData.Gender;
            comboBoxMaritalEMHRD.Text = SelectedData.MaritalStatus;
            comboBoxUserTypeEMHRD.SelectedValue = SelectedData.UserAccount.UserType;


        }

        private void NewMode()
        {
            dgvEmployeeManagerHRD.ClearSelection();
            SelectedData = new EmployeeInfo() { DOB = DateTime.Now, JoiningDate = DateTime.Now, HireDate = DateTime.Now, UserAccount = new UserAccount() };
            this.PopulateData();
        }
        private void btnNewEMHRD_Click(object sender, EventArgs e)
        {

            this.EnableEdit();
            this.NewMode();

        }

        private void DisableEdit()
        {
            txtBoxNameEMHRD.ReadOnly = true;
            txtBoxIdEMHRD.ReadOnly = true;
            txtBoxAddressEMHRD.ReadOnly = true;
            txtBoxEmailEMHRD.ReadOnly = true;
            txtBoxContactEMHRD.ReadOnly = true;
            txtBoxNationalityEMHRD.ReadOnly = true;
            txtBoxReligionEMHRD.ReadOnly = true;

            txtBoxNIDEMHRD.ReadOnly = true;
            comboBoxBloodEMHRD.Enabled = false;
            comboBoxGenderEMHRD.Enabled = false;
            comboBoxMaritalEMHRD.Enabled = false;
            comboBoxDeptEMHRD.Enabled = false;
            comboBoxDesigEMHRD.Enabled = false;
            comboBoxStatusEMHRD.Enabled = false;
            dateTimeDOBEMHRD.Enabled = false;
            dateTimeHireEMHRD.Enabled = false;
            dateTimeJoinEMHRD.Enabled = false;
        }

        private void EnableEdit()
        {
            txtBoxNameEMHRD.ReadOnly = false;
            txtBoxAddressEMHRD.ReadOnly = false;
            txtBoxEmailEMHRD.ReadOnly = false;
            txtBoxContactEMHRD.ReadOnly = false;
            txtBoxNationalityEMHRD.ReadOnly = false;
            txtBoxReligionEMHRD.ReadOnly = false;
            txtBoxNIDEMHRD.ReadOnly = false;
            comboBoxBloodEMHRD.Enabled = true;
            comboBoxGenderEMHRD.Enabled = true;
            comboBoxMaritalEMHRD.Enabled = true;
            comboBoxDeptEMHRD.Enabled = true;
            comboBoxDesigEMHRD.Enabled = true;
            comboBoxStatusEMHRD.Enabled = true;
            dateTimeDOBEMHRD.Enabled = true;
            dateTimeHireEMHRD.Enabled = true;
            dateTimeJoinEMHRD.Enabled = true;
        }

        private void dgvEmployeeManagerHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvEmployeeManagerHRD.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentData.FirstOrDefault(q => q.EmpID == id) ?? new EmployeeInfo();

                this.PopulateData();
                this.DisableEdit();
            }
        }

        private void btnSearchEMHRD_Click(object sender, EventArgs e)
        {
            this.LoadEmpInfo();
        }

        private void btnRefreshEMHRD_Click(object sender, EventArgs e)
        {
            this.InitialMode();
        }

        private void btnDeleteEMHRD_Click(object sender, EventArgs e)
        {
            if (SelectedData.EmpID == 0)
            {
                MetroMessageBox.Show(this, "No Information Selected");
                return;
            }

            if (MetroMessageBox.Show(this, "Are you sure", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            var result = emprepo.Delete(SelectedData.EmpID);

            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            CurrentData.Remove(SelectedData);
            this.NewMode();
            this.RefreshDGV();

        }

        private void btnEditEMHRD_Click(object sender, EventArgs e)
        {
            this.EnableEdit();
        }

        private void btnSaveEMHRD_Click(object sender, EventArgs e)
        {

            this.FillData();
            bool NewData = SelectedData.EmpID == 0;
            var result = emprepo.Save(SelectedData);

            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            if (NewData)
            {
                try
                {
                    MailMessage mail = new MailMessage("khalid.bd13@gmail.com", txtBoxEmailEMHRD.Text);
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    //mail.From = new MailAddress("khalidibnehasan@gmail.com");
                    //mail.To.Add(new MailAddress("kh.coderbd@gmail.com"));
                    mail.Subject = "Your Employee Account Details";
                    mail.Body = "Your Username is: "+ result.Data.EmpID + " and Password is: "+ result.Data.UserAccount.Password;

                    SmtpServer.Port = 587;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("khalid.bd13@gmail.com", "h1a2s3a4n5");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    MetroMessageBox.Show(this, "Account Credentials Sent To Employee\'s Email.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                CurrentData.Add(result.Data);
                MetroFramework.MetroMessageBox.Show(this, result.Message);
            }
            else
            {

                var objChanged = CurrentData.FirstOrDefault(q => q.EmpID == result.Data.EmpID);

                if (objChanged != null)
                {
                    objChanged.EmpID = result.Data.EmpID;
                    objChanged.Address = result.Data.Address;
                    objChanged.BloodGroup = result.Data.BloodGroup;
                    objChanged.Contact = result.Data.Contact;
                    objChanged.EmpName = result.Data.EmpName;
                    objChanged.Email = result.Data.Email;
                    objChanged.DOB = result.Data.DOB;
                    objChanged.Gender = result.Data.Gender;
                    objChanged.MaritalStatus = result.Data.MaritalStatus;
                    objChanged.NID = result.Data.NID;
                    objChanged.Nationality = result.Data.Nationality;
                    objChanged.Religion = result.Data.Religion;
                    objChanged.Status = result.Data.Status;
                    objChanged.Department = result.Data.Department;
                    objChanged.Designation = result.Data.Designation;
                    objChanged.JoiningDate = result.Data.JoiningDate;
                    objChanged.HireDate = result.Data.HireDate;
                    objChanged.UserAccount.UserID = result.Data.UserAccount.UserID;
                    objChanged.UserAccount.UserType = result.Data.UserAccount.UserType;



                }
            }

            SelectedData = result.Data;

            MetroMessageBox.Show(this, "Employee Added");

            this.PopulateData();
            this.RefreshDGV();


        }

        private void FillData()
        {

            SelectedData.Address = txtBoxAddressEMHRD.Text;
            SelectedData.BloodGroup = comboBoxBloodEMHRD.Text;
            SelectedData.Contact = txtBoxContactEMHRD.Text;
            SelectedData.EmpName = txtBoxNameEMHRD.Text;
            SelectedData.Email = txtBoxEmailEMHRD.Text;
            SelectedData.DOB = dateTimeDOBEMHRD.Value;
            SelectedData.Gender = comboBoxGenderEMHRD.Text;
            SelectedData.MaritalStatus = comboBoxMaritalEMHRD.Text;
            SelectedData.NID = txtBoxNIDEMHRD.Text;
            SelectedData.Nationality = txtBoxNationalityEMHRD.Text;
            SelectedData.Religion = txtBoxReligionEMHRD.Text;
            SelectedData.Status = comboBoxStatusEMHRD.Text;
            SelectedData.DeptID = Int32.Parse(comboBoxDeptEMHRD.SelectedValue.ToString());
            SelectedData.DesigID = Int32.Parse(comboBoxDesigEMHRD.SelectedValue.ToString());
            SelectedData.JoiningDate = dateTimeJoinEMHRD.Value;
            SelectedData.HireDate = dateTimeHireEMHRD.Value;
            SelectedData.UserAccount.UserType = Int32.Parse(comboBoxUserTypeEMHRD.SelectedValue.ToString());
            SelectedData.UserAccount.Password= new Random().Next(1000, 9999).ToString();


        }

        private void EmployeeManagerHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }
    }
}