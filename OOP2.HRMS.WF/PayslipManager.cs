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
using OOP2.HRMS.FRAMEWORK.Object;
using OOP2.HRMS.Reporting;
using OOP2.HRMS.REPO;

namespace OOP2.HRMS.WF
{
    public partial class PayslipManager : MetroFramework.Forms.MetroForm
    {
        public PayslipManager()
        {
            InitializeComponent();
        }

        public PayslipManager(int EmpId, string EmpName, DateTime PayDate, string Payroll, int Basic, int House, int Medical, int Conveyance, int Addition, int Deduction, int Total)
        {
            InitializeComponent();
            PayslipModule pm= new PayslipModule()
            {
                EmpId = EmpId,
                EmpName = EmpName,
                PayDate = PayDate,
                Payroll = Payroll,
                BasicSalary = Basic,
                HouseRent = House,
                MedicalAllowance = Medical,
                Conveyance = Conveyance,
                Addition = Addition,
                Deduction = Deduction,
                NetTotal = Total
            };

            List<PayslipModule> payslipModules = new List<PayslipModule>();
            payslipModules.Add(pm);

            PayslipReport pr = new PayslipReport();
            pr.DataSource = payslipModules;
            reportViewer1.ReportSource = pr;
            reportViewer1.RefreshReport();
        }
        private void PayslipManager_Load(object sender, EventArgs e)
        {
            //this.Init();
        }

        private void PayslipManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            PayslipHRE ps= new PayslipHRE();
            this.Hide();
            ps.Show();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        /*        private void Init()
                {
                    DelegateCollection.SelectPayslip sp= new DelegateCollection.SelectPayslip(CollectInfo);
                    PayslipHRE payslip= new PayslipHRE(sp);
                }

                public void CollectInfo(int EmpId, string EmpName, DateTime PayDate, string Payroll, int Basic, int House, int Medical, int Conveyance, int Addition, int Deduction, int Total)
                {
                    PayslipModule pm= new PayslipModule(){EmpName = EmpName, EmpId = EmpId, PayDate = PayDate, Payroll = Payroll, BasicSalary = Basic, HouseRent = House, MedicalAllowance = Medical, Conveyance = Conveyance, Addition = Addition, Deduction = Deduction, NetTotal = Total};

                    List<PayslipModule> payslipModules = new List<PayslipModule>();
                    payslipModules.Add(pm);

                    PayslipReport pr= new PayslipReport();
                    pr.DataSource = payslipModules;
                    reportViewer1.ReportSource = pr;
                    reportViewer1.RefreshReport();
                }*/
    }
}
