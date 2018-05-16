using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class DepartmentRepo
    {
        HRMSContext context = new HRMSContext();

        public Result<List<Department>> GetAll(string key)
        {
            var result = new Result<List<Department>>();
            try
            {
                IQueryable<Department> query = context.Departments;

                /* if (ValidationHelper.IsIntValid(key))
                 {
                     int id = Int32.Parse(key);
                     query = query.Where(q => q.DeptID == id);
                 }
                 else if (ValidationHelper.IsStringValid(key))
                 {
                     query = query.Where(q => q.DeptName.Contains(key));
                 }*/

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<bool> Delete(int id)
        {
            var result = new Result<bool>();
            try
            {
                var objToDelete = context.Departments.FirstOrDefault(q => q.DeptID == id);


                if (objToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";

                    return result;
                }


                context.Departments.Remove(objToDelete);

                context.SaveChanges();
                result.Data = true;

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }


            return result;
        }

        public Result<Department> Save(Department depatmentInfo)
        {
            var result = new Result<Department>();

            try
            {
                var objToSave = context.Departments.FirstOrDefault(q => q.DeptID == depatmentInfo.DeptID);

                if (objToSave == null)
                {
                    objToSave = new Department();
                    context.Departments.Add(objToSave);

                }

                objToSave.DeptID = depatmentInfo.DeptID;
                objToSave.DeptName = depatmentInfo.DeptName;

                context.SaveChanges();
                result.Data = context.Departments.FirstOrDefault(q => q.DeptID == objToSave.DeptID);


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
