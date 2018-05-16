using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HRMS.DATA;
using OOP2.HRMS.FRAMEWORK;

namespace OOP2.HRMS.REPO
{
    public class EmployeeSalaryRepo
    {
        HRMSContext context = new HRMSContext();

        public Result<List<EmployeeSalary>> GetAll(string key)
        {

            var result = new Result<List<EmployeeSalary>>();

            try
            {
                IQueryable<EmployeeSalary> query = context.EmployeeSalaries;

                if (ValidationHelper.IsIntValid(key))
                {
                    int id = Int32.Parse(key);

                    query = query.Where(q => q.ID == id);
                }
                else if (ValidationHelper.IsStringValid(key))
                {
                    query = query.Where(q => q.EmployeeInfo.EmpName.Contains(key));
                }

                result.Data = query.ToList();
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }

        public Result<EmployeeSalary> GetById(int id, string key)
        {
            var result = new Result<EmployeeSalary>();
            try
            {
                var query = context.EmployeeSalaries.FirstOrDefault(c => c.EmpID == id && c.IsActive== true);

                result.Data = query;
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
                var objToDelete = context.EmployeeSalaries.FirstOrDefault(q => q.ID == id);
                //var objUserToDelete = context.UserAccounts.FirstOrDefault(q => q.UserID == id);

                if (objToDelete == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";

                    return result;
                }

                //context.UserAccounts.Remove(objUserToDelete);
                context.EmployeeSalaries.Remove(objToDelete);

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

        public Result<EmployeeSalary> Save(EmployeeSalary employeeSalary)
        {
            var result = new Result<EmployeeSalary>();
            bool isNew = false;
            try
            {
                var objToSave = context.EmployeeSalaries.FirstOrDefault(q => q.ID == employeeSalary.ID);

                if (objToSave == null)
                {
                    objToSave = new EmployeeSalary();
                    context.EmployeeSalaries.Add(objToSave);
                    isNew = true;
                }

                objToSave.EmpID = employeeSalary.EmployeeInfo.EmpID;
                objToSave.ID = employeeSalary.ID;
                objToSave.BasicSalary = employeeSalary.BasicSalary;
                objToSave.Conveyance = employeeSalary.Conveyance;
                objToSave.Date = employeeSalary.Date;
                objToSave.HouseAllowance = employeeSalary.HouseAllowance;
                objToSave.Medical = employeeSalary.Medical;
                objToSave.ProvidentFund = employeeSalary.ProvidentFund;
                objToSave.IsActive = employeeSalary.IsActive;


                if (!IsValidToSave(objToSave, result))
                    return result;

                if (employeeSalary.IsActive)
                {

                    var salaries =
                        context.EmployeeSalaries.Where(q => q.EmpID == objToSave.EmpID && q.ID != objToSave.ID);
                    foreach (var salary in salaries)
                    {
                        salary.IsActive = false;
                    }
                }

                context.SaveChanges();

                result.Data = context.EmployeeSalaries.FirstOrDefault(q => q.ID == objToSave.ID);
                if (result.Data != null && isNew)
                {
                    result.Data.EmployeeInfo = context.EmployeeInfoes.FirstOrDefault(d => d.EmpID == result.Data.EmpID);
                }

            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Message = e.Message;
            }

            return result;
        }


        public bool IsValidToSave(EmployeeSalary obj, Result<EmployeeSalary> result)
        {
            if (!ValidationHelper.IsNumberValid(obj.HouseAllowance.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid House Rent";
                return false;
            }

             if (!ValidationHelper.IsNumberValid(obj.BasicSalary.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid House Rent";
                return false;
            }

            if (!ValidationHelper.IsNumberValid(obj.Medical.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid House Rent";
                return false;
            }

            if (!ValidationHelper.IsNumberValid(obj.ProvidentFund.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid House Rent";
                return false;
            }


            if (!ValidationHelper.IsNumberValid(obj.Conveyance.ToString()))
            {
                result.HasError = true;
                result.Message = "Invalid House Rent";
                return false;
            }

            return true;
        }


    }
}