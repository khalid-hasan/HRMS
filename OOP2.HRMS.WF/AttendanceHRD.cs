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
    public partial class AttendanceHRD : MetroFramework.Forms.MetroForm
    {
        private int userID = LoginHelper.UserProfile.ID;
        private string userName = LoginHelper.UserProfile.Name;
        AttendanceRepo repo = new AttendanceRepo();
        private List<Attendance> currentData = new List<Attendance>();
        public Attendance selectedData { get; set; }
        public AttendanceHRD()
        {
            InitializeComponent();
        }

        private void AttendanceHRD_Load(object sender, EventArgs e)
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
            this.LoadAttendance();
        }

        private void LoadAttendance()
        {
            var result = repo.GetAll(txtBoxAttendanceHRD.Text);

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
                selectedData = new Attendance();
            }
            this.RefreshDGV();
        }

        private void RefreshDGV()
        {
            dgvAttendanceHRD.AutoGenerateColumns = false;
            dgvAttendanceHRD.DataSource = currentData.ToList();
            dgvAttendanceHRD.Refresh();
            dgvAttendanceHRD.ClearSelection();

            for (int i = 0; i < dgvAttendanceHRD.Rows.Count; i++)
            {
                if (dgvAttendanceHRD.Rows[i].Cells[1].Value.ToString() == selectedData.AttnID.ToString())
                {
                    dgvAttendanceHRD.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void AttendanceHRD_FormClosing(object sender, FormClosingEventArgs e)
        {
            HomeHRD home = new HomeHRD();
            home.Show();
            this.Hide();
        }

        private void btnSearchAttendanceHRD_Click(object sender, EventArgs e)
        {
            this.LoadAttendance();
        }

        private void metroPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
