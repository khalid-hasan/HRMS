using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.HRMS.DATA
{
    public partial class Payslip
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
        public string PayrollName
        {
            get
            {
                if (this.Payroll == null)
                    return "-";

                return this.Payroll.Name;
            }
        }

        public int NetTotal
        {
            get { return this.BasicSalary + this.HouseAllowance + this.Conveyance + this.Medical + this.Addition - this.Deduction; }
        }
    }
}
