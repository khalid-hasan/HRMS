using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.HRMS.DATA
{
    public partial class EmployeeInfo
    {
        public string DepartmentName {
            get
            {
                if (this.Department == null)
                    return "-";

                return this.Department.DeptName;
            }
        }

        public string DesignationName
        {
            get
            {
                if (this.Designation == null)
                    return "-";

                return this.Designation.DesigName;
            }
        }
    }
}
