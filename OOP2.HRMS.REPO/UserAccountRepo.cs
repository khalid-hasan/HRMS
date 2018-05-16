using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class UserAccountRepo
    {
        HRMSContext context = new HRMSContext();
        public Result<UserAccount> GetById(int id, string key)
        {
            var result = new Result<UserAccount>();
            try
            {
                UserAccount query = context.UserAccounts.FirstOrDefault(c => c.UserID == id);

                result.Data = query;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
