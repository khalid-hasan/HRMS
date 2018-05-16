using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.HRMS.FRAMEWORK.Object
{
    public class PayslipModule
    {
        public string EmpName { get; set; }
        public int EmpId { get; set; }
        public DateTime PayDate { get; set; }
        public string Payroll { get; set; }
        public int BasicSalary { get; set; }
        public int MedicalAllowance { get; set; }
        public int HouseRent { get; set; }
        public int Conveyance { get; set; }
        public int Addition { get; set; }
        public int Deduction { get; set; }
        public int NetTotal
        {
/*            get { return BasicSalary + MedicalAllowance + HouseRent + Conveyance + Addition + Deduction;}
            set { this.NetTotal = value; }*/
            get;
            set;
        }
    }
}
