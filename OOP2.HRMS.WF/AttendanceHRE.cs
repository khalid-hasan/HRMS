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
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;
using OOP2.HRMS.REPO;

namespace OOP2.HRMS.WF
{
    public partial class AttendanceHRE : MetroFramework.Forms.MetroForm
    {
        AttendanceRepo repo = new AttendanceRepo();

        public AttendanceHRE()
        {
            InitializeComponent();
        }

        private void AttendanceHREmployee_Load(object sender, EventArgs e)
        {

        }



        private void txtEmpID_Enter(object sender, EventArgs e)
        {

        }

        private void AttendanceHRE_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginManager lm = new LoginManager();
            lm.Show();
        }

        private void txtEmpID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!ValidationHelper.IsNumberValid(txtEmpID.Text))
                    return;

                var attendance = new Attendance()
                {
                    EmpID = Int32.Parse(txtEmpID.Text),
                    LogTime = DateTime.Now
                };

                var result = repo.Save(attendance);

                txtEmpID.Text = "";
                if (result.HasError)
                {
                    MetroMessageBox.Show(this, result.Message);
                    lblStatus.Text = "";
                    return;
                }

                lblStatus.Text = result.Data.EmpID + " LOGGED";
                txtEmpID.Focus();
            }
        }
    }
}
