using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;

namespace OOP2.HRMS.FRAMEWORK
{
    public class DelegateCollection
    {
        public delegate void SelectEmployee(string ename, int eID);
        public delegate void SelectPayslip(int EmpId, string EmpName, DateTime PayDate, string Payroll, int Basic, int House, int Medical, int Conveyance, int Addition, int Deduction, int Total);
    }
}
