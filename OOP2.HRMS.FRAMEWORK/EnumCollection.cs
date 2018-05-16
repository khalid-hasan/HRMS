using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.HRMS.FRAMEWORK
{
    public class EnumCollection
    {
        public enum UserType
        {
            Director = 1,
            Employee = 2
        }

        public enum LeaveStatus
        {
            Approved = 1,
            Rejected = 2,
            Pending = 3
        }

        public static List<EnumDetail> GetAllUserTypes()
        {
            var list = new List<EnumDetail>();

            list.Add(new EnumDetail()
            {
                ID = 1,
                Name = "Director"
            });

            list.Add(new EnumDetail()
            {
                ID = 2,
                Name = "Employee"
            });

            return list;
        }
    }

        public class EnumDetail
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
}