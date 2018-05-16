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
    public partial class LeaveHRE : MetroFramework.Forms.MetroForm
    {
        private int userID = LoginHelper.UserProfile.ID;
        private string userName = LoginHelper.UserProfile.Name;
        LeaveRepo repo= new LeaveRepo();
        private List<LeaveForm> currentData = new List<LeaveForm>();
        private LeaveForm selectedData { get; set; }
        public LeaveHRE()
        {
            InitializeComponent();
        }

        private void LeaveHREmp_Load(object sender, EventArgs e)
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
            txtboxIDLeaveHRD.Text = userID.ToString();
            txtboxNameLeaveHRD.Text = userName;
            this.LoadForm();
        }

        private void LoadForm()
        {
            var result = repo.GetById(userID, "");

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
        private void PopulateData()
        {
            lblStatusLeaveHRE.Visible = true;
            lblStatusLeaveHRE.Text= Enum.GetName(typeof(EnumCollection.LeaveStatus), selectedData.Status);
        }
        private void RefreshDGV()
        {
            dgvLeaveHRE.AutoGenerateColumns = false;
            dgvLeaveHRE.DataSource = currentData.ToList();
            dgvLeaveHRE.Refresh();
            dgvLeaveHRE.ClearSelection();

            for (int i = 0; i < dgvLeaveHRE.Rows.Count; i++)
            {
                if (dgvLeaveHRE.Rows[i].Cells[0].Value.ToString() == selectedData.LeaveID.ToString())
                {
                    dgvLeaveHRE.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void btnApplyLeaveHREmp_Click_1(object sender, EventArgs e)
        {
            selectedData = new LeaveForm(); ;
            this.Fill();

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
                    objToChange.EmpID = result.Data.EmpID;
                    objToChange.Type = result.Data.Type;
                    objToChange.Period = result.Data.Period;
                    objToChange.FromDate = result.Data.FromDate;
                    objToChange.ToDate = result.Data.ToDate;
                    objToChange.NoOfDays = result.Data.NoOfDays;
                    objToChange.Status = result.Data.Status;
                }
            }

            selectedData = result.Data;

            MetroFramework.MetroMessageBox.Show(this, "Applied.");
        }
        private void Fill()
        {
            selectedData.EmpID = userID;
            selectedData.Type = comboBoxLeaveTypeLeaveHREmp.Text;
            selectedData.Period = comboBoxLeavePeriodLeaveHREmp.Text;
            selectedData.FromDate = dateTimeFromLeaveHREmp.Value;
            selectedData.ToDate = dateTimeToLeaveHREmp.Value;
            selectedData.NoOfDays = (dateTimeToLeaveHREmp.Value - dateTimeFromLeaveHREmp.Value).TotalDays.ToString();
            selectedData.Status = (int)EnumCollection.LeaveStatus.Pending;

        }
        private void txtboxNumberOfDayLeaaveHREmp_Click_1(object sender, EventArgs e)
        {
            txtboxNumberOfDayLeaaveHREmp.Text =
                (dateTimeToLeaveHREmp.Value - dateTimeFromLeaveHREmp.Value).TotalDays.ToString();
        }

        private void btnRefreshLeaveHRE_Click(object sender, EventArgs e)
        {
            this.Init();
        }

        private void dgvLeaveHRE_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Int32.Parse(dgvLeaveHRE.Rows[e.RowIndex].Cells[0].Value.ToString());
                selectedData = currentData.FirstOrDefault(c => c.LeaveID == id) ?? new LeaveForm();
                this.PopulateData();
            }
        }

        private void LeaveHRE_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRE home = new HomeHRE();
            home.Show();
            this.Hide();
        }
    }
}
