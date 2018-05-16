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

namespace OOP2.HRMS.WF
{
    public partial class LoginManager : MetroFramework.Forms.MetroForm
    {
        public LoginManager()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Login();
        }

        private void Login()
        {
            HRMSContext context= new HRMSContext();
            int userID;
            if (Int32.TryParse(txtboxUsername.Text, out userID))
            {
                var obj = context.UserAccounts.FirstOrDefault(u =>
                    u.UserID == userID && u.Password.Equals(txtboxPassword.Text));

                if (obj == null)
                {
                    MetroFramework.MetroMessageBox.Show(this, "Invalid ID or Password.");
                    return;
                }

                var obj1 = context.EmployeeInfoes.FirstOrDefault(d => d.EmpID == obj.UserID);

                var up = new UserProfile()
                {
                    ID = obj.UserID,
                    Name = obj1.EmpName,
                    UserTypeID = obj.UserType
                };

                LoginHelper.UserProfile = up;

                if (obj.UserType == (int)EnumCollection.UserType.Director)
                {
                    HomeHRD homeHRD = new HomeHRD();
                    homeHRD.Show();
                }
                else if (obj.UserType == (int)EnumCollection.UserType.Employee)
                {
                    HomeHRE homeHRE = new HomeHRE();
                    homeHRE.Show();
                }
                this.Hide();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Invalid ID or Password.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AttendanceHRE a = new AttendanceHRE();
            a.Show();
            this.Hide();
        }
    }
}
