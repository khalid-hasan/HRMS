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
    public partial class PayslipHRD : MetroFramework.Forms.MetroForm
    {
        HRMSContext context = new HRMSContext();
        PayslipRepo repo = new PayslipRepo();
        EmployeeSalaryRepo salaryRepo= new EmployeeSalaryRepo();
        private List<Payslip> CurrentData = new List<Payslip>();
        private  EmployeeSalary CurrentSalary= new EmployeeSalary();
        private Payslip SelectedData { get; set; }
        private string ename;
        private int eID;
        public PayslipHRD()
        {
            InitializeComponent();
        }

        private void PayslipHRD_Load(object sender, EventArgs e)
        {
            try
            {
                this.Init();
            }
            catch (Exception exception)
            {
                MetroFramework.MetroMessageBox.Show(this, exception.Message);
            }
        }

        private void Init()
        {
            comboBoxPayrollDatePayrollHRD.DataSource = context.Payrolls.ToList();
            comboBoxPayrollDatePayrollHRD.DisplayMember = "Name";
            comboBoxPayrollDatePayrollHRD.ValueMember = "ID";

            this.LoadPayslip();
        }

        private void LoadPayslip()
        {
            var result = repo.GetAll("");


            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }

            CurrentData = result.Data.ToList();

            if (CurrentData.Count > 0)
            {
                SelectedData = CurrentData[0];

            }
            else
            {
                SelectedData = new Payslip(){Date = DateTime.Now};
            }

            this.PopulateData();
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvPayslipHRD.AutoGenerateColumns = false;
            dgvPayslipHRD.DataSource = CurrentData.ToList();
            dgvPayslipHRD.Refresh();

            dgvPayslipHRD.ClearSelection();

            for (int i = 0; i < dgvPayslipHRD.Rows.Count; i++)
            {
                if (dgvPayslipHRD.Rows[i].Cells[0].Value.ToString() == SelectedData.ID.ToString())
                {
                    dgvPayslipHRD.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void PopulateData()
        {
            txtBoxIDPayslipHRD.Text = SelectedData.ID == 0 ? "Auto-Generated" : SelectedData.ID.ToString();
            txtBoxNamePayslipHRD.Text = SelectedData.EmployeeName;
            comboBoxPayrollDatePayrollHRD.SelectedValue = SelectedData.PayrollID;
/*            txtBoxSalaryPayrollHRD.Text = SelectedData.EmployeeSalary.BasicSalary.ToString();
            txtBoxHousePayslipHRD.Text = SelectedData.EmployeeSalary.HouseAllowance.ToString();
            txtBoxMedicalPayrollHRD.Text = SelectedData.EmployeeSalary.Medical.ToString();
            txtBoxConveyancePayrollHRD.Text = SelectedData.EmployeeSalary.Conveyance.ToString();*/

            txtBoxSalaryPayrollHRD.Text = SelectedData.BasicSalary.ToString();
            txtBoxHousePayslipHRD.Text = SelectedData.HouseAllowance.ToString();
            txtBoxMedicalPayrollHRD.Text = SelectedData.Medical.ToString();
            txtBoxConveyancePayrollHRD.Text = SelectedData.Conveyance.ToString();

            txtBoxAdditionPayrollHRD.Text = SelectedData.Addition.ToString();
            txtBoxDeductionPayrollHRD.Text = SelectedData.Deduction.ToString();
            dateTimeDatePayrollHRD.Value = SelectedData.Date;
            comboBoxStatusPayrollHRD.Text = SelectedData.StatusID;

            btnSavePayslipHRD.Enabled = SelectedData.StatusID != "Post";
        }

        private void dgvPayslipHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvPayslipHRD.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentData.FirstOrDefault(q => q.ID == id) ?? new Payslip(){ Date = DateTime.Now };

                this.PopulateData();
            }
        }

        private void btnRefreshPayslipHRD_Click(object sender, EventArgs e)
        {
            this.Init();
        }

        private void btnSavePayslipHRD_Click(object sender, EventArgs e)
        {
            this.Fill();
            bool NewData = SelectedData.ID == 0;
            var result = repo.Save(SelectedData);

            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
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
                    objChanged.PayrollID = result.Data.PayrollID;
                    //objChanged.EmpSalaryID = result.Data.EmpSalaryID;
                    objChanged.BasicSalary = result.Data.BasicSalary;
                    objChanged.HouseAllowance = result.Data.HouseAllowance;
                    objChanged.Medical = result.Data.Medical;
                    objChanged.Conveyance = result.Data.Conveyance;
                    objChanged.Addition = result.Data.Addition;
                    objChanged.Deduction = result.Data.Deduction;
                    objChanged.Date = result.Data.Date;
                    objChanged.StatusID = result.Data.StatusID;
                }
            }

            SelectedData = result.Data;

            MetroFramework.MetroMessageBox.Show(this, "Entry Successful.");
            this.RefreshDGV();
            this.PopulateData();
        }

        private void Fill()
        {
            SelectedData.EmpID = SelectedData.EmployeeInfo.EmpID;
            SelectedData.PayrollID = Int32.Parse(comboBoxPayrollDatePayrollHRD.SelectedValue.ToString());
            SelectedData.BasicSalary = Int32.Parse(txtBoxSalaryPayrollHRD.Text);
            SelectedData.HouseAllowance = Int32.Parse(txtBoxHousePayslipHRD.Text);
            SelectedData.Medical = Int32.Parse(txtBoxMedicalPayrollHRD.Text);
            SelectedData.Conveyance = Int32.Parse(txtBoxConveyancePayrollHRD.Text);
            SelectedData.Addition = Int32.Parse(txtBoxAdditionPayrollHRD.Text);
            SelectedData.Deduction = Int32.Parse(txtBoxDeductionPayrollHRD.Text);
            SelectedData.Date = dateTimeDatePayrollHRD.Value;
            SelectedData.StatusID = comboBoxStatusPayrollHRD.Text;

        }

        private void btnDeletePayslipHRD_Click(object sender, EventArgs e)
        {
            if (SelectedData.ID == 0)
            {
                MetroFramework.MetroMessageBox.Show(this, "No Information Selected");

            }

            if (MetroFramework.MetroMessageBox.Show(this, "Are you sure", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            var result = repo.Delete(SelectedData.ID);

            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }

            CurrentData.Remove(SelectedData);
            this.RefreshDGV();
        }

        private void PayslipHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }

        private void btnAddPayslipHRD_Click(object sender, EventArgs e)
        {
            this.New();
        }

        private void New()
        {
            dgvPayslipHRD.ClearSelection();
            SelectedData = new Payslip(){Date = DateTime.Now};
            this.PopulateData();
            btnMoreEmpPayslipHRD.Enabled = true;
        }

        private void btnMoreEmpPayslipHRD_Click(object sender, EventArgs e)
        {
            DelegateCollection.SelectEmployee se = new DelegateCollection.SelectEmployee(CollectInfo);
            EmployeeListESHRD emplist = new EmployeeListESHRD(se);
            emplist.Show();
        }

        public void CollectInfo(string ename, int eID)
        {

            SelectedData.EmployeeInfo = new EmployeeInfo() { EmpID = eID, EmpName = ename };
            this.PopulateDatabyID();
        }

        private void PopulateDatabyID()
        {
            var empsSalary = salaryRepo.GetById(SelectedData.EmployeeInfo.EmpID, "");

            CurrentSalary = empsSalary.Data;

            txtBoxNamePayslipHRD.Text = CurrentSalary.EmployeeName;
            txtBoxSalaryPayrollHRD.Text = CurrentSalary.BasicSalary.ToString();
            txtBoxHousePayslipHRD.Text = CurrentSalary.HouseAllowance.ToString();
            txtBoxMedicalPayrollHRD.Text = CurrentSalary.Medical.ToString();
            txtBoxConveyancePayrollHRD.Text = CurrentSalary.Conveyance.ToString();
        }
    }
}
