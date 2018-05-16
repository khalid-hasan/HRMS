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
    public partial class PayslipHRE : MetroFramework.Forms.MetroForm
    {
        private int userID = LoginHelper.UserProfile.ID;
        PayslipRepo repo= new PayslipRepo();
        private List<Payslip> CurrentDatas = new List<Payslip>();
        private Payslip SelectedData { get; set; }
        //private DelegateCollection.SelectPayslip SelectPayslip { get; set; }

        public PayslipHRE()
        {
            InitializeComponent();
        }
/*        public PayslipHRE(DelegateCollection.SelectPayslip sp)
        {
            InitializeComponent();
            SelectPayslip = sp;
        }*/

        private void PayslipHRE_Load(object sender, EventArgs e)
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
            this.LoadPayslip();
        }

        private void LoadPayslip()
        {
            var result = repo.GetById(userID, "");


            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }
            CurrentDatas = result.Data.ToList();

            if (CurrentDatas.Count > 0)
            {
                SelectedData = CurrentDatas[0];

            }
            else
            {
                SelectedData = new Payslip() {Date = DateTime.Now};
            }
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvPayslipHRE.AutoGenerateColumns = false;
            dgvPayslipHRE.DataSource = CurrentDatas.ToList();
            dgvPayslipHRE.Refresh();

            dgvPayslipHRE.ClearSelection();

            for (int i = 0; i < dgvPayslipHRE.Rows.Count; i++)
            {
                if (dgvPayslipHRE.Rows[i].Cells[0].Value.ToString() == SelectedData.ID.ToString())
                {
                    dgvPayslipHRE.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void PayslipHRE_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRE home= new HomeHRE();
            this.Hide();
            home.Show();
        }

        private void dgvPayslipHRE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvPayslipHRE.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentDatas.FirstOrDefault(q => q.ID == id) ?? new Payslip();
            }
        }

        private void btnSelectEmpListESHRD_Click(object sender, EventArgs e)
        {
/*            SelectPayslip(SelectedData.EmpID,
                SelectedData.EmployeeName,
                SelectedData.Date,
                SelectedData.PayrollName,
                SelectedData.BasicSalary,
                SelectedData.HouseAllowance,
                SelectedData.Medical,
                SelectedData.Conveyance,
                SelectedData.Addition,
                SelectedData.Deduction,
                SelectedData.NetTotal);
            PayslipManager pm= new PayslipManager();
            pm.Show();
            this.Hide();*/

            PayslipManager pm= new PayslipManager(SelectedData.EmpID,
                SelectedData.EmployeeName,
                SelectedData.Date,
                SelectedData.PayrollName,
                SelectedData.BasicSalary,
                SelectedData.HouseAllowance,
                SelectedData.Medical,
                SelectedData.Conveyance,
                SelectedData.Addition,
                SelectedData.Deduction,
                SelectedData.NetTotal);
            pm.Show();
            this.Hide();
        }
    }
}
