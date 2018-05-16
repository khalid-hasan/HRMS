using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class LeaveRepo
    {
        HRMSContext context= new HRMSContext();

        public Result<List<LeaveForm>> GetAll(string key)
        {
            var result = new Result<List<LeaveForm>>();
            try
            {
                IQueryable<LeaveForm> query = context.LeaveForms;

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
        public Result<List<LeaveForm>> GetById(int id, string key)
        {
            var result = new Result<List<LeaveForm>>();
            try
            {
                IQueryable<LeaveForm> query = context.LeaveForms.Where(c => c.EmpID == id);

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<LeaveForm> Save(LeaveForm leave)
        {
            var result = new Result<LeaveForm>();

            try
            {
                var objToSave = context.LeaveForms.FirstOrDefault(c => c.LeaveID == leave.LeaveID);
                if (objToSave == null)
                {
                    objToSave = new LeaveForm();
                    context.LeaveForms.Add(objToSave);
                }

                objToSave.EmpID = leave.EmpID;
                objToSave.Type = leave.Type;
                objToSave.Period = leave.Period;
                objToSave.FromDate = leave.FromDate;
                objToSave.ToDate = leave.ToDate;
                objToSave.NoOfDays = leave.NoOfDays;
                objToSave.Status = leave.Status;


                if (!IsValidToSave(objToSave, result))
                {
                    return result;
                }
                context.SaveChanges();
                result.Data = context.LeaveForms.FirstOrDefault(c => c.LeaveID == objToSave.LeaveID);
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public bool IsValidToSave(LeaveForm obj, Result<LeaveForm> result)
        {
            return true;
        }
    }
}
