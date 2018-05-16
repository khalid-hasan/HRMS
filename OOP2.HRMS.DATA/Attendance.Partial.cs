using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.HRMS.DATA
{
    public partial class Attendance
    {
        public string EmployeeName
        {
            get
            {
                if (this.EmployeeInfo == null)
                    return "-";

                return this.EmployeeInfo.EmpName;
            }
        }
    }
}
