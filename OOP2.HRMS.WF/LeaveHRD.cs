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
    public partial class LeaveHRD : MetroFramework.Forms.MetroForm
    {
        private int userID = LoginHelper.UserProfile.ID;
        private string userName = LoginHelper.UserProfile.Name;
        LeaveRepo repo= new LeaveRepo();
        private List<LeaveForm> currentData = new List<LeaveForm>();
        public LeaveForm selectedData { get; set; }
        public LeaveHRD()
        {
            InitializeComponent();
        }
        private void LeaveHRD_Load(object sender, EventArgs e)
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
            this.LoadLeaveData();
        }

        private void LoadLeaveData()
        {
            var result = repo.GetAll("");

            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }

            currentData = result.Data.ToList();

            if (currentData.Count > 0)
            {
                selectedData = currentData[0];
            }
            else
            {
                selectedData = new LeaveForm();
            }

            this.PopulateData();
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvLeaveHRD.AutoGenerateColumns = false;
            dgvLeaveHRD.DataSource = currentData.ToList();
            dgvLeaveHRD.Refresh();
            dgvLeaveHRD.ClearSelection();

            for (int i = 0; i < dgvLeaveHRD.Rows.Count; i++)
            {
                if (dgvLeaveHRD.Rows[i].Cells[1].Value.ToString() == selectedData.LeaveID.ToString())
                {
                    dgvLeaveHRD.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void PopulateData()
        {
            txtboxIDLeaveHRD.Text = userID.ToString();
            txtboxNameLeaveHRD.Text = userName;
            txtboxDepartmentLeaveHRD.Text = selectedData.EmployeeInfo.Department.DeptName;
            txtboxDesignationLeaveHRD.Text = selectedData.EmployeeInfo.Designation.DesigName;
            txtboxLeaveTypeLeaveHRD.Text = selectedData.Type;
            txtboxLeavePeriodLeaveHRD.Text = selectedData.Period;
            dateTimeFromLeaveHRD.Text = selectedData.FromDate.ToString();
            dateTimeToLeaveHRD.Text = selectedData.ToDate.ToString();
            txtboxNumberOfDaysLeaveHRD.Text = selectedData.NoOfDays;
            lblStatusChangeLeaveHRD.Visible = true;
            lblStatusChangeLeaveHRD.Text =
                Enum.GetName(typeof(EnumCollection.LeaveStatus), selectedData.Status);
        }

        private void btnRefreshLeaveHRD_Click(object sender, EventArgs e)
        {
            this.Init();
        }

        private void dgvLeaveHRD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvLeaveHRD.Rows[e.RowIndex].Cells[1].Value.ToString());
                selectedData = currentData.FirstOrDefault(c => c.LeaveID == id) ?? new LeaveForm();
                this.PopulateData();
            }
        }

        private void btnApproveLeaveHRD_Click(object sender, EventArgs e)
        {
            this.FillApprove();

            bool isNew = selectedData == null;
            var result = repo.Save(selectedData);

            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }

            if (isNew)
            {
                currentData.Add(result.Data);
            }
            else
            {
                var objToChange = currentData.FirstOrDefault(c => c.LeaveID == result.Data.LeaveID);
                if (objToChange != null)
                {
                    objToChange.Status = result.Data.Status;
                }
            }

            selectedData = result.Data;

            MetroFramework.MetroMessageBox.Show(this, "Approved.");

            this.PopulateData();
            this.RefreshDGV();
        }

        private void FillApprove()
        {
            selectedData.Status = (int)EnumCollection.LeaveStatus.Approved;
        }

        private void btnRejectLeaveHRD_Click(object sender, EventArgs e)
        {
            this.FillReject();

            bool isNew = selectedData == null;
            var result = repo.Save(selectedData);

            if (result.HasError)
            {
                MetroFramework.MetroMessageBox.Show(this, result.Message);
                return;
            }

            if (isNew)
            {
                currentData.Add(result.Data);
            }
            else
            {
                var objToChange = currentData.FirstOrDefault(c => c.LeaveID == result.Data.LeaveID);
                if (objToChange != null)
                {
                    objToChange.Status = result.Data.Status;
                }
            }

            selectedData = result.Data;

            MetroFramework.MetroMessageBox.Show(this, "Rejected.");
            
            this.PopulateData();
            this.RefreshDGV();
        }
        private void FillReject()
        {
            selectedData.Status = (int)EnumCollection.LeaveStatus.Rejected;
        }

        private void LeaveHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }
    }
}
