using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.HRMS.DATA
{
    public partial class EmployeeSalary
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

        public int Salary
        {
            get { return this.BasicSalary + this.HouseAllowance + this.Conveyance + this.Medical - this.ProvidentFund; }
        }
    }
}