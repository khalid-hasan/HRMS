using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class AttendanceRepo
    {
        HRMSContext context = new HRMSContext();

        public Result<List<Attendance>> GetAll(string key)
        {
            var result = new Result<List<Attendance>>();
            try
            {
                IQueryable<Attendance> query = context.Attendances.OrderBy(e=>e.EmpID).ThenByDescending(e=>e.LogTime);

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }
        public Result<List<Attendance>> GetById(int id, string key)
        {
            var result = new Result<List<Attendance>>();
            try
            {
                IQueryable<Attendance> query = context.Attendances.Where(c => c.EmpID == id);

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<Attendance> Save(Attendance attendance)
        {
            var result = new Result<Attendance>();

            try
            {
                var objToSave = context.Attendances.FirstOrDefault(c => c.AttnID == attendance.AttnID);
                if (objToSave == null)
                {
                    objToSave = new Attendance();
                    context.Attendances.Add(objToSave);
                }

                objToSave.EmpID = attendance.EmpID;
                objToSave.LogTime = attendance.LogTime;


                if (!IsValidToSave(objToSave, result))
                {
                    return result;
                }
                context.SaveChanges();
                result.Data = context.Attendances.FirstOrDefault(c => c.AttnID == objToSave.AttnID);
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public bool IsValidToSave(Attendance obj, Result<Attendance> result)
        {
            if (!context.EmployeeInfoes.Any(e => e.EmpID == obj.EmpID))
            {
                result.HasError = true;
                result.Message = "Invalid Emp ID";
                return false;
            }
            return true;
        }
    }
}
