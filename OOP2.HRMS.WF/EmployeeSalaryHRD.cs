using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class EmployeeSalaryHRD : MetroForm
    {
        EmployeeSalaryRepo salrepo = new EmployeeSalaryRepo();
        private EmployeeSalary SelectedData { get; set; }
        private List<EmployeeSalary> CurrentData = new List<EmployeeSalary>();

        //private bool isInValid = false;
        private string ename;
        private int eID;
        public EmployeeSalaryHRD()
        {
            InitializeComponent();
        }


        private void EmployeeSalary_Load(object sender, EventArgs e)
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
            txtBoxSearchESHRD.Text = "";

            this.LoadInfo();
        }

        private void LoadInfo()
        {
            var result = salrepo.GetAll(txtBoxSearchESHRD.Text);

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
                SelectedData = new EmployeeSalary() { Date = DateTime.Now };
            }

            this.Populate();
            this.DisableEdit();
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvEmployeeSalaryHRD.AutoGenerateColumns = false;
            dgvEmployeeSalaryHRD.DataSource = CurrentData.ToList();
            dgvEmployeeSalaryHRD.Refresh();

            dgvEmployeeSalaryHRD.ClearSelection();

            for (int i = 0; i < dgvEmployeeSalaryHRD.Rows.Count; i++)
            {
                if (dgvEmployeeSalaryHRD.Rows[i].Cells[0].Value.ToString() == SelectedData.ID.ToString())
                {
                    dgvEmployeeSalaryHRD.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void Populate()
        {
            txtBoxIDESHRD.Text = SelectedData.ID == 0 ? "Auto Generated" : SelectedData.ID.ToString();
            txtBoxEmpNameESHRD.Text = SelectedData.EmployeeName;
            txtBoxConveyanceESHRD.Text = SelectedData.Conveyance.ToString();
            txtBoxSalaryESHRD.Text = SelectedData.BasicSalary.ToString();
            txtBoxMedicalESHRD.Text = SelectedData.Medical.ToString();
            txtBoxHouseRentESlHRD.Text = SelectedData.HouseAllowance.ToString();
            txtBoxFundESHRD.Text = SelectedData.ProvidentFund.ToString();
            dateTimeDateESHRD.Value = SelectedData.Date;
            chkIsActiveESHRD.Checked = SelectedData.IsActive;
        }

        private void NewMode()
        {
            dgvEmployeeSalaryHRD.ClearSelection();
            SelectedData = new EmployeeSalary() { Date = DateTime.Now };
            this.Populate();
            btnMoreEmpESHRD.Enabled = true;
        }

        private void DisableEdit()
        {
            txtBoxFundESHRD.ReadOnly = true;
            txtBoxEmpNameESHRD.ReadOnly = true;
            txtBoxConveyanceESHRD.ReadOnly = true;
            txtBoxHouseRentESlHRD.ReadOnly = true;
            txtBoxMedicalESHRD.ReadOnly = true;
            txtBoxSalaryESHRD.ReadOnly = true;
            txtBoxIDESHRD.ReadOnly = true;
            dateTimeDateESHRD.Enabled = false;
            btnMoreEmpESHRD.Enabled = false;
            chkIsActiveESHRD.Enabled = false;
        }

        private void EnableEdit()
        {
            txtBoxFundESHRD.ReadOnly = false;
            //txtBoxEmpNameESHRD.ReadOnly = false;
            txtBoxConveyanceESHRD.ReadOnly = false;
            txtBoxHouseRentESlHRD.ReadOnly = false;
            txtBoxMedicalESHRD.ReadOnly = false;
            txtBoxSalaryESHRD.ReadOnly = false;
            dateTimeDateESHRD.Enabled = true;
            chkIsActiveESHRD.Enabled = true;

        }

        private void btnNewESHRD_Click(object sender, EventArgs e)
        {
            this.EnableEdit();
            this.NewMode();


        }

        private void dgvEmployeeSalaryHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvEmployeeSalaryHRD.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentData.FirstOrDefault(q => q.ID == id) ?? new EmployeeSalary();

                this.Populate();
            }

            btnMoreEmpESHRD.Enabled = false;
            chkIsActiveESHRD.Enabled = false;
        }

        private void btnSearchESHRD_Click(object sender, EventArgs e)
        {
            this.LoadInfo();
        }

        private void btnRefreshESHRD_Click(object sender, EventArgs e)
        {
            this.InitialMode();
        }

        private void btnEditESHRD_Click(object sender, EventArgs e)
        {
            this.EnableEdit();
        }

        private void btnDeleteESHRD_Click(object sender, EventArgs e)
        {
            if (SelectedData.ID == 0)
            {
                MetroMessageBox.Show(this, "Please select a Row First");
                return;
            }

            if (MetroMessageBox.Show(this, "Are you sure", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            var result = salrepo.Delete(SelectedData.ID);

            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            CurrentData.Remove(SelectedData);
            this.NewMode();
            this.RefreshDGV();
        }

        private void btnMoreEmpESHRD_Click(object sender, EventArgs e)
        {
            DelegateCollection.SelectEmployee se  = new DelegateCollection.SelectEmployee(CollectInfo);
            EmployeeListESHRD emplist = new EmployeeListESHRD(se);
            emplist.Show();
        }

        public void CollectInfo(string ename, int eID)
        {

            SelectedData.EmployeeInfo = new EmployeeInfo() { EmpID = eID, EmpName = ename };
            this.Populate();
        }


        private void btnSaveESHRD_Click(object sender, EventArgs e)
        {
            if (!this.FillData())
                return;


            bool NewData = SelectedData.ID == 0;
            var result = salrepo.Save(SelectedData);

            if (result.HasError)
            {
                MetroMessageBox.Show(this, result.Message);
                return;
            }

            if (NewData)
            {
                CurrentData.Add(result.Data);

            }
            else
            {

                var objChanged = CurrentData.FirstOrDefault(q => q.ID == result.Data.ID);

                if (objChanged != null)
                {
                    objChanged.EmpID = result.Data.EmpID;
                    objChanged.ID = result.Data.ID;
                    objChanged.Date = result.Data.Date;
                    objChanged.HouseAllowance = result.Data.HouseAllowance;
                    objChanged.BasicSalary = result.Data.BasicSalary;
                    objChanged.Medical = result.Data.Medical;
                    objChanged.ProvidentFund = result.Data.ProvidentFund;
                    objChanged.Conveyance = result.Data.Conveyance;
                    objChanged.IsActive = result.Data.IsActive;

                }
            }

            SelectedData = result.Data;

            MetroMessageBox.Show(this, "Salary Added");

            this.Populate();
            this.RefreshDGV();
        }

        private bool FillData()
        {
            try
            {
                SelectedData.BasicSalary = Int32.Parse(txtBoxSalaryESHRD.Text);
                SelectedData.Conveyance = Int32.Parse(txtBoxConveyanceESHRD.Text);
                SelectedData.Medical = Int32.Parse(txtBoxMedicalESHRD.Text);
                SelectedData.Date = dateTimeDateESHRD.Value;
                SelectedData.HouseAllowance = Int32.Parse(txtBoxHouseRentESlHRD.Text);
                SelectedData.ProvidentFund = Int32.Parse(txtBoxFundESHRD.Text);
                SelectedData.IsActive = chkIsActiveESHRD.Checked;
                SelectedData.EmpID = eID;

                return true;
            }
            catch (Exception e)
            {
                MetroMessageBox.Show(this, "Invalid Input");
                return false;

            }


        }

        private void EmployeeSalaryHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }
    }
}