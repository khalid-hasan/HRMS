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
using OOP2.HRMS.REPO;

namespace OOP2.HRMS.WF
{
    public partial class PayrollHRD : MetroFramework.Forms.MetroForm
    {
        HRMSContext context = new HRMSContext();
        PayrollRepo repo = new PayrollRepo();
        private List<Payroll> CurrentData = new List<Payroll>();
        private Payroll SelectedData { get; set; }
        public PayrollHRD()
        {
            InitializeComponent();
        }

        private void PayrollHRD_Load(object sender, EventArgs e)
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
            this.LoadPayroll();
        }

        private void LoadPayroll()
        {
            var result = repo.GetAll(txtBoxSearchPayrollHRD.Text);


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
                SelectedData = new Payroll();
            }

            this.PopulateData();
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvPayrollHRD.AutoGenerateColumns = false;
            dgvPayrollHRD.DataSource = CurrentData.ToList();
            dgvPayrollHRD.Refresh();

            dgvPayrollHRD.ClearSelection();

            for (int i = 0; i < dgvPayrollHRD.Rows.Count; i++)
            {
                if (dgvPayrollHRD.Rows[i].Cells[0].Value.ToString() == SelectedData.ID.ToString())
                {
                    dgvPayrollHRD.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void PopulateData()
        {
            txtboxIDPayrollHRD.Text = SelectedData.ID == 0 ? "Auto-Generated" : SelectedData.ID.ToString();
            txtboxNamePayrollHRD.Text = SelectedData.Name == "" ? "Auto-Generated" : SelectedData.Name;
            txtBoxYearPayrollHRD.Text = SelectedData.Year.ToString();
            comboBoxMonthPayrollHRD.Text = SelectedData.Month.ToString();
        }

        private void PopulateDataOnNew()
        {
            txtboxIDPayrollHRD.Text = SelectedData.ID == 0 ? "Auto-Generated" : SelectedData.ID.ToString();
            txtboxNamePayrollHRD.Text = SelectedData.Name == "" ? "Auto-Generated" : SelectedData.Name;
            txtBoxYearPayrollHRD.Text = "";
            comboBoxMonthPayrollHRD.SelectedIndex = -1 ;
        }

        private void dgvPayrollHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvPayrollHRD.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentData.FirstOrDefault(q => q.ID == id) ?? new Payroll();

                this.PopulateData();
            }
        }

        private void btnSearchPayrollHRD_Click(object sender, EventArgs e)
        {
            this.LoadPayroll();
        }

        private void btnRefreshPayrollHRD_Click(object sender, EventArgs e)
        {
            this.Init();
        }

        private void btnNewEMHRD_Click(object sender, EventArgs e)
        {
            this.New();
            this.EnableFields();
        }

        private void New()
        {
            dgvPayrollHRD.ClearSelection();
            SelectedData = new Payroll();
            this.PopulateDataOnNew();
        }

        private void EnableFields()
        {
            txtBoxYearPayrollHRD.ReadOnly = false;
            txtBoxYearPayrollHRD.Enabled = true;
            comboBoxMonthPayrollHRD.Enabled = true;
            btnSavePayrollHRD.Enabled = true;
        }

        private void btnEditEMHRD_Click(object sender, EventArgs e)
        {
            this.EnableFields();
        }

        private void btnSavePayrollHRD_Click(object sender, EventArgs e)
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
                    objChanged.Name = result.Data.Name;
                    objChanged.Month = result.Data.Month;
                    objChanged.Year = result.Data.Year;
                }
            }

            SelectedData = result.Data;

            MetroFramework.MetroMessageBox.Show(this, "Entry Successful.");
            this.RefreshDGV();
            this.PopulateData();
        }

        private void Fill()
        {
            SelectedData.Month = Int32.Parse(comboBoxMonthPayrollHRD.Text);
            SelectedData.Year = Int32.Parse(txtBoxYearPayrollHRD.Text);
            DateTime dt= new DateTime(1111,SelectedData.Month,1);
            SelectedData.Name = dt.ToString("MMMM") + ", " + SelectedData.Year;
        }

        private void btnDeleteEMHRD_Click(object sender, EventArgs e)
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
            this.New();
            this.RefreshDGV();
        }

        private void PayrollHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }
    }
}
