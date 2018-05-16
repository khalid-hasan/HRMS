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
    public partial class EmployeeListESHRD : MetroForm
    {
        DelegateCollection.SelectEmployee selectEmp;
        EmployeeInfoRepo emprepo = new EmployeeInfoRepo();
        private EmployeeInfo SelectedData { get; set; }
        private List<EmployeeInfo> CurrentData = new List<EmployeeInfo>();
        private DelegateCollection.SelectEmployee SelectEmp { get; set; }

        public EmployeeListESHRD(DelegateCollection.SelectEmployee se)
        {
            InitializeComponent();
            SelectEmp = se;
        }

        private void EmployeeListESHRD_Load(object sender, EventArgs e)
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
            this.LoadEmpInfo();
        }

        private void LoadEmpInfo()
        {
            var result = emprepo.GetAll("");


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
                SelectedData = new EmployeeInfo() { DOB = DateTime.Now, JoiningDate = DateTime.Now, HireDate = DateTime.Now };
            }


            this.RefreshDGV();

        }

        private void RefreshDGV()
        {
            dgvEmpListESHRD.AutoGenerateColumns = false;
            dgvEmpListESHRD.DataSource = CurrentData.ToList();
            dgvEmpListESHRD.Refresh();

            dgvEmpListESHRD.ClearSelection();

            for (int i = 0; i < dgvEmpListESHRD.Rows.Count; i++)
            {
                if (dgvEmpListESHRD.Rows[i].Cells[0].Value.ToString() == SelectedData.EmpID.ToString())
                {
                    dgvEmpListESHRD.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void dgvEmpListESHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvEmpListESHRD.Rows[e.RowIndex].Cells[0].Value.ToString());
                SelectedData = CurrentData.FirstOrDefault(q => q.EmpID == id) ?? new EmployeeInfo();
            }

        }


        private void btnSelectEmpListESHRD_Click(object sender, EventArgs e)
        {
            SelectEmp(SelectedData.EmpName, SelectedData.EmpID);
            this.Close();

        }

        private void EmployeeListESHRD_FormClosing(object sender, FormClosingEventArgs e)
        { 
            this.Hide();
        }
    }
}